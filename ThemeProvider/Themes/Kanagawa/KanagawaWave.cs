// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Kanagawa Wave color palette with official hex values.
/// A dark theme inspired by Japanese paintings with warm, muted tones.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaWave : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#dcd7ba", "#16161d"], // Fg0, BgAlt
		Primary = "#7e9cd8", // CrystalBlue
		Alternate = "#d27e99", // SakuraPink
		Success = "#98bb6c", // SummerGreen
		CallToAction = "#7fb4ca", // SpringBlue
		Information = "#a3d4d5", // IceBlue
		Caution = "#e6c384", // AutumnYellow
		Warning = "#c0a36e", // BoatYellow2
		Error = "#e82424", // WaveRed
		Failure = "#e82424", // WaveRed
		Debug = "#d27e99", // SakuraPink
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Kanagawa Wave is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
