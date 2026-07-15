// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Analysis;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

/// <summary>Renders a set of theme analyses as a single deterministic markdown document.</summary>
internal static class MarkdownReport
{
	/// <summary>Builds the markdown report.</summary>
	/// <param name="analyses">The per-theme results, in the order they should appear.</param>
	/// <returns>The report text.</returns>
	public static string Build(IReadOnlyList<ThemeAnalysis> analyses)
	{
		StringBuilder sb = new();

		int failed = analyses.Count(a => a.Status == CheckStatus.Fail);
		int warned = analyses.Count(a => a.Status == CheckStatus.Warn);
		int passed = analyses.Count - failed - warned;

		sb.AppendLine("# Theme palette analysis");
		sb.AppendLine();
		sb.AppendLine(Inv($"{analyses.Count} themes analyzed: {passed} pass, {warned} warn, {failed} fail."));
		sb.AppendLine();
		AppendLegend(sb);

		sb.AppendLine("## Summary");
		sb.AppendLine();
		sb.AppendLine("| Theme | Mode | Text | Glyph | State delta | Hue drift | Chroma | Status |");
		sb.AppendLine("| --- | --- | --- | --- | --- | --- | --- | --- |");
		foreach (ThemeAnalysis a in analyses)
		{
			ThemeMetrics m = a.Metrics;
			string mode = a.IsDark ? "dark" : "light";
			sb.AppendLine(Inv($"| {a.Name} | {mode} | {m.MinTextContrast:0.0}:1 | {m.MinGlyphContrast:0.0}:1 | {m.MinStateDelta:0.000} | {m.MaxHueDrift:0}deg | {m.MinChromaRetention * 100.0:0}% | {Label(a.Status)} |"));
		}

		sb.AppendLine();
		sb.AppendLine("## Findings");
		sb.AppendLine();

		IReadOnlyList<ThemeAnalysis> withFindings = [.. analyses.Where(a => a.Findings.Count > 0)];
		if (withFindings.Count == 0)
		{
			sb.AppendLine("No warnings or failures.");
		}
		else
		{
			foreach (ThemeAnalysis a in withFindings)
			{
				sb.AppendLine(Inv($"### {a.Name} — {Label(a.Status)}"));
				sb.AppendLine();
				foreach (Finding f in a.Findings.OrderByDescending(f => f.Status))
				{
					sb.AppendLine(Inv($"- **{Label(f.Status)}** [{f.Category}] {f.Message}"));
				}

				sb.AppendLine();
			}
		}

		AppendUnusedMeanings(sb, analyses);

		return sb.ToString();
	}

	private static void AppendLegend(StringBuilder sb)
	{
		sb.AppendLine("## What is measured");
		sb.AppendLine();
		sb.AppendLine(Inv($"- **Text** — lowest WCAG contrast of body text over any surface it renders on. Gate: {AnalysisThresholds.TextContrastAa:0.0}:1 (AA)."));
		sb.AppendLine(Inv($"- **Glyph** — lowest contrast of UI glyphs (checkmark). Gate: {AnalysisThresholds.GlyphContrast:0.0}:1 (WCAG 1.4.11)."));
		sb.AppendLine(Inv($"- **State delta** — smallest Oklab lightness step between adjacent interaction states (button/frame hover/active). Gate: {AnalysisThresholds.MinStateLightnessDelta:0.000}."));
		sb.AppendLine(Inv($"- **Hue drift** — largest angle an accent wanders from the theme's source palette. Gate: {AnalysisThresholds.MaxAccentHueDriftDegrees:0}deg."));
		sb.AppendLine(Inv($"- **Chroma** — smallest fraction of source accent chroma retained. Warns below {AnalysisThresholds.MinAccentChromaRetention * 100.0:0}%."));
		sb.AppendLine();
		sb.AppendLine("Disabled text is checked against an invisibility floor only, since it is meant to read as muted.");
		sb.AppendLine();
	}

	private static void AppendUnusedMeanings(StringBuilder sb, IReadOnlyList<ThemeAnalysis> analyses)
	{
		IReadOnlyList<ThemeAnalysis> withUnused = [.. analyses.Where(a => a.Metrics.UnusedMeanings.Count > 0)];
		if (withUnused.Count == 0)
		{
			return;
		}

		sb.AppendLine("## Semantic meanings defined but unused by the ImGui mapper");
		sb.AppendLine();
		sb.AppendLine("These are computed by the theme but never mapped to an ImGui slot, so status colors (success/warning/error/etc.) are unavailable to applications through the mapper.");
		sb.AppendLine();
		foreach (ThemeAnalysis a in withUnused)
		{
			string meanings = string.Join(", ", a.Metrics.UnusedMeanings.Select(m => m.ToString()));
			sb.AppendLine(Inv($"- {a.Name}: {meanings}"));
		}

		sb.AppendLine();
	}

	private static string Label(CheckStatus status) => status switch
	{
		CheckStatus.Fail => "FAIL",
		CheckStatus.Warn => "WARN",
		_ => "PASS",
	};

	private static string Inv(FormattableString text) => text.ToString(CultureInfo.InvariantCulture);
}
