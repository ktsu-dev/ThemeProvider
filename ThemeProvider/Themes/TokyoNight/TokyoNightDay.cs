// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Tokyo Night Day color palette with official hex values.
/// The light variant of Tokyo Night with bright backgrounds and dark text.
/// Based on the Tokyo Night theme by enkia.
/// </summary>
public class TokyoNightDay : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light background colors
	public static readonly Color Background = Color.FromHex("#e1e2e7");
	public static readonly Color BgAlt = Color.FromHex("#e9e9ed");
	public static readonly Color Bg0 = Color.FromHex("#e1e2e7");
	public static readonly Color Bg1 = Color.FromHex("#e9e9ec");
	public static readonly Color Bg2 = Color.FromHex("#c4c8da");
	public static readonly Color Sel0 = Color.FromHex("#b7c5d3");
	public static readonly Color Sel1 = Color.FromHex("#a8b4c5");

	// Dark foreground colors for light theme
	public static readonly Color Fg0 = Color.FromHex("#3760bf");
	public static readonly Color Fg1 = Color.FromHex("#4c505e");
	public static readonly Color Fg2 = Color.FromHex("#5a5f6d");
	public static readonly Color Comment = Color.FromHex("#9699a3");

	// Tokyo Night Day color palette
	public static readonly Color Blue = Color.FromHex("#2e7de9");
	public static readonly Color Purple = Color.FromHex("#9854f1");
	public static readonly Color Cyan = Color.FromHex("#007197");
	public static readonly Color Green = Color.FromHex("#587539");
	public static readonly Color Teal = Color.FromHex("#33635c");
	public static readonly Color Yellow = Color.FromHex("#8c6c3e");
	public static readonly Color Orange = Color.FromHex("#b15c00");
	public static readonly Color Red = Color.FromHex("#f52a65");
	public static readonly Color Magenta = Color.FromHex("#9854f1");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Darkest (for text in light theme)
		BgAlt,       // Lightest (for backgrounds)
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
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
	/// Tokyo Night Day is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
