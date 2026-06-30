// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Light Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#fffbef) for better readability.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLightHard : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#fffbef");
	public static readonly Color BgDim = Color.FromHex("#f8f4e6");
	public static readonly Color Bg0 = Color.FromHex("#fffbef");
	public static readonly Color Bg1 = Color.FromHex("#f7f4e0");
	public static readonly Color Bg2 = Color.FromHex("#f0ecce");
	public static readonly Color Bg3 = Color.FromHex("#e9e5c5");
	public static readonly Color Bg4 = Color.FromHex("#e1ddbb");
	public static readonly Color Bg5 = Color.FromHex("#dad5b1");
	public static readonly Color Grey0 = Color.FromHex("#a6b0a0");
	public static readonly Color Grey1 = Color.FromHex("#939f91");
	public static readonly Color Grey2 = Color.FromHex("#829181");
	public static readonly Color Fg = Color.FromHex("#5c6a72");
	public static readonly Color Red = Color.FromHex("#f85552");
	public static readonly Color Orange = Color.FromHex("#f57d26");
	public static readonly Color Yellow = Color.FromHex("#dfa000");
	public static readonly Color Green = Color.FromHex("#8da101");
	public static readonly Color Aqua = Color.FromHex("#35a77c");
	public static readonly Color Blue = Color.FromHex("#3a94c5");
	public static readonly Color Purple = Color.FromHex("#df69ba");
	public static readonly Color StatuslineA = Color.FromHex("#8da101");
	public static readonly Color StatuslineB = Color.FromHex("#5c6a72");
	public static readonly Color StatuslineC = Color.FromHex("#fffbef");

	public static Collection<Color> Neutrals =>
	[
		Fg,          // Darkest (for text in light theme)
		BgDim,       // Lightest (for backgrounds in light theme - high contrast)
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Green],
		[SemanticMeaning.Alternate] = [Orange],
		[SemanticMeaning.Success] = [Blue],
		[SemanticMeaning.CallToAction] = [Aqua],
		[SemanticMeaning.Information] = [Purple],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Red],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Everforest Light Hard is a light theme with high contrast
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
