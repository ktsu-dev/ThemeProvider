// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Kanagawa Lotus color palette with official hex values.
/// A light variant inspired by lotus flowers and zen gardens.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaLotus : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#545464", "#f7f4dd"], // Fg0, BgAlt
		Primary = "#4d699b", // CrystalBlue
		Alternate = "#b35b79", // SakuraPink
		Success = "#6f894e", // SummerGreen
		CallToAction = "#5e857a", // SpringBlue
		Information = "#7e9fb8", // IceBlue
		Caution = "#77713f", // AutumnYellow
		Warning = "#b98f56", // BoatYellow2
		Error = "#cc5d73", // WaveRed
		Failure = "#cc5d73", // WaveRed
		Debug = "#b35b79", // SakuraPink
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Kanagawa Lotus is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
