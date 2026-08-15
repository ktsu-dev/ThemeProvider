// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nord;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Nord color palette with exact hex values and properties.
/// Based on the official specification: https://www.nordtheme.com/
/// </summary>
public class Nord : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#eceff4", "#2e3440"], // Nord6, Nord0
		Primary = "#88c0d0", // Nord8
		Alternate = "#b48ead", // Nord15
		Success = "#a3be8c", // Nord14
		CallToAction = "#a3be8c", // Nord14
		Information = "#5e81ac", // Nord10
		Caution = "#d08770", // Nord12
		Warning = "#ebcb8b", // Nord13
		Error = "#bf616a", // Nord11
		Failure = "#bf616a", // Nord11
		Debug = "#b48ead", // Nord15
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Nord is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
