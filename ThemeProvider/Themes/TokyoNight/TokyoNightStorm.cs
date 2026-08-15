// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Tokyo Night Storm color palette with official hex values.
/// Storm variant uses slightly lighter backgrounds for reduced contrast.
/// Based on the Tokyo Night theme by enkia.
/// </summary>
public class TokyoNightStorm : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#c0caf5", "#1f2335"], // Fg0, BgAlt
		Primary = "#7aa2f7", // Blue
		Alternate = "#bb9af7", // Purple
		Success = "#9ece6a", // Green
		CallToAction = "#7dcfff", // Cyan
		Information = "#7aa2f7", // Blue
		Caution = "#e0af68", // Yellow
		Warning = "#ff9e64", // Orange
		Error = "#f7768e", // Red
		Failure = "#f7768e", // Red
		Debug = "#bb9af7", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Tokyo Night Storm is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
