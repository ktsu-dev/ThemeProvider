// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Nightfox color palette with official hex values.
/// A soft dark theme with blue and orange accents.
/// Based on the Nightfox theme by EdenEast.
/// </summary>
public class Nightfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors
	public static readonly Color Background = Color.FromHex("#192330");
	public static readonly Color BgAlt = Color.FromHex("#131a24");
	public static readonly Color Bg0 = Color.FromHex("#192330");
	public static readonly Color Bg1 = Color.FromHex("#212e3f");
	public static readonly Color Bg2 = Color.FromHex("#29394f");
	public static readonly Color Bg3 = Color.FromHex("#39506d");
	public static readonly Color Sel0 = Color.FromHex("#2b3b51");
	public static readonly Color Sel1 = Color.FromHex("#3c5372");

	// Foreground colors
	public static readonly Color Fg0 = Color.FromHex("#d6d6d7");
	public static readonly Color Fg1 = Color.FromHex("#cdcecf");
	public static readonly Color Fg2 = Color.FromHex("#aeafb0");
	public static readonly Color Fg3 = Color.FromHex("#71839b");
	public static readonly Color Comment = Color.FromHex("#738091");

	// Accent colors
	public static readonly Color Red = Color.FromHex("#c94f6d");
	public static readonly Color Orange = Color.FromHex("#f4a261");
	public static readonly Color Yellow = Color.FromHex("#dbc074");
	public static readonly Color Green = Color.FromHex("#81b29a");
	public static readonly Color Blue = Color.FromHex("#719cd6");
	public static readonly Color Cyan = Color.FromHex("#63cdcf");
	public static readonly Color Magenta = Color.FromHex("#9d79d6");
	public static readonly Color Pink = Color.FromHex("#d67ad2");

	public static Collection<Color> Neutrals =>
	[
		Fg1,         // Lightest
		BgAlt,       // Darkest
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
	/// Nightfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
