// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Carbonfox color palette with official hex values.
/// A minimalist dark variant inspired by carbon fiber and industrial design.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Carbonfox : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#f2f4f8", "#0c0c0c"], // Fg0, BgAlt
		Primary = "#78a9ff", // Blue
		Alternate = "#33b1ff", // Cyan
		Success = "#25be6a", // Green
		CallToAction = "#33b1ff", // Cyan
		Information = "#78a9ff", // Blue
		Caution = "#08bdba", // Yellow
		Warning = "#3ddbd9", // Orange
		Error = "#ee5396", // Red
		Failure = "#ee5396", // Red
		Debug = "#be95ff", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Carbonfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
