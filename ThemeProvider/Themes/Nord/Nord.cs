// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Nord;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the official Nord color palette with exact hex values and properties.
/// Based on the official specification: https://www.nordtheme.com/
/// </summary>
public class Nord : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Polar Night (Dark colors)
	public static readonly Color Nord0 = Color.FromHex("#2e3440");
	public static readonly Color Nord1 = Color.FromHex("#3b4252");
	public static readonly Color Nord2 = Color.FromHex("#434c5e");
	public static readonly Color Nord3 = Color.FromHex("#4c566a");

	// Snow Storm (Light colors)
	public static readonly Color Nord4 = Color.FromHex("#d8dee9");
	public static readonly Color Nord5 = Color.FromHex("#e5e9f0");
	public static readonly Color Nord6 = Color.FromHex("#eceff4");

	// Frost (Blue colors)
	public static readonly Color Nord7 = Color.FromHex("#8fbcbb");
	public static readonly Color Nord8 = Color.FromHex("#88c0d0");
	public static readonly Color Nord9 = Color.FromHex("#81a1c1");
	public static readonly Color Nord10 = Color.FromHex("#5e81ac");

	// Aurora (Accent colors)
	public static readonly Color Nord11 = Color.FromHex("#bf616a"); // Red
	public static readonly Color Nord12 = Color.FromHex("#d08770"); // Orange
	public static readonly Color Nord13 = Color.FromHex("#ebcb8b"); // Yellow
	public static readonly Color Nord14 = Color.FromHex("#a3be8c"); // Green
	public static readonly Color Nord15 = Color.FromHex("#b48ead"); // Purple

	public static Collection<Color> Neutrals =>
	[
		Nord6, // Lightest
		Nord0, // Darkest
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [Nord8], // Signature cyan
		[SemanticMeaning.Alternate] = [Nord15], // Purple
		[SemanticMeaning.Success] = [Nord14], // Green
		[SemanticMeaning.CallToAction] = [Nord14], // Green
		[SemanticMeaning.Information] = [Nord10], // Darker blue
		[SemanticMeaning.Caution] = [Nord12], // Orange
		[SemanticMeaning.Warning] = [Nord13], // Yellow
		[SemanticMeaning.Error] = [Nord11], // Red
		[SemanticMeaning.Failure] = [Nord11], // Red
		[SemanticMeaning.Debug] = [Nord15] // Purple
	};

	/// <summary>
	/// Nord is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
