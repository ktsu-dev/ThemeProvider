// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Gruvbox Light color palette with official hex values.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxLight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light colors (backgrounds in light theme)
	public static readonly Color LightHard = Color.FromHex("#f9f5d7");
	public static readonly Color Light0 = Color.FromHex("#fbf1c7");
	public static readonly Color Light0Soft = Color.FromHex("#f2e5bc");
	public static readonly Color Light1 = Color.FromHex("#ebdbb2");
	public static readonly Color Light2 = Color.FromHex("#d5c4a1");
	public static readonly Color Light3 = Color.FromHex("#bdae93");
	public static readonly Color Light4 = Color.FromHex("#a89984");

	// Dark colors (foregrounds in light theme)
	public static readonly Color Dark0Hard = Color.FromHex("#1d2021");
	public static readonly Color Dark0 = Color.FromHex("#282828");
	public static readonly Color Dark0Soft = Color.FromHex("#32302f");
	public static readonly Color Dark1 = Color.FromHex("#3c3836");
	public static readonly Color Dark2 = Color.FromHex("#504945");
	public static readonly Color Dark3 = Color.FromHex("#665c54");
	public static readonly Color Dark4 = Color.FromHex("#7c6f64");

	// Faded colors for light theme
	public static readonly Color FadedRed = Color.FromHex("#cc241d");
	public static readonly Color FadedGreen = Color.FromHex("#98971a");
	public static readonly Color FadedYellow = Color.FromHex("#d79921");
	public static readonly Color FadedBlue = Color.FromHex("#458588");
	public static readonly Color FadedPurple = Color.FromHex("#b16286");
	public static readonly Color FadedAqua = Color.FromHex("#689d6a");
	public static readonly Color FadedOrange = Color.FromHex("#d65d0e");

	// Neutral colors
	public static readonly Color Gray = Color.FromHex("#928374");

	public static Collection<Color> Neutrals =>
	[
		Dark0Hard,   // Darkest (for text in light theme)
		LightHard,   // Lightest (for backgrounds in light theme)
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [FadedOrange],
		[SemanticMeaning.Alternate] = [FadedPurple],
		[SemanticMeaning.Success] = [FadedGreen],
		[SemanticMeaning.CallToAction] = [FadedGreen],
		[SemanticMeaning.Information] = [FadedAqua],
		[SemanticMeaning.Caution] = [FadedBlue],
		[SemanticMeaning.Warning] = [FadedYellow],
		[SemanticMeaning.Error] = [FadedRed],
		[SemanticMeaning.Failure] = [FadedRed],
		[SemanticMeaning.Debug] = [FadedPurple]
	};

	/// <summary>
	/// Gruvbox Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
