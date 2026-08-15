// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Duskfox color palette with official hex values.
/// A muted dark theme with desaturated warm tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Duskfox : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#e0def4", "#1a1826"], // Fg0, BgAlt
		Primary = "#9ccfd8", // Blue
		Alternate = "#c4a7e7", // Magenta
		Success = "#a3be8c", // Green
		CallToAction = "#9ccfd8", // Cyan
		Information = "#9ccfd8", // Blue
		Caution = "#f6c177", // Yellow
		Warning = "#ea9a97", // Orange
		Error = "#eb6f92", // Red
		Failure = "#eb6f92", // Red
		Debug = "#f5c2e7", // Pink
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Duskfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
