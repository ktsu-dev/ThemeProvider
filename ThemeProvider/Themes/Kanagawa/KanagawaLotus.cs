// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Kanagawa Lotus color palette with official hex values.
/// A light variant inspired by lotus flowers and zen gardens.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaLotus : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Lotus-inspired light backgrounds
	public static readonly Color Background = Color.FromHex("#f2ecbc");
	public static readonly Color BgAlt = Color.FromHex("#f7f4dd");
	public static readonly Color Bg0 = Color.FromHex("#f2ecbc");
	public static readonly Color Bg1 = Color.FromHex("#e7dba0");
	public static readonly Color Bg2 = Color.FromHex("#e4d794");
	public static readonly Color Bg3 = Color.FromHex("#d8ce9b");
	public static readonly Color WaveBlue1 = Color.FromHex("#d7d3c0");
	public static readonly Color WaveBlue2 = Color.FromHex("#d5cea3");

	// Lotus foreground colors - soft and natural
	public static readonly Color Fg0 = Color.FromHex("#545464");
	public static readonly Color Fg1 = Color.FromHex("#43436c");
	public static readonly Color Fg2 = Color.FromHex("#6f6f9a");
	public static readonly Color Comment = Color.FromHex("#a6a69c");

	// Soft lotus palette - muted and serene
	public static readonly Color SakuraPink = Color.FromHex("#b35b79");
	public static readonly Color WaveRed = Color.FromHex("#cc5d73");
	public static readonly Color SummerGreen = Color.FromHex("#6f894e");
	public static readonly Color AutumnYellow = Color.FromHex("#77713f");
	public static readonly Color CrystalBlue = Color.FromHex("#4d699b");
	public static readonly Color SpringBlue = Color.FromHex("#5e857a");
	public static readonly Color KatanaGray = Color.FromHex("#8a8a7a");
	public static readonly Color IceBlue = Color.FromHex("#7e9fb8");
	public static readonly Color BoatYellow1 = Color.FromHex("#836f4a");
	public static readonly Color BoatYellow2 = Color.FromHex("#b98f56");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [CrystalBlue],
		[SemanticMeaning.Alternate] = [SakuraPink],
		[SemanticMeaning.Success] = [SummerGreen],
		[SemanticMeaning.CallToAction] = [SpringBlue],
		[SemanticMeaning.Information] = [IceBlue],
		[SemanticMeaning.Caution] = [AutumnYellow],
		[SemanticMeaning.Warning] = [BoatYellow2],
		[SemanticMeaning.Error] = [WaveRed],
		[SemanticMeaning.Failure] = [WaveRed],
		[SemanticMeaning.Debug] = [SakuraPink]
	};

	/// <summary>
	/// Kanagawa Lotus is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
