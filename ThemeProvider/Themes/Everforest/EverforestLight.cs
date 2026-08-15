// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Light color palette with official hex values.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLight : ISemanticTheme
{
	// Colors shared by several meanings are named once, after the palette entry they come from.
	private const string Red = "#f85552";

	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#5c6a72", "#f3efda"], // Fg, BgDim
		Primary = "#8da101", // Green
		Alternate = "#f57d26", // Orange
		Success = "#3a94c5", // Blue
		CallToAction = "#35a77c", // Aqua
		Information = "#df69ba", // Purple
		Caution = "#dfa000", // Yellow
		Warning = Red,
		Error = Red,
		Failure = Red,
		Debug = "#df69ba", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
