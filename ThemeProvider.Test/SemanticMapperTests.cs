// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System;
using System.Collections.Generic;
using ktsu.Semantics.Color;
using ktsu.ThemeProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies the priority-to-lightness contract of <see cref="SemanticColorMapper.MakeCompletePalette"/>.
/// <para>
/// In-gamut guarantees are asserted across the whole catalog by
/// <see cref="SemanticColorMapperEdgeCaseTests.MakeCompletePalette_ForEveryTheme_StaysInGamut"/>;
/// this class covers the ordering guarantee instead.
/// </para>
/// </summary>
[TestClass]
public class SemanticMapperTests
{
	/// <summary>
	/// The mapper targets a strictly increasing lightness per priority level, but Oklab-to-linear
	/// round-trips leave floating-point residuals (~1e-12), so an epsilon guards the comparison.
	/// </summary>
	private const double LightnessEpsilon = 1e-9;

	/// <summary>
	/// Across every registered theme, Oklab lightness must move monotonically as Priority rises
	/// from VeryLow to VeryHigh — upward for dark themes, downward for light ones. This is the
	/// visual-hierarchy guarantee the whole priority system rests on.
	/// </summary>
	[TestMethod]
	public void CompletePalette_LightnessIsMonotonicInPriority()
	{
		// Enum.GetValues returns values in ascending numeric order (VeryLow=0 … VeryHigh=6).
		Priority[] priorities = Enum.GetValues<Priority>();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			ISemanticTheme theme = info.CreateInstance();
			IReadOnlyDictionary<SemanticColorRequest, Color> palette =
				SemanticColorMapper.MakeCompletePalette(theme);

			foreach (SemanticMeaning meaning in theme.SemanticMapping.Keys)
			{
				AssertMonotonic(info.Name, theme.IsDarkTheme, meaning, priorities, palette);
			}
		}
	}

	private static void AssertMonotonic(
		string themeName,
		bool isDarkTheme,
		SemanticMeaning meaning,
		Priority[] priorities,
		IReadOnlyDictionary<SemanticColorRequest, Color> palette)
	{
		double? previous = null;

		foreach (Priority priority in priorities)
		{
			if (!palette.TryGetValue(new SemanticColorRequest(meaning, priority), out Color color))
			{
				continue;
			}

			double lightness = color.ToOklab().L;

			if (previous is { } previousLightness)
			{
				string detail =
					$"{themeName}: lightness moved the wrong way ({previousLightness:F6} → {lightness:F6}) " +
					$"for {meaning} at {priority}";

				if (isDarkTheme)
				{
					Assert.IsGreaterThanOrEqualTo(previousLightness - LightnessEpsilon, lightness, detail);
				}
				else
				{
					Assert.IsLessThanOrEqualTo(previousLightness + LightnessEpsilon, lightness, detail);
				}
			}

			previous = lightness;
		}
	}

	/// <summary>
	/// The lowest and highest priority of a meaning must actually differ, otherwise the ramp is flat
	/// and priority carries no visual information.
	/// </summary>
	[TestMethod]
	public void CompletePalette_PriorityRampSpansARange()
	{
		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			ISemanticTheme theme = info.CreateInstance();
			IReadOnlyDictionary<SemanticColorRequest, Color> palette =
				SemanticColorMapper.MakeCompletePalette(theme);

			double lowest = palette[new(SemanticMeaning.Neutral, Priority.VeryLow)].ToOklab().L;
			double highest = palette[new(SemanticMeaning.Neutral, Priority.VeryHigh)].ToOklab().L;

			Assert.AreNotEqual(
				lowest,
				highest,
				LightnessEpsilon,
				$"{info.Name}: neutral ramp is flat across priorities");
		}
	}
}
