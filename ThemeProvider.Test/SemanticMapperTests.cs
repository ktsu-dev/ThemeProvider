// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Test;

using System;
using System.Collections.Generic;
using ktsu.Semantics.Color;
using ktsu.ThemeProvider;
using ktsu.ThemeProvider.Themes.Catppuccin;
using ktsu.ThemeProvider.Themes.Gruvbox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies in-gamut and lightness-ordering guarantees for <see cref="SemanticColorMapper.MakeCompletePalette"/>.
/// </summary>
[TestClass]
public class SemanticMapperTests
{
	/// <summary>
	/// Every color produced by the mapper for the Catppuccin Mocha theme must have
	/// linear-RGB channels within [0, 1].
	/// </summary>
	[TestMethod]
	public void MochaPalette_AllColorsInGamut()
	{
		Mocha theme = new();
		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);

		foreach (KeyValuePair<SemanticColorRequest, Color> kvp in palette)
		{
			Color c = kvp.Value;
			string label = kvp.Key.ToString();
			Assert.IsTrue(c.R is >= 0.0 and <= 1.0, $"Red out of gamut ({c.R:F4}) for {label}");
			Assert.IsTrue(c.G is >= 0.0 and <= 1.0, $"Green out of gamut ({c.G:F4}) for {label}");
			Assert.IsTrue(c.B is >= 0.0 and <= 1.0, $"Blue out of gamut ({c.B:F4}) for {label}");
		}
	}

	/// <summary>
	/// Every color produced by the mapper for the Gruvbox Dark theme must have
	/// linear-RGB channels within [0, 1].
	/// </summary>
	[TestMethod]
	public void GruvboxDarkPalette_AllColorsInGamut()
	{
		GruvboxDark theme = new();
		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);

		foreach (KeyValuePair<SemanticColorRequest, Color> kvp in palette)
		{
			Color c = kvp.Value;
			string label = kvp.Key.ToString();
			Assert.IsTrue(c.R is >= 0.0 and <= 1.0, $"Red out of gamut ({c.R:F4}) for {label}");
			Assert.IsTrue(c.G is >= 0.0 and <= 1.0, $"Green out of gamut ({c.G:F4}) for {label}");
			Assert.IsTrue(c.B is >= 0.0 and <= 1.0, $"Blue out of gamut ({c.B:F4}) for {label}");
		}
	}

	/// <summary>
	/// For each semantic meaning in the Catppuccin Mocha dark theme, Oklab lightness must be
	/// non-decreasing as Priority rises from VeryLow to VeryHigh.
	/// <para>
	/// The mapper targets a strictly increasing lightness per priority level; floating-point
	/// residuals from Oklab-to-linear round-trips may introduce tiny deviations (~1e-12),
	/// so we allow an epsilon of 1e-9 before calling the invariant violated.
	/// </para>
	/// </summary>
	[TestMethod]
	public void MochaPalette_PriorityLightnessNonDecreasingForDarkTheme()
	{
		Mocha theme = new();
		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);

		// Enum.GetValues returns values in ascending numeric order (VeryLow=0 … VeryHigh=6).
		Priority[] priorities = Enum.GetValues<Priority>();

		foreach (SemanticMeaning meaning in theme.SemanticMapping.Keys)
		{
			double previousL = double.MinValue;
			foreach (Priority priority in priorities)
			{
				SemanticColorRequest request = new(meaning, priority);
				if (palette.TryGetValue(request, out Color color))
				{
					double l = color.ToOklab().L;
					Assert.IsTrue(
						l >= previousL - 1e-9,
						$"Lightness decreased ({previousL:F6} → {l:F6}) for {meaning} at {priority}");
					previousL = l;
				}
			}
		}
	}
}
