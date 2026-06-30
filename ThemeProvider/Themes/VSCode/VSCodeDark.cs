// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.VSCode;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the VSCode Dark+ color palette with official hex values.
/// Based on the default VSCode Dark+ theme.
/// </summary>
public class VSCodeDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#1e1e1e");
	public static readonly Color SidebarBackground = Color.FromHex("#252526");
	public static readonly Color Selection = Color.FromHex("#264f78");
	public static readonly Color LineHighlight = Color.FromHex("#2a2d2e");
	public static readonly Color Foreground = Color.FromHex("#d4d4d4");
	public static readonly Color Comment = Color.FromHex("#6a9955");
	public static readonly Color StringColor = Color.FromHex("#ce9178");
	public static readonly Color Number = Color.FromHex("#b5cea8");
	public static readonly Color Keyword = Color.FromHex("#569cd6");
	public static readonly Color Function = Color.FromHex("#dcdcaa");
	public static readonly Color Variable = Color.FromHex("#9cdcfe");
	public static readonly Color Type = Color.FromHex("#4ec9b0");
	public static readonly Color Error = Color.FromHex("#f44747");
	public static readonly Color Warning = Color.FromHex("#ffcc02");
	public static readonly Color Info = Color.FromHex("#75beff");
	public static readonly Color Purple = Color.FromHex("#c586c0");
	public static readonly Color Orange = Color.FromHex("#d7ba7d");

	public static Collection<Color> Neutrals =>
	[
		Foreground,      // Lightest
		Background,      // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Keyword],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Number],
		[SemanticMeaning.CallToAction] = [Number],
		[SemanticMeaning.Information] = [Info],
		[SemanticMeaning.Caution] = [StringColor],
		[SemanticMeaning.Warning] = [Warning],
		[SemanticMeaning.Error] = [Error],
		[SemanticMeaning.Failure] = [Error],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// VSCode Dark+ is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
