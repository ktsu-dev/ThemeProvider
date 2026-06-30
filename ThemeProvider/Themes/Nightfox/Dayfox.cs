// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Dayfox color palette with official hex values.
/// A soft light theme with warm accents.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Dayfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light background colors
	public static readonly Color Background = Color.FromHex("#f6f2ee");
	public static readonly Color BgAlt = Color.FromHex("#efeae6");
	public static readonly Color Bg0 = Color.FromHex("#f6f2ee");
	public static readonly Color Bg1 = Color.FromHex("#f0ebe7");
	public static readonly Color Bg2 = Color.FromHex("#e9e4e0");
	public static readonly Color Bg3 = Color.FromHex("#e1dcd8");
	public static readonly Color Sel0 = Color.FromHex("#e7ddd9");
	public static readonly Color Sel1 = Color.FromHex("#d6ccc7");

	// Dark foreground colors
	public static readonly Color Fg0 = Color.FromHex("#1d344f");
	public static readonly Color Fg1 = Color.FromHex("#24394f");
	public static readonly Color Fg2 = Color.FromHex("#3c5d6f");
	public static readonly Color Fg3 = Color.FromHex("#6b7d86");
	public static readonly Color Comment = Color.FromHex("#7d7a68");

	// Accent colors for light theme
	public static readonly Color Red = Color.FromHex("#a5222f");
	public static readonly Color Orange = Color.FromHex("#955f20");
	public static readonly Color Yellow = Color.FromHex("#986936");
	public static readonly Color Green = Color.FromHex("#396847");
	public static readonly Color Blue = Color.FromHex("#2848a9");
	public static readonly Color Cyan = Color.FromHex("#287980");
	public static readonly Color Magenta = Color.FromHex("#7847bd");
	public static readonly Color Pink = Color.FromHex("#b9477c");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Orange],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Cyan],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Magenta]
	};

	/// <summary>
	/// Dayfox is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
