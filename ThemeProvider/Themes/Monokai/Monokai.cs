// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Monokai;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the classic Monokai color palette with exact hex values.
/// Based on the original Monokai theme by Wimer Hazenberg.
/// </summary>
public class Monokai : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#f8f8f2", "#272822"], // Foreground, Background
		Primary = "#66d9ef", // Blue
		Alternate = "#ae81ff", // Purple
		Success = "#a6e22e", // Green
		CallToAction = "#a6e22e", // Green
		Information = "#a1efe4", // Aqua
		Caution = "#fd971f", // Orange
		Warning = "#f4bf75", // Yellow
		Error = "#f92672", // Red
		Failure = "#f92672", // Red
		Debug = "#ae81ff", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Monokai is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
