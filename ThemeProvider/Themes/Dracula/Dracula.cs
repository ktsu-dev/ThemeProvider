// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Dracula;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Dracula color palette with exact hex values.
/// Based on the official Dracula theme specification.
/// </summary>
public class Dracula : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#f8f8f2", "#282a36"], // Foreground, Background
		Primary = "#bd93f9", // Purple
		Alternate = "#ff79c6", // Pink
		Success = "#50fa7b", // Green
		CallToAction = "#50fa7b", // Green
		Information = "#8be9fd", // Cyan
		Caution = "#ffb86c", // Orange
		Warning = "#f1fa8c", // Yellow
		Error = "#ff5555", // Red
		Failure = "#ff5555", // Red
		Debug = "#bd93f9", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Dracula is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
