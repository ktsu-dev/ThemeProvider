// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Dayfox color palette with official hex values.
/// A soft light theme with warm accents.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Dayfox : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#1d344f", "#efeae6"], // Fg0, BgAlt
		Primary = "#2848a9", // Blue
		Alternate = "#955f20", // Orange
		Success = "#396847", // Green
		CallToAction = "#287980", // Cyan
		Information = "#2848a9", // Blue
		Caution = "#986936", // Yellow
		Warning = "#955f20", // Orange
		Error = "#a5222f", // Red
		Failure = "#a5222f", // Red
		Debug = "#7847bd", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Dayfox is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
