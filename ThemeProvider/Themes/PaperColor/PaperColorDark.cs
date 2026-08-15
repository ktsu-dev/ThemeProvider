// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.PaperColor;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the PaperColor Dark color palette with official hex values.
/// A clean, minimal dark theme inspired by Google's Material Design.
/// Based on the PaperColor theme by NLKNguyen.
/// </summary>
public class PaperColorDark : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#d0d0d0", "#1c1c1c"], // Fg0, Background
		Primary = "#0087d7", // Blue
		Alternate = "#af87d7", // Purple
		Success = "#5faf00", // Green
		CallToAction = "#00afaf", // Teal
		Information = "#0087d7", // Blue
		Caution = "#ffaf00", // Yellow
		Warning = "#ff8700", // Orange
		Error = "#af005f", // Red
		Failure = "#af005f", // Red
		Debug = "#d70087", // Pink
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// PaperColor Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
