// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Kanagawa Wave color palette with official hex values.
/// A dark theme inspired by Japanese paintings with warm, muted tones.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaWave : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Background colors - traditional Japanese palette
	public static readonly Color Background = Color.FromHex("#1f1f28");
	public static readonly Color BgAlt = Color.FromHex("#16161d");
	public static readonly Color Bg0 = Color.FromHex("#1f1f28");
	public static readonly Color Bg1 = Color.FromHex("#2a2a37");
	public static readonly Color Bg2 = Color.FromHex("#363646");
	public static readonly Color Bg3 = Color.FromHex("#54546d");
	public static readonly Color WaveBlue1 = Color.FromHex("#223249");
	public static readonly Color WaveBlue2 = Color.FromHex("#2d4f67");

	// Foreground colors
	public static readonly Color Fg0 = Color.FromHex("#dcd7ba");
	public static readonly Color Fg1 = Color.FromHex("#c8c093");
	public static readonly Color Fg2 = Color.FromHex("#9caca8");
	public static readonly Color Comment = Color.FromHex("#727169");

	// Traditional color palette
	public static readonly Color SakuraPink = Color.FromHex("#d27e99");
	public static readonly Color WaveRed = Color.FromHex("#e82424");
	public static readonly Color SummerGreen = Color.FromHex("#98bb6c");
	public static readonly Color AutumnYellow = Color.FromHex("#e6c384");
	public static readonly Color CrystalBlue = Color.FromHex("#7e9cd8");
	public static readonly Color SpringBlue = Color.FromHex("#7fb4ca");
	public static readonly Color KatanaGray = Color.FromHex("#717c7c");
	public static readonly Color IceBlue = Color.FromHex("#a3d4d5");
	public static readonly Color BoatYellow1 = Color.FromHex("#938056");
	public static readonly Color BoatYellow2 = Color.FromHex("#c0a36e");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		BgAlt,       // Darkest
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
	/// Kanagawa Wave is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
