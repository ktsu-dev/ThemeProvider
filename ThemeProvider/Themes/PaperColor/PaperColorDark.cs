// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.PaperColor;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the PaperColor Dark color palette with official hex values.
/// A clean, minimal dark theme inspired by Google's Material Design.
/// Based on the PaperColor theme by NLKNguyen.
/// </summary>
public class PaperColorDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dark background colors
	public static readonly Color Background = Color.FromHex("#1c1c1c");
	public static readonly Color BgAlt = Color.FromHex("#262626");
	public static readonly Color Bg0 = Color.FromHex("#1c1c1c");
	public static readonly Color Bg1 = Color.FromHex("#262626");
	public static readonly Color Bg2 = Color.FromHex("#303030");
	public static readonly Color Selection = Color.FromHex("#4e4e4e");
	public static readonly Color LineNumbers = Color.FromHex("#585858");

	// Light foreground colors for dark theme
	public static readonly Color Fg0 = Color.FromHex("#d0d0d0");
	public static readonly Color Fg1 = Color.FromHex("#bcbcbc");
	public static readonly Color Comment = Color.FromHex("#808080");

	// Material Design inspired dark colors
	public static readonly Color Red = Color.FromHex("#af005f");
	public static readonly Color Pink = Color.FromHex("#d70087");
	public static readonly Color Orange = Color.FromHex("#ff8700");
	public static readonly Color Yellow = Color.FromHex("#ffaf00");
	public static readonly Color Green = Color.FromHex("#5faf00");
	public static readonly Color Teal = Color.FromHex("#00afaf");
	public static readonly Color Blue = Color.FromHex("#0087d7");
	public static readonly Color Purple = Color.FromHex("#af87d7");
	public static readonly Color Brown = Color.FromHex("#8f8f00");
	public static readonly Color Gray = Color.FromHex("#8a8a8a");

	public static Collection<Color> Neutrals =>
	[
		Fg0,         // Lightest
		Background,  // Darkest
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
	/// PaperColor Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
