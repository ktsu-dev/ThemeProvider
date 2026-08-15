// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Gruvbox Light Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds for better readability.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxLightHard : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#1d2021", "#f9f5d7"], // Dark0Hard, LightHard
		Primary = "#d65d0e", // FadedOrange
		Alternate = "#b16286", // FadedPurple
		Success = "#98971a", // FadedGreen
		CallToAction = "#98971a", // FadedGreen
		Information = "#689d6a", // FadedAqua
		Caution = "#458588", // FadedBlue
		Warning = "#d79921", // FadedYellow
		Error = "#cc241d", // FadedRed
		Failure = "#cc241d", // FadedRed
		Debug = "#b16286", // FadedPurple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Gruvbox Light Hard is a light theme with high contrast
	/// </summary>
	public bool IsDarkTheme => false;
}
