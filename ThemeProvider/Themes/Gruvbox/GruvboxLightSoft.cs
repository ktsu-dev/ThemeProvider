// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Gruvbox Light Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds for reduced eye strain.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxLightSoft : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#1d2021", "#f2e5bc"], // Dark0Hard, Light0Soft
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
	/// Gruvbox Light Soft is a light theme with reduced contrast
	/// </summary>
	public bool IsDarkTheme => false;
}
