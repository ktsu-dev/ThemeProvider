// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Duskfox color palette with official hex values.
/// A muted dark theme with desaturated warm tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Duskfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors
	public static readonly Color Background = Color.FromHex("#232136");
	public static readonly Color BgAlt = Color.FromHex("#1a1826");
	public static readonly Color Bg0 = Color.FromHex("#232136");
	public static readonly Color Bg1 = Color.FromHex("#2d2a45");
	public static readonly Color Bg2 = Color.FromHex("#373354");
	public static readonly Color Bg3 = Color.FromHex("#47407d");
	public static readonly Color Sel0 = Color.FromHex("#2a2d3a");
	public static readonly Color Sel1 = Color.FromHex("#3c3a52");

	// Foreground colors
	public static readonly Color Fg0 = Color.FromHex("#e0def4");
	public static readonly Color Fg1 = Color.FromHex("#cdcbe0");
	public static readonly Color Fg2 = Color.FromHex("#aeafc7");
	public static readonly Color Fg3 = Color.FromHex("#6e6a86");
	public static readonly Color Comment = Color.FromHex("#6e6a86");

	// Muted accent colors
	public static readonly Color Red = Color.FromHex("#eb6f92");
	public static readonly Color Orange = Color.FromHex("#ea9a97");
	public static readonly Color Yellow = Color.FromHex("#f6c177");
	public static readonly Color Green = Color.FromHex("#a3be8c");
	public static readonly Color Blue = Color.FromHex("#9ccfd8");
	public static readonly Color Cyan = Color.FromHex("#9ccfd8");
	public static readonly Color Magenta = Color.FromHex("#c4a7e7");
	public static readonly Color Pink = Color.FromHex("#f5c2e7");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Magenta],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Cyan],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Pink]
	};

	/// <summary>
	/// Duskfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
