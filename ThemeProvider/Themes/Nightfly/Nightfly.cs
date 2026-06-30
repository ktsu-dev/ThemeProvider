// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nightfly;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Nightfly color palette with official hex values.
/// Based on the Nightfly theme by bluz71.
/// </summary>
public class Nightfly : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#011627");
	public static readonly Color Selection = Color.FromHex("#1d3b53");
	public static readonly Color Foreground = Color.FromHex("#d6deeb");
	public static readonly Color Comment = Color.FromHex("#637777");
	public static readonly Color Red = Color.FromHex("#fc514e");
	public static readonly Color Orange = Color.FromHex("#f78c6c");
	public static readonly Color Yellow = Color.FromHex("#e3d18a");
	public static readonly Color Green = Color.FromHex("#addb67");
	public static readonly Color Teal = Color.FromHex("#4db5bd");
	public static readonly Color Blue = Color.FromHex("#82aaff");
	public static readonly Color Purple = Color.FromHex("#c792ea");
	public static readonly Color Cyan = Color.FromHex("#7fdbca");
	public static readonly Color White = Color.FromHex("#ffffff");
	public static readonly Color Gray1 = Color.FromHex("#1e2030");
	public static readonly Color Gray2 = Color.FromHex("#2c3043");
	public static readonly Color Gray3 = Color.FromHex("#506477");
	public static readonly Color Gray4 = Color.FromHex("#7e8294");
	public static readonly Color Gray5 = Color.FromHex("#a1aab8");

	public static Collection<Color> Neutrals =>
	[
		White,       // Lightest
		Background,  // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Cyan],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Nightfly is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
