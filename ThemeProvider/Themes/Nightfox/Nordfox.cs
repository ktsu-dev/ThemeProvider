// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Nordfox color palette with official hex values.
/// A Nord-inspired variant with cool blue tones and arctic aesthetics.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Nordfox : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#cdcecf", "#232831"], // Fg0, BgAlt
		Primary = "#81a1c1", // Blue
		Alternate = "#88c0d0", // Cyan
		Success = "#a3be8c", // Green
		CallToAction = "#88c0d0", // Cyan
		Information = "#81a1c1", // Blue
		Caution = "#ebcb8b", // Yellow
		Warning = "#d08770", // Orange
		Error = "#bf616a", // Red
		Failure = "#bf616a", // Red
		Debug = "#b48ead", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Nordfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
