// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Terafox color palette with official hex values.
/// An earthy, terra-inspired variant with warm brown and green tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Terafox : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#fbebd3", "#0f1c1e"], // Fg0, BgAlt
		Primary = "#7aa4a1", // Green
		Alternate = "#ffa500", // Orange
		Success = "#7aa4a1", // Green
		CallToAction = "#a1cdd8", // Cyan
		Information = "#5a93aa", // Blue
		Caution = "#fdb292", // Yellow
		Warning = "#ffa500", // Orange
		Error = "#e85c51", // Red
		Failure = "#e85c51", // Red
		Debug = "#ad5c7c", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Terafox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
