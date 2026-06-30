// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Tokyo Night Storm color palette with official hex values.
/// Storm variant uses slightly lighter backgrounds for reduced contrast.
/// Based on the Tokyo Night theme by enkia.
/// </summary>
public class TokyoNightStorm : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors (Storm variant uses #24283b instead of #1a1b26)
	public static readonly Color Background = Color.FromHex("#24283b");
	public static readonly Color BgAlt = Color.FromHex("#1f2335");
	public static readonly Color Bg0 = Color.FromHex("#24283b");
	public static readonly Color Bg1 = Color.FromHex("#2d3348");
	public static readonly Color Bg2 = Color.FromHex("#414868");
	public static readonly Color Sel0 = Color.FromHex("#2d3f76");
	public static readonly Color Sel1 = Color.FromHex("#364a82");

	// Foreground colors
	public static readonly Color Fg0 = Color.FromHex("#c0caf5");
	public static readonly Color Fg1 = Color.FromHex("#a9b1d6");
	public static readonly Color Fg2 = Color.FromHex("#9aa5ce");
	public static readonly Color Comment = Color.FromHex("#565f89");

	// Tokyo Night core colors
	public static readonly Color Blue = Color.FromHex("#7aa2f7");
	public static readonly Color Purple = Color.FromHex("#bb9af7");
	public static readonly Color Cyan = Color.FromHex("#7dcfff");
	public static readonly Color Green = Color.FromHex("#9ece6a");
	public static readonly Color Teal = Color.FromHex("#1abc9c");
	public static readonly Color Yellow = Color.FromHex("#e0af68");
	public static readonly Color Orange = Color.FromHex("#ff9e64");
	public static readonly Color Red = Color.FromHex("#f7768e");
	public static readonly Color Magenta = Color.FromHex("#bb9af7");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
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
	/// Tokyo Night Storm is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
