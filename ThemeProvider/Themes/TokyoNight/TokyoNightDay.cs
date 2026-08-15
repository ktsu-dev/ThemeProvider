// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Tokyo Night Day color palette with official hex values.
/// The light variant of Tokyo Night with bright backgrounds and dark text.
/// Based on the Tokyo Night theme by enkia.
/// </summary>
public class TokyoNightDay : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#3760bf", "#e9e9ed"], // Fg0, BgAlt
		Primary = "#2e7de9", // Blue
		Alternate = "#9854f1", // Purple
		Success = "#587539", // Green
		CallToAction = "#007197", // Cyan
		Information = "#2e7de9", // Blue
		Caution = "#8c6c3e", // Yellow
		Warning = "#b15c00", // Orange
		Error = "#f52a65", // Red
		Failure = "#f52a65", // Red
		Debug = "#9854f1", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Tokyo Night Day is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
