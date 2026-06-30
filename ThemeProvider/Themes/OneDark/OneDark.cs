// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.OneDark;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the One Dark color palette with official hex values.
/// Based on the Atom One Dark theme.
/// </summary>
public class OneDark : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#282c34");
	public static readonly Color CursorLine = Color.FromHex("#2c323c");
	public static readonly Color Selection = Color.FromHex("#3e4451");
	public static readonly Color Foreground = Color.FromHex("#abb2bf");
	public static readonly Color Comment = Color.FromHex("#5c6370");
	public static readonly Color Red = Color.FromHex("#e06c75");
	public static readonly Color Orange = Color.FromHex("#d19a66");
	public static readonly Color Yellow = Color.FromHex("#e5c07b");
	public static readonly Color Green = Color.FromHex("#98c379");
	public static readonly Color Cyan = Color.FromHex("#56b6c2");
	public static readonly Color Blue = Color.FromHex("#61afef");
	public static readonly Color Purple = Color.FromHex("#c678dd");
	public static readonly Color White = Color.FromHex("#ffffff");
	public static readonly Color Black = Color.FromHex("#181a1f");
	public static readonly Color VisualGray = Color.FromHex("#3e4451");
	public static readonly Color SpecialGray = Color.FromHex("#3b4048");
	public static readonly Color VertSplit = Color.FromHex("#181a1f");

	public static Collection<Color> Neutrals =>
	[
		White,         // Lightest
		Black,         // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Cyan],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// One Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
