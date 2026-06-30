// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Catppuccin;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Catppuccin Macchiato color palette with exact hex values and properties.
/// Based on the official specification: https://catppuccin.com/palette
/// </summary>
public class Macchiato : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Base = Color.FromHex("#24273a");
	public static readonly Color Mantle = Color.FromHex("#1e2030");
	public static readonly Color Crust = Color.FromHex("#181926");
	public static readonly Color Surface0 = Color.FromHex("#363a4f");
	public static readonly Color Surface1 = Color.FromHex("#494d64");
	public static readonly Color Surface2 = Color.FromHex("#5b6078");
	public static readonly Color Overlay0 = Color.FromHex("#6e738d");
	public static readonly Color Overlay1 = Color.FromHex("#8087a2");
	public static readonly Color Overlay2 = Color.FromHex("#939ab7");
	public static readonly Color Text = Color.FromHex("#cad3f5");
	public static readonly Color Subtext1 = Color.FromHex("#b8c0e0");
	public static readonly Color Subtext0 = Color.FromHex("#a5adcb");
	public static readonly Color Lavender = Color.FromHex("#b7bdf8");
	public static readonly Color Blue = Color.FromHex("#8aadf4");
	public static readonly Color Sapphire = Color.FromHex("#7dc4e4");
	public static readonly Color Sky = Color.FromHex("#91d7e3");
	public static readonly Color Teal = Color.FromHex("#8bd5ca");
	public static readonly Color Green = Color.FromHex("#a6da95");
	public static readonly Color Yellow = Color.FromHex("#eed49f");
	public static readonly Color Peach = Color.FromHex("#f5a97f");
	public static readonly Color Maroon = Color.FromHex("#ee99a0");
	public static readonly Color Red = Color.FromHex("#ed8796");
	public static readonly Color Mauve = Color.FromHex("#c6a0f6");
	public static readonly Color Pink = Color.FromHex("#f5bde6");

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
	/// Catppuccin Macchiato is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
