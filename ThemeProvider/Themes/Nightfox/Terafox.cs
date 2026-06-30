// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Terafox color palette with official hex values.
/// An earthy, terra-inspired variant with warm brown and green tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Terafox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Earth-toned background colors
	public static readonly Color Background = Color.FromHex("#152528");
	public static readonly Color BgAlt = Color.FromHex("#0f1c1e");
	public static readonly Color Bg0 = Color.FromHex("#152528");
	public static readonly Color Bg1 = Color.FromHex("#1d3337");
	public static readonly Color Bg2 = Color.FromHex("#254147");
	public static readonly Color Bg3 = Color.FromHex("#2f4f56");
	public static readonly Color Sel0 = Color.FromHex("#293e40");
	public static readonly Color Sel1 = Color.FromHex("#3a5458");

	// Natural foreground colors
	public static readonly Color Fg0 = Color.FromHex("#fbebd3");
	public static readonly Color Fg1 = Color.FromHex("#f6e2c7");
	public static readonly Color Fg2 = Color.FromHex("#adbcbc");
	public static readonly Color Fg3 = Color.FromHex("#7d8c8c");
	public static readonly Color Comment = Color.FromHex("#868d8d");

	// Terra accent colors
	public static readonly Color Red = Color.FromHex("#e85c51");
	public static readonly Color Orange = Color.FromHex("#ffa500");
	public static readonly Color Yellow = Color.FromHex("#fdb292");
	public static readonly Color Green = Color.FromHex("#7aa4a1");
	public static readonly Color Blue = Color.FromHex("#5a93aa");
	public static readonly Color Cyan = Color.FromHex("#a1cdd8");
	public static readonly Color Magenta = Color.FromHex("#ad5c7c");
	public static readonly Color Pink = Color.FromHex("#ff8080");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Green],
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
	/// Terafox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
