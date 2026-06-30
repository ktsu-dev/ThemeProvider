// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Latte color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Latte : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Base = Color.FromHex("#eff1f5");
	public static readonly Color Mantle = Color.FromHex("#e6e9ef");
	public static readonly Color Crust = Color.FromHex("#dce0e8");
	public static readonly Color Surface0 = Color.FromHex("#ccd0da");
	public static readonly Color Surface1 = Color.FromHex("#bcc0cc");
	public static readonly Color Surface2 = Color.FromHex("#acb0be");
	public static readonly Color Overlay0 = Color.FromHex("#9ca0b0");
	public static readonly Color Overlay1 = Color.FromHex("#8c8fa1");
	public static readonly Color Overlay2 = Color.FromHex("#7c7f93");
	public static readonly Color Text = Color.FromHex("#4c4f69");
	public static readonly Color Subtext1 = Color.FromHex("#5c5f77");
	public static readonly Color Subtext0 = Color.FromHex("#6c6f85");
	public static readonly Color Lavender = Color.FromHex("#7287fd");
	public static readonly Color Blue = Color.FromHex("#1e66f5");
	public static readonly Color Sapphire = Color.FromHex("#209fb5");
	public static readonly Color Sky = Color.FromHex("#04a5e5");
	public static readonly Color Teal = Color.FromHex("#179299");
	public static readonly Color Green = Color.FromHex("#40a02b");
	public static readonly Color Yellow = Color.FromHex("#df8e1d");
	public static readonly Color Peach = Color.FromHex("#fe640b");
	public static readonly Color Maroon = Color.FromHex("#e64553");
	public static readonly Color Red = Color.FromHex("#d20f39");
	public static readonly Color Mauve = Color.FromHex("#8839ef");
	public static readonly Color Pink = Color.FromHex("#ea76cb");

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
	/// Catppuccin Latte is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
