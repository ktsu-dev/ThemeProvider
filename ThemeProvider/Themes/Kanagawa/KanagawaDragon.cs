// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Kanagawa Dragon color palette with official hex values.
/// A darker, more intense variant inspired by Japanese dragons and ink paintings.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaDragon : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#c5c9c5", "#0d0c0c"], // Fg0, Background
		Primary = "#8ba4b0", // CrystalBlue
		Alternate = "#c4746e", // SakuraPink
		Success = "#8a9a7b", // SummerGreen
		CallToAction = "#7fb4ca", // SpringBlue
		Information = "#9cabca", // IceBlue
		Caution = "#c4b28a", // AutumnYellow
		Warning = "#b6927b", // BoatYellow2
		Error = "#c4746e", // WaveRed
		Failure = "#c4746e", // WaveRed
		Debug = "#c4746e", // SakuraPink
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Kanagawa Dragon is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
