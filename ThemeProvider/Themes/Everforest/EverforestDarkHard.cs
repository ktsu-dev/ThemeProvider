// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Dark Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#272e33).
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDarkHard : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#272e33");
	public static readonly Color BgDim = Color.FromHex("#1e2326");
	public static readonly Color Bg0 = Color.FromHex("#272e33");
	public static readonly Color Bg1 = Color.FromHex("#2e383c");
	public static readonly Color Bg2 = Color.FromHex("#374145");
	public static readonly Color Bg3 = Color.FromHex("#414b50");
	public static readonly Color Bg4 = Color.FromHex("#495156");
	public static readonly Color Bg5 = Color.FromHex("#4f5b58");
	public static readonly Color Grey0 = Color.FromHex("#7a8478");
	public static readonly Color Grey1 = Color.FromHex("#859289");
	public static readonly Color Grey2 = Color.FromHex("#9da9a0");
	public static readonly Color Fg = Color.FromHex("#d3c6aa");
	public static readonly Color Red = Color.FromHex("#e67e80");
	public static readonly Color Orange = Color.FromHex("#e69875");
	public static readonly Color Yellow = Color.FromHex("#dbbc7f");
	public static readonly Color Green = Color.FromHex("#a7c080");
	public static readonly Color Aqua = Color.FromHex("#83c092");
	public static readonly Color Blue = Color.FromHex("#7fbbb3");
	public static readonly Color Purple = Color.FromHex("#d699b6");
	public static readonly Color StatuslineA = Color.FromHex("#a7c080");
	public static readonly Color StatuslineB = Color.FromHex("#d3c6aa");
	public static readonly Color StatuslineC = Color.FromHex("#272e33");

	public static Collection<Color> Neutrals =>
	[
		Fg,          // Lightest
		BgDim,       // Darkest
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
	/// Everforest Dark Hard is a dark theme with high contrast
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
