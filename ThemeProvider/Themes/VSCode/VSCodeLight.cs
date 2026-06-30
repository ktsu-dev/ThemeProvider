// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.VSCode;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the VSCode Light+ color palette with official hex values.
/// Based on the default VSCode Light+ theme.
/// </summary>
public class VSCodeLight : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#ffffff");
	public static readonly Color SidebarBackground = Color.FromHex("#f3f3f3");
	public static readonly Color Selection = Color.FromHex("#add6ff");
	public static readonly Color LineHighlight = Color.FromHex("#f0f0f0");
	public static readonly Color Foreground = Color.FromHex("#000000");
	public static readonly Color Comment = Color.FromHex("#008000");
	public static readonly Color StringColor = Color.FromHex("#a31515");
	public static readonly Color Number = Color.FromHex("#098658");
	public static readonly Color Keyword = Color.FromHex("#0000ff");
	public static readonly Color Function = Color.FromHex("#795e26");
	public static readonly Color Variable = Color.FromHex("#001080");
	public static readonly Color Type = Color.FromHex("#267f99");
	public static readonly Color Error = Color.FromHex("#cd3131");
	public static readonly Color Warning = Color.FromHex("#bf8803");
	public static readonly Color Info = Color.FromHex("#316bcd");
	public static readonly Color Purple = Color.FromHex("#af00db");
	public static readonly Color Orange = Color.FromHex("#e07041");

	public static Collection<Color> Neutrals =>
	[
		Foreground,         // Darkest (for text in light theme)
		Background,         // Lightest (for backgrounds in light theme)
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
	/// VSCode Light+ is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
