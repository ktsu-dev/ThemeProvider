// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.PaperColor;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the PaperColor Light color palette with official hex values.
/// A clean, minimal light theme inspired by Google's Material Design.
/// Based on the PaperColor theme by NLKNguyen.
/// </summary>
public class PaperColorLight : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#444444", "#ffffff"], // Fg0, BgAlt
		Primary = "#0087af", // Blue
		Alternate = "#8700af", // Purple
		Success = "#008700", // Green
		CallToAction = "#00af87", // Teal
		Information = "#0087af", // Blue
		Caution = "#d78700", // Yellow
		Warning = "#d75f00", // Orange
		Error = "#af0000", // Red
		Failure = "#af0000", // Red
		Debug = "#d70087", // Pink
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// PaperColor Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
