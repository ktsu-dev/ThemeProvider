// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Monokai;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the classic Monokai color palette with exact hex values.
/// Based on the original Monokai theme by Wimer Hazenberg.
/// </summary>
public class Monokai : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static readonly Color Background = Color.FromHex("#272822");
	public static readonly Color CurrentLine = Color.FromHex("#49483e");
	public static readonly Color Selection = Color.FromHex("#49483e");
	public static readonly Color Foreground = Color.FromHex("#f8f8f2");
	public static readonly Color Comment = Color.FromHex("#75715e");
	public static readonly Color Red = Color.FromHex("#f92672");
	public static readonly Color Orange = Color.FromHex("#fd971f");
	public static readonly Color Yellow = Color.FromHex("#f4bf75");
	public static readonly Color Green = Color.FromHex("#a6e22e");
	public static readonly Color Aqua = Color.FromHex("#a1efe4");
	public static readonly Color Blue = Color.FromHex("#66d9ef");
	public static readonly Color Purple = Color.FromHex("#ae81ff");

	public static Collection<Color> Neutrals =>
	[
		Foreground,    // Lightest
		Background,    // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Blue],
		[SemanticMeaning.Alternate] = [Purple],
		[SemanticMeaning.Success] = [Green],
		[SemanticMeaning.CallToAction] = [Green],
		[SemanticMeaning.Information] = [Aqua],
		[SemanticMeaning.Caution] = [Orange],
		[SemanticMeaning.Warning] = [Yellow],
		[SemanticMeaning.Error] = [Red],
		[SemanticMeaning.Failure] = [Red],
		[SemanticMeaning.Debug] = [Purple]
	};

	/// <summary>
	/// Monokai is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
