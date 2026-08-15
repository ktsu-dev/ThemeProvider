// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Dark Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#272e33).
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDarkHard : ISemanticTheme
{
	// Colors shared by several meanings are named once, after the palette entry they come from.
	private const string Red = "#e67e80";

	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#d3c6aa", "#1e2326"], // Fg, BgDim
		Primary = "#a7c080", // Green
		Alternate = "#e69875", // Orange
		Success = "#7fbbb3", // Blue
		CallToAction = "#83c092", // Aqua
		Information = "#d699b6", // Purple
		Caution = "#dbbc7f", // Yellow
		Warning = Red,
		Error = Red,
		Failure = Red,
		Debug = "#d699b6", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Dark Hard is a dark theme with high contrast
	/// </summary>
	public bool IsDarkTheme => true;
}
