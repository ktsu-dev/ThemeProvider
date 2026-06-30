// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Nordfox color palette with official hex values.
/// A Nord-inspired variant with cool blue tones and arctic aesthetics.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Nordfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Nord-inspired background colors
	public static readonly Color Background = Color.FromHex("#2e3440");
	public static readonly Color BgAlt = Color.FromHex("#232831");
	public static readonly Color Bg0 = Color.FromHex("#2e3440");
	public static readonly Color Bg1 = Color.FromHex("#3b4252");
	public static readonly Color Bg2 = Color.FromHex("#434c5e");
	public static readonly Color Bg3 = Color.FromHex("#4c566a");
	public static readonly Color Sel0 = Color.FromHex("#3e4a5b");
	public static readonly Color Sel1 = Color.FromHex("#4f6074");

	// Nord foreground colors
	public static readonly Color Fg0 = Color.FromHex("#cdcecf");
	public static readonly Color Fg1 = Color.FromHex("#b6b8bb");
	public static readonly Color Fg2 = Color.FromHex("#81848a");
	public static readonly Color Fg3 = Color.FromHex("#60728a");
	public static readonly Color Comment = Color.FromHex("#60728a");

	// Nord accent colors
	public static readonly Color Red = Color.FromHex("#bf616a");
	public static readonly Color Orange = Color.FromHex("#d08770");
	public static readonly Color Yellow = Color.FromHex("#ebcb8b");
	public static readonly Color Green = Color.FromHex("#a3be8c");
	public static readonly Color Blue = Color.FromHex("#81a1c1");
	public static readonly Color Cyan = Color.FromHex("#88c0d0");
	public static readonly Color Magenta = Color.FromHex("#b48ead");
	public static readonly Color Pink = Color.FromHex("#b48ead");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Cyan],
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
	/// Nordfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
