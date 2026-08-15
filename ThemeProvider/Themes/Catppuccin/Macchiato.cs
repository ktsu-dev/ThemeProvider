// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Macchiato color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Macchiato : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#cad3f5", "#181926"], // Text, Crust
		Primary = "#8aadf4", // Blue
		Alternate = "#f5bde6", // Pink
		Success = "#a6da95", // Green
		CallToAction = "#a6da95", // Green
		Information = "#7dc4e4", // Sapphire
		Caution = "#ee99a0", // Maroon
		Warning = "#f5a97f", // Peach
		Error = "#ed8796", // Red
		Failure = "#ed8796", // Red
		Debug = "#c6a0f6", // Mauve
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Catppuccin Macchiato is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
