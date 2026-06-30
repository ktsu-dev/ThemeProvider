// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Test;

using ktsu.Semantics.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies WCAG contrast-ratio computations and the <see cref="Color.AdjustForContrast"/> helper.
/// </summary>
[TestClass]
public class AccessibilityTests
{
	/// <summary>
	/// Pure black against pure white must yield a contrast ratio of exactly 21, the WCAG maximum.
	/// This pins the <see cref="Color.ContrastRatio"/> formula: (lighter + 0.05) / (darker + 0.05).
	/// </summary>
	[TestMethod]
	public void BlackVsWhiteContrastRatio_IsExactly21()
	{
		Color black = Color.FromHex("#000000");
		Color white = Color.FromHex("#ffffff");
		double ratio = black.ContrastRatio(white);
		Assert.AreEqual(21.0, ratio, 1e-6, "Black vs white contrast ratio must be 21");
	}

	/// <summary>
	/// A near-zero-contrast pair (two very dark Catppuccin Mocha colors) must reach at least
	/// WCAG AA (4.5:1) after <see cref="Color.AdjustForContrast"/> targets AA.
	/// Background is Mocha Base (#1e1e2e); foreground is Mocha Surface0 (#313244).
	/// </summary>
	[TestMethod]
	public void LowContrastMochaPair_AdjustedForAA_ReachesAtLeastAA()
	{
		Color background = Color.FromHex("#1e1e2e"); // Mocha.Base — very dark
		Color foreground = Color.FromHex("#313244"); // Mocha.Surface0 — dark, low contrast

		Color adjusted = foreground.AdjustForContrast(background, AccessibilityLevel.AA);
		AccessibilityLevel level = adjusted.AccessibilityLevelAgainst(background);

		Assert.IsTrue(
			level >= AccessibilityLevel.AA,
			$"Expected at least AA after adjustment but got {level}. ContrastRatio={adjusted.ContrastRatio(background):F2}");
	}
}
