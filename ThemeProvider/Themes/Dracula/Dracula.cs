// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Dracula;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Dracula color palette with exact hex values.
/// Based on the official Dracula theme specification.
/// </summary>
public class Dracula : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#282a36");
	public static readonly Color CurrentLine = Color.FromHex("#44475a");
	public static readonly Color Selection = Color.FromHex("#44475a");
	public static readonly Color Foreground = Color.FromHex("#f8f8f2");
	public static readonly Color Comment = Color.FromHex("#6272a4");
	public static readonly Color Cyan = Color.FromHex("#8be9fd");
	public static readonly Color Green = Color.FromHex("#50fa7b");
	public static readonly Color Orange = Color.FromHex("#ffb86c");
	public static readonly Color Pink = Color.FromHex("#ff79c6");
	public static readonly Color Purple = Color.FromHex("#bd93f9");
	public static readonly Color Red = Color.FromHex("#ff5555");
	public static readonly Color Yellow = Color.FromHex("#f1fa8c");

	public static Collection<Color> Neutrals =>
	[
		Foreground,   // Lightest
		Background,   // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Purple],
		[SemanticMeaning.Alternate] = [Pink],
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
	/// Dracula is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
