// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.PaperColor;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the PaperColor Light color palette with official hex values.
/// A clean, minimal light theme inspired by Google's Material Design.
/// Based on the PaperColor theme by NLKNguyen.
/// </summary>
public class PaperColorLight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Light background colors
	public static readonly Color Background = Color.FromHex("#eeeeee");
	public static readonly Color BgAlt = Color.FromHex("#ffffff");
	public static readonly Color Bg0 = Color.FromHex("#eeeeee");
	public static readonly Color Bg1 = Color.FromHex("#e4e4e4");
	public static readonly Color Bg2 = Color.FromHex("#d0d0d0");
	public static readonly Color Selection = Color.FromHex("#e4e4e4");
	public static readonly Color LineNumbers = Color.FromHex("#878787");

	// Dark foreground colors
	public static readonly Color Fg0 = Color.FromHex("#444444");
	public static readonly Color Fg1 = Color.FromHex("#878787");
	public static readonly Color Comment = Color.FromHex("#8e908c");

	// Material Design inspired colors
	public static readonly Color Red = Color.FromHex("#af0000");
	public static readonly Color Pink = Color.FromHex("#d70087");
	public static readonly Color Orange = Color.FromHex("#d75f00");
	public static readonly Color Yellow = Color.FromHex("#d78700");
	public static readonly Color Green = Color.FromHex("#008700");
	public static readonly Color Teal = Color.FromHex("#00af87");
	public static readonly Color Blue = Color.FromHex("#0087af");
	public static readonly Color Purple = Color.FromHex("#8700af");
	public static readonly Color Brown = Color.FromHex("#5f8700");
	public static readonly Color Gray = Color.FromHex("#5f5f5f");

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
		[SemanticMeaning.CallToAction] = [Teal],
		[SemanticMeaning.Information] = [Blue],
		[SemanticMeaning.Caution] = [Yellow],
		[SemanticMeaning.Warning] = [Orange],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Pink]
	};

	/// <summary>
	/// PaperColor Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
