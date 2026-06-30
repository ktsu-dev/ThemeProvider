// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.TokyoNight;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Tokyo Night color palette with official hex values.
/// Based on the popular Tokyo Night theme by Enkia.
/// </summary>
public class TokyoNight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#1a1b26");
	public static readonly Color BackgroundHighlight = Color.FromHex("#24283b");
	public static readonly Color Terminal = Color.FromHex("#1d202f");
	public static readonly Color Foreground = Color.FromHex("#c0caf5");
	public static readonly Color ForegroundDark = Color.FromHex("#a9b1d6");
	public static readonly Color ForegroundGutter = Color.FromHex("#3b4261");
	public static readonly Color Dark3 = Color.FromHex("#545c7e");
	public static readonly Color Comment = Color.FromHex("#565f89");
	public static readonly Color Dark5 = Color.FromHex("#737aa2");
	public static readonly Color Blue0 = Color.FromHex("#3d59a1");
	public static readonly Color Blue = Color.FromHex("#7aa2f7");
	public static readonly Color Cyan = Color.FromHex("#7dcfff");
	public static readonly Color Blue1 = Color.FromHex("#2ac3de");
	public static readonly Color Blue2 = Color.FromHex("#0db9d7");
	public static readonly Color Blue5 = Color.FromHex("#89ddff");
	public static readonly Color Blue6 = Color.FromHex("#b4f9f8");
	public static readonly Color Blue7 = Color.FromHex("#394b70");
	public static readonly Color Magenta = Color.FromHex("#bb9af7");
	public static readonly Color Magenta2 = Color.FromHex("#ff007c");
	public static readonly Color Purple = Color.FromHex("#9d7cd8");
	public static readonly Color Orange = Color.FromHex("#ff9e64");
	public static readonly Color Yellow = Color.FromHex("#e0af68");
	public static readonly Color Green = Color.FromHex("#9ece6a");
	public static readonly Color Green1 = Color.FromHex("#73daca");
	public static readonly Color Green2 = Color.FromHex("#41a6b5");
	public static readonly Color Teal = Color.FromHex("#1abc9c");
	public static readonly Color Red = Color.FromHex("#f7768e");
	public static readonly Color Red1 = Color.FromHex("#db4b4b");

	public static Collection<Color> Neutrals =>
	[
		Foreground,         // Lightest
		Background,         // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Magenta],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Cyan],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red1],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Tokyo Night is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
