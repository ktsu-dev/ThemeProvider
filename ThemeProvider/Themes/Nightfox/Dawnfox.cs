// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Dawnfox color palette with official hex values.
/// A dawn-inspired light variant with soft, warm morning tones.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Dawnfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dawn-inspired light backgrounds
	public static readonly Color Background = Color.FromHex("#faf4ed");
	public static readonly Color BgAlt = Color.FromHex("#f4ede4");
	public static readonly Color Bg0 = Color.FromHex("#faf4ed");
	public static readonly Color Bg1 = Color.FromHex("#f2e9de");
	public static readonly Color Bg2 = Color.FromHex("#eae0d5");
	public static readonly Color Bg3 = Color.FromHex("#d7c9bd");
	public static readonly Color Sel0 = Color.FromHex("#e9dfdb");
	public static readonly Color Sel1 = Color.FromHex("#ddd1c7");

	// Morning foreground colors
	public static readonly Color Fg0 = Color.FromHex("#575279");
	public static readonly Color Fg1 = Color.FromHex("#6e6a86");
	public static readonly Color Fg2 = Color.FromHex("#797593");
	public static readonly Color Fg3 = Color.FromHex("#9893a5");
	public static readonly Color Comment = Color.FromHex("#a8a5b8");

	// Dawn accent colors - soft pastels
	public static readonly Color Red = Color.FromHex("#b4637a");
	public static readonly Color Orange = Color.FromHex("#ea9d34");
	public static readonly Color Yellow = Color.FromHex("#d7827e");
	public static readonly Color Green = Color.FromHex("#286983");
	public static readonly Color Blue = Color.FromHex("#56949f");
	public static readonly Color Cyan = Color.FromHex("#d7827e");
	public static readonly Color Magenta = Color.FromHex("#907aa9");
	public static readonly Color Pink = Color.FromHex("#d685af");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Pink],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Blue],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Magenta]
	};

	/// <summary>
	/// Dawnfox is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
