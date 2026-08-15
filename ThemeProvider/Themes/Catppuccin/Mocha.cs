// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Mocha color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Mocha : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#cdd6f4", "#11111b"], // Text, Crust
		Primary = "#89b4fa", // Blue
		Alternate = "#f5c2e7", // Pink
		Success = "#a6e3a1", // Green
		CallToAction = "#a6e3a1", // Green
		Information = "#74c7ec", // Sapphire
		Caution = "#eba0ac", // Maroon
		Warning = "#fab387", // Peach
		Error = "#f38ba8", // Red
		Failure = "#f38ba8", // Red
		Debug = "#cba6f7", // Mauve
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Catppuccin Mocha is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
