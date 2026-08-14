// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Analysis;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Numerics;

using Hexa.NET.ImGui;

using ktsu.Semantics.Color;
using ktsu.ThemeProvider.ImGui;

/// <summary>
/// Runs the four color checks against the actual ImGui palette a theme produces, plus the
/// semantic palette it is derived from. Analyzes the real shipped output of
/// <see cref="ImGuiPaletteMapper"/> so a change to the mapper is caught here too.
/// </summary>
internal static class PaletteAnalyzer
{
	// The semantic meanings ImGuiPaletteMapper actually consumes. Kept in sync with that mapper by hand;
	// meanings outside this set are reported as unused, and a missing member is reported as a coverage gap.
	private static readonly SemanticMeaning[] MapperUsedMeanings =
	[
		SemanticMeaning.Neutral,
		SemanticMeaning.Primary,
		SemanticMeaning.Alternate,
	];

	// Text-over-surface pairs. Body text is drawn on all of these; each must clear WCAG AA.
	private static readonly (ImGuiCol Fg, ImGuiCol Bg)[] TextPairs =
	[
		(ImGuiCol.Text, ImGuiCol.WindowBg),
		(ImGuiCol.Text, ImGuiCol.ChildBg),
		(ImGuiCol.Text, ImGuiCol.PopupBg),
		(ImGuiCol.Text, ImGuiCol.MenuBarBg),
		(ImGuiCol.Text, ImGuiCol.FrameBg),
		(ImGuiCol.Text, ImGuiCol.Button),
		(ImGuiCol.Text, ImGuiCol.Header),
		(ImGuiCol.Text, ImGuiCol.TitleBgActive),
		(ImGuiCol.Text, ImGuiCol.Tab),
		(ImGuiCol.Text, ImGuiCol.TabSelected),
		(ImGuiCol.Text, ImGuiCol.TableHeaderBg),
	];

	// Disabled text is intentionally muted; it only needs to stay above the invisibility floor.
	private static readonly (ImGuiCol Fg, ImGuiCol Bg)[] DisabledTextPairs =
	[
		(ImGuiCol.TextDisabled, ImGuiCol.WindowBg),
		(ImGuiCol.TextDisabled, ImGuiCol.FrameBg),
	];

	// UI glyphs (the checkmark) drawn inside frames; WCAG 1.4.11 non-text contrast applies.
	private static readonly (ImGuiCol Fg, ImGuiCol Bg)[] GlyphPairs =
	[
		(ImGuiCol.CheckMark, ImGuiCol.FrameBg),
		(ImGuiCol.CheckMark, ImGuiCol.WindowBg),
	];

	// Adjacent interaction states that must be visibly distinct.
	private static readonly (ImGuiCol A, ImGuiCol B)[] StatePairs =
	[
		(ImGuiCol.Button, ImGuiCol.ButtonHovered),
		(ImGuiCol.ButtonHovered, ImGuiCol.ButtonActive),
		(ImGuiCol.FrameBg, ImGuiCol.FrameBgHovered),
		(ImGuiCol.FrameBgHovered, ImGuiCol.FrameBgActive),
	];

	// Accent meanings and the priorities the mapper draws from them, used for the fidelity check.
	private static readonly (SemanticMeaning Meaning, Priority[] Priorities)[] AccentUsage =
	[
		(SemanticMeaning.Primary, [Priority.MediumLow, Priority.Medium, Priority.High, Priority.VeryHigh]),
		(SemanticMeaning.Alternate, [Priority.Medium, Priority.High]),
	];

	/// <summary>Analyzes one theme and returns its full result.</summary>
	/// <param name="info">The theme to analyze.</param>
	/// <returns>The analysis, including headline metrics and any findings.</returns>
	public static ThemeAnalysis Analyze(ThemeRegistry.ThemeInfo info)
	{
		Ensure.NotNull(info);

		ISemanticTheme theme = info.CreateInstance();
		IReadOnlyDictionary<ImGuiCol, Vector4> imgui = new ImGuiPaletteMapper().MapTheme(theme);
		IReadOnlyDictionary<SemanticColorRequest, Color> semantic = SemanticColorMapper.MakeCompletePalette(theme);

		List<Finding> findings = [];

		(double minTextContrast, string minTextPair) = CheckContrast(imgui, TextPairs, AnalysisThresholds.TextContrastAa, "Text contrast", findings);
		CheckContrast(imgui, DisabledTextPairs, AnalysisThresholds.DisabledTextFloor, "Disabled text", findings);
		(double minGlyphContrast, string minGlyphPair) = CheckContrast(imgui, GlyphPairs, AnalysisThresholds.GlyphContrast, "Glyph contrast", findings);
		(double minStateDelta, string minStatePair) = CheckDistinctness(imgui, findings);
		(double maxHueDrift, double minChromaRetention) = CheckFidelity(theme, semantic, findings);
		IReadOnlyList<SemanticMeaning> unused = CheckCoverage(theme, findings);

		ThemeMetrics metrics = new(
			minTextContrast, minTextPair,
			minGlyphContrast, minGlyphPair,
			minStateDelta, minStatePair,
			maxHueDrift, minChromaRetention,
			unused);

		return new ThemeAnalysis(info.Name, info.Family, info.IsDark, metrics, findings);
	}

