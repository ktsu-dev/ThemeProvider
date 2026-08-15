// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Frappe color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Frappe : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#c6d0f5", "#232634"], // Text, Crust
		Primary = "#8caaee", // Blue
		Alternate = "#f4b8e4", // Pink
		Success = "#a6d189", // Green
		CallToAction = "#a6d189", // Green
		Information = "#85c1dc", // Sapphire
		Caution = "#ea999c", // Maroon
		Warning = "#ef9f76", // Peach
		Error = "#e78284", // Red
		Failure = "#e78284", // Red
		Debug = "#ca9ee6", // Mauve
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Catppuccin Frappe is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
