// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Tokyo Night color palette with official hex values.
/// Based on the popular Tokyo Night theme by Enkia.
/// </summary>
public class TokyoNight : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#c0caf5", "#1a1b26"], // Foreground, Background
		Primary = "#7aa2f7", // Blue
		Alternate = "#bb9af7", // Magenta
		Success = "#9ece6a", // Green
		CallToAction = "#9ece6a", // Green
		Information = "#7dcfff", // Cyan
		Caution = "#ff9e64", // Orange
		Warning = "#e0af68", // Yellow
		Error = "#f7768e", // Red
		Failure = "#db4b4b", // Red1
		Debug = "#9d7cd8", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Tokyo Night is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
