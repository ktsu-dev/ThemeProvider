// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Kanagawa;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Kanagawa Dragon color palette with official hex values.
/// A darker, more intense variant inspired by Japanese dragons and ink paintings.
/// Based on the Kanagawa theme by rebelot.
/// </summary>
public class KanagawaDragon : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dragon-inspired darker backgrounds
	public static readonly Color Background = Color.FromHex("#0d0c0c");
	public static readonly Color BgAlt = Color.FromHex("#181616");
	public static readonly Color Bg0 = Color.FromHex("#0d0c0c");
	public static readonly Color Bg1 = Color.FromHex("#181616");
	public static readonly Color Bg2 = Color.FromHex("#201d23");
	public static readonly Color Bg3 = Color.FromHex("#282727");
	public static readonly Color WaveBlue1 = Color.FromHex("#0a0a0a");
	public static readonly Color WaveBlue2 = Color.FromHex("#12120f");

	// Dragon foreground colors - intense and sharp
	public static readonly Color Fg0 = Color.FromHex("#c5c9c5");
	public static readonly Color Fg1 = Color.FromHex("#b5b4b1");
	public static readonly Color Fg2 = Color.FromHex("#9e9b93");
	public static readonly Color Comment = Color.FromHex("#625e5a");

	// Intense dragon palette
	public static readonly Color SakuraPink = Color.FromHex("#c4746e");
	public static readonly Color WaveRed = Color.FromHex("#c4746e");
	public static readonly Color SummerGreen = Color.FromHex("#8a9a7b");
	public static readonly Color AutumnYellow = Color.FromHex("#c4b28a");
	public static readonly Color CrystalBlue = Color.FromHex("#8ba4b0");
	public static readonly Color SpringBlue = Color.FromHex("#7fb4ca");
	public static readonly Color KatanaGray = Color.FromHex("#8a8980");
	public static readonly Color IceBlue = Color.FromHex("#9cabca");
	public static readonly Color BoatYellow1 = Color.FromHex("#a69764");
	public static readonly Color BoatYellow2 = Color.FromHex("#b6927b");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		Background,  // Darkest
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
	/// Kanagawa Dragon is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
