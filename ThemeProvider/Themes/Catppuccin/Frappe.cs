// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Frappe color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Frappe : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Base = Color.FromHex("#303446");
	public static readonly Color Mantle = Color.FromHex("#292c3c");
	public static readonly Color Crust = Color.FromHex("#232634");
	public static readonly Color Surface0 = Color.FromHex("#414559");
	public static readonly Color Surface1 = Color.FromHex("#51576d");
	public static readonly Color Surface2 = Color.FromHex("#626880");
	public static readonly Color Overlay0 = Color.FromHex("#737994");
	public static readonly Color Overlay1 = Color.FromHex("#838ba7");
	public static readonly Color Overlay2 = Color.FromHex("#949cbb");
	public static readonly Color Text = Color.FromHex("#c6d0f5");
	public static readonly Color Subtext1 = Color.FromHex("#b5bfe2");
	public static readonly Color Subtext0 = Color.FromHex("#a5adce");
	public static readonly Color Lavender = Color.FromHex("#babbf1");
	public static readonly Color Blue = Color.FromHex("#8caaee");
	public static readonly Color Sapphire = Color.FromHex("#85c1dc");
	public static readonly Color Sky = Color.FromHex("#99d1db");
	public static readonly Color Teal = Color.FromHex("#81c8be");
	public static readonly Color Green = Color.FromHex("#a6d189");
	public static readonly Color Yellow = Color.FromHex("#e5c890");
	public static readonly Color Peach = Color.FromHex("#ef9f76");
	public static readonly Color Maroon = Color.FromHex("#ea999c");
	public static readonly Color Red = Color.FromHex("#e78284");
	public static readonly Color Mauve = Color.FromHex("#ca9ee6");
	public static readonly Color Pink = Color.FromHex("#f4b8e4");

	public static Collection<Color> Neutrals =>
	[
		Text,
		Crust,
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Pink],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Sapphire],
		[SemanticMeaning.Caution] = [Maroon],
		[SemanticMeaning.Warning] = [Peach],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Mauve]
	};

	/// <summary>
	/// Catppuccin Frappe is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
