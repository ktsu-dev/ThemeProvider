// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Nightfox color palette with official hex values.
/// A soft dark theme with blue and orange accents.
/// Based on the Nightfox theme by EdenEast.
/// </summary>
public class Nightfox : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#cdcecf", "#131a24"], // Fg1, BgAlt
		Primary = "#719cd6", // Blue
		Alternate = "#f4a261", // Orange
		Success = "#81b29a", // Green
		CallToAction = "#63cdcf", // Cyan
		Information = "#719cd6", // Blue
		Caution = "#dbc074", // Yellow
		Warning = "#f4a261", // Orange
		Error = "#c94f6d", // Red
		Failure = "#c94f6d", // Red
		Debug = "#9d79d6", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Nightfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
