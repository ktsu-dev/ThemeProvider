// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Analysis;

/// <summary>
/// The fixed gate values every theme is measured against. Tune them here; the report and the
/// process exit code both derive from these. Contrast values are WCAG contrast ratios (1..21),
/// lightness deltas are in Oklab L (0..1), hue drift is in degrees, chroma retention is a ratio.
/// </summary>
internal static class AnalysisThresholds
{
	/// <summary>Minimum contrast for body text over the surfaces it is drawn on (WCAG AA, normal text).</summary>
	public const double TextContrastAa = 4.5;

	/// <summary>Floor for intentionally-muted disabled text: below this it reads as invisible rather than dimmed.</summary>
	public const double DisabledTextFloor = 2.0;

	/// <summary>Minimum contrast for UI glyphs such as the checkmark (WCAG 1.4.11 non-text contrast).</summary>
	public const double GlyphContrast = 3.0;

	/// <summary>Minimum Oklab lightness difference between adjacent interaction states to be perceptible.</summary>
	public const double MinStateLightnessDelta = 0.015;

	/// <summary>Maximum hue drift (degrees) an accent color may wander from the theme's source palette.</summary>
	public const double MaxAccentHueDriftDegrees = 12.0;

	/// <summary>Warn when a derived accent keeps less than this fraction of the source palette's chroma.</summary>
	public const double MinAccentChromaRetention = 0.40;

	/// <summary>Chroma below which a color is treated as neutral and its hue is not meaningful.</summary>
	public const double MeaningfulChroma = 0.02;
}
