// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Carbonfox color palette with official hex values.
/// A minimalist dark variant inspired by carbon fiber and industrial design.
/// Based on the Nightfox theme family by EdenEast.
/// </summary>
public class Carbonfox : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Carbon-inspired dark backgrounds
	public static readonly Color Background = Color.FromHex("#161616");
	public static readonly Color BgAlt = Color.FromHex("#0c0c0c");
	public static readonly Color Bg0 = Color.FromHex("#161616");
	public static readonly Color Bg1 = Color.FromHex("#21272a");
	public static readonly Color Bg2 = Color.FromHex("#2a3439");
	public static readonly Color Bg3 = Color.FromHex("#4b5563");
	public static readonly Color Sel0 = Color.FromHex("#2a2a2a");
	public static readonly Color Sel1 = Color.FromHex("#3d3d3d");

	// Industrial foreground colors
	public static readonly Color Fg0 = Color.FromHex("#f2f4f8");
	public static readonly Color Fg1 = Color.FromHex("#b6b8bb");
	public static readonly Color Fg2 = Color.FromHex("#8b8d8f");
	public static readonly Color Fg3 = Color.FromHex("#6f7d8c");
	public static readonly Color Comment = Color.FromHex("#6f7d8c");

	// Carbon accent colors - minimal and functional
	public static readonly Color Red = Color.FromHex("#ee5396");
	public static readonly Color Orange = Color.FromHex("#3ddbd9");
	public static readonly Color Yellow = Color.FromHex("#08bdba");
	public static readonly Color Green = Color.FromHex("#25be6a");
	public static readonly Color Blue = Color.FromHex("#78a9ff");
	public static readonly Color Cyan = Color.FromHex("#33b1ff");
	public static readonly Color Magenta = Color.FromHex("#be95ff");
	public static readonly Color Pink = Color.FromHex("#ff7eb6");

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
	/// Carbonfox is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
