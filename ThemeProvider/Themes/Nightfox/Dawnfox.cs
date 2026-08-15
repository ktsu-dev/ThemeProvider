// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Dawnfox color palette with official hex values.
/// A dawn-inspired light variant with soft, warm morning tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Dawnfox : ISemanticTheme
{
	// Colors shared by several meanings are named once, after the palette entry they come from.
	private const string Blue = "#56949f";

	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#575279", "#f4ede4"], // Fg0, BgAlt
		Primary = Blue,
		Alternate = "#d685af", // Pink
		Success = "#286983", // Green
		CallToAction = Blue,
		Information = Blue,
		Caution = "#d7827e", // Yellow
		Warning = "#ea9d34", // Orange
		Error = "#b4637a", // Red
		Failure = "#b4637a", // Red
		Debug = "#907aa9", // Magenta
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Dawnfox is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
