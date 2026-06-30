// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Mocha color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Mocha : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Base = Color.FromHex("#1e1e2e");
	public static readonly Color Mantle = Color.FromHex("#181825");
	public static readonly Color Crust = Color.FromHex("#11111b");
	public static readonly Color Surface0 = Color.FromHex("#313244");
	public static readonly Color Surface1 = Color.FromHex("#45475a");
	public static readonly Color Surface2 = Color.FromHex("#585b70");
	public static readonly Color Overlay0 = Color.FromHex("#6c7086");
	public static readonly Color Overlay1 = Color.FromHex("#7f849c");
	public static readonly Color Overlay2 = Color.FromHex("#9399b2");
	public static readonly Color Text = Color.FromHex("#cdd6f4");
	public static readonly Color Subtext1 = Color.FromHex("#bac2de");
	public static readonly Color Subtext0 = Color.FromHex("#a6adc8");
	public static readonly Color Lavender = Color.FromHex("#b4befe");
	public static readonly Color Blue = Color.FromHex("#89b4fa");
	public static readonly Color Sapphire = Color.FromHex("#74c7ec");
	public static readonly Color Sky = Color.FromHex("#89dceb");
	public static readonly Color Teal = Color.FromHex("#94e2d5");
	public static readonly Color Green = Color.FromHex("#a6e3a1");
	public static readonly Color Yellow = Color.FromHex("#f9e2af");
	public static readonly Color Peach = Color.FromHex("#fab387");
	public static readonly Color Maroon = Color.FromHex("#eba0ac");
	public static readonly Color Red = Color.FromHex("#f38ba8");
	public static readonly Color Mauve = Color.FromHex("#cba6f7");
	public static readonly Color Pink = Color.FromHex("#f5c2e7");

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
	/// Catppuccin Mocha is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
