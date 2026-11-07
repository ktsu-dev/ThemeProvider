// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Dark color palette with official hex values.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDark : ISemanticTheme
{
	// Colors shared by several meanings are named once, after the palette entry they come from.
	private const string Red = "#e67e80";
	private const string Aqua = "#83c092";

	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#d3c6aa", "#232a2e"], // Fg, BgDim
		Primary = "#a7c080", // Green
		Alternate = Aqua,
		Success = "#7fbbb3", // Blue
		CallToAction = Aqua,
		Information = "#d699b6", // Purple
		Caution = "#dbbc7f", // Yellow
		Warning = "#e69875", // Orange
		Error = Red,
		Failure = Red,
		Debug = "#d699b6", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