	private static (double Min, string Pair) CheckContrast(
		IReadOnlyDictionary<ImGuiCol, Vector4> imgui,
		(ImGuiCol Fg, ImGuiCol Bg)[] pairs,
		double gate,
		string category,
		List<Finding> findings)
	{
		double min = double.PositiveInfinity;
		string minPair = "n/a";

		foreach ((ImGuiCol fg, ImGuiCol bg) in pairs)
		{
			if (ToColor(imgui, fg) is not Color fgColor || ToColor(imgui, bg) is not Color bgColor)
			{
				continue;
			}

			double ratio = fgColor.ContrastRatio(bgColor);
			string pair = $"{fg}/{bg}";
			if (ratio < min)
			{
				min = ratio;
				minPair = pair;
			}

			if (ratio < gate)
			{
				findings.Add(new Finding(
					category,
					CheckStatus.Fail,
					$"{pair} is {Num(ratio)}:1 (needs {Num(gate)}:1)"));
			}
		}

		return (double.IsPositiveInfinity(min) ? 0.0 : min, minPair);
	}

	private static (double MinDelta, string Pair) CheckDistinctness(
		IReadOnlyDictionary<ImGuiCol, Vector4> imgui,
		List<Finding> findings)
	{
		double min = double.PositiveInfinity;
		string minPair = "n/a";

		foreach ((ImGuiCol a, ImGuiCol b) in StatePairs)
		{
			if (ToColor(imgui, a) is not Color ca || ToColor(imgui, b) is not Color cb)
			{
				continue;
			}

			double delta = Math.Abs(ca.ToOklab().L - cb.ToOklab().L);
			string pair = $"{a}->{b}";
			if (delta < min)
			{
				min = delta;
				minPair = pair;
			}

			if (delta < AnalysisThresholds.MinStateLightnessDelta)
			{
				findings.Add(new Finding(
					"Interaction distinctness",
					CheckStatus.Fail,
					$"{pair} differ by only {Num(delta, "0.000")} Oklab L (needs {Num(AnalysisThresholds.MinStateLightnessDelta, "0.000")})"));
			}
		}

		return (double.IsPositiveInfinity(min) ? 0.0 : min, minPair);
	}

	private static (double MaxHueDrift, double MinChromaRetention) CheckFidelity(
		ISemanticTheme theme,
		IReadOnlyDictionary<SemanticColorRequest, Color> semantic,
		List<Finding> findings)
	{
		double maxHueDrift = 0.0;
		double minChromaRetention = double.PositiveInfinity;

		foreach ((SemanticMeaning meaning, Priority[] priorities) in AccentUsage)
		{
			if (!theme.SemanticMapping.TryGetValue(meaning, out Collection<Color>? sourceColors) || sourceColors.Count == 0)
			{
				continue;
			}

			List<Oklch> sourceHued = [.. sourceColors
				.Select(c => c.ToOklch())
				.Where(o => o.C > AnalysisThresholds.MeaningfulChroma)];
			double maxSourceChroma = sourceColors.Max(c => c.ToOklch().C);

			foreach (Priority priority in priorities)
			{
				if (!semantic.TryGetValue(new SemanticColorRequest(meaning, priority), out Color derived))
				{
					continue;
				}

				Oklch d = derived.ToOklch();

				if (maxSourceChroma > AnalysisThresholds.MeaningfulChroma)
				{
					double retention = d.C / maxSourceChroma;
					if (retention < minChromaRetention)
					{
						minChromaRetention = retention;
					}

					if (retention < AnalysisThresholds.MinAccentChromaRetention)
					{
						findings.Add(new Finding(
							"Canonical fidelity",
							CheckStatus.Warn,
							$"{meaning} {priority} keeps only {Num(retention * 100.0, "0")}% of source chroma"));
					}
				}

				if (d.C > AnalysisThresholds.MeaningfulChroma && sourceHued.Count > 0)
				{
					double drift = sourceHued.Min(s => HueDelta(d.H, s.H));
					if (drift > maxHueDrift)
					{
						maxHueDrift = drift;
					}

					if (drift > AnalysisThresholds.MaxAccentHueDriftDegrees)
					{
						findings.Add(new Finding(
							"Canonical fidelity",
							CheckStatus.Warn,
							$"{meaning} {priority} hue drifts {Num(drift, "0")}deg from source (max {Num(AnalysisThresholds.MaxAccentHueDriftDegrees, "0")}deg)"));
					}
				}
			}
		}

		return (maxHueDrift, double.IsPositiveInfinity(minChromaRetention) ? 1.0 : minChromaRetention);
	}

	private static IReadOnlyList<SemanticMeaning> CheckCoverage(ISemanticTheme theme, List<Finding> findings)
	{
		HashSet<SemanticMeaning> provided = [.. theme.SemanticMapping.Keys];

		foreach (SemanticMeaning required in MapperUsedMeanings)
		{
			if (!provided.Contains(required))
			{
				// Neutral and Primary underpin backgrounds and accents; Alternate only tints plots/selection.
				CheckStatus severity = required == SemanticMeaning.Alternate ? CheckStatus.Warn : CheckStatus.Fail;
				findings.Add(new Finding(
					"Semantic coverage",
					severity,
					$"theme does not define {required}, which the ImGui mapper needs; those slots fall back to defaults"));
			}
		}

		return [.. provided.Where(m => !MapperUsedMeanings.Contains(m)).OrderBy(m => m.ToString(), StringComparer.Ordinal)];
	}

	private static Color? ToColor(IReadOnlyDictionary<ImGuiCol, Vector4> imgui, ImGuiCol slot) =>
		imgui.TryGetValue(slot, out Vector4 v) ? Color.FromSrgb(v.X, v.Y, v.Z, v.W) : null;

	private static double HueDelta(double a, double b)
	{
		double d = Math.Abs(a - b) % 360.0;
		return d > 180.0 ? 360.0 - d : d;
	}

	private static string Num(double value, string format = "0.0") =>
		value.ToString(format, CultureInfo.InvariantCulture);
}
