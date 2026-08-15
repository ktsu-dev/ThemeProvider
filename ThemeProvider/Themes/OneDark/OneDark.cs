// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.OneDark;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the One Dark color palette with official hex values.
/// Based on the Atom One Dark theme.
/// </summary>
public class OneDark : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#ffffff", "#181a1f"], // White, Black
		Primary = "#61afef", // Blue
		Alternate = "#c678dd", // Purple
		Success = "#98c379", // Green
		CallToAction = "#98c379", // Green
		Information = "#56b6c2", // Cyan
		Caution = "#d19a66", // Orange
		Warning = "#e5c07b", // Yellow
		Error = "#e06c75", // Red
		Failure = "#e06c75", // Red
		Debug = "#c678dd", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// One Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
