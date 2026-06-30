// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Gruvbox Dark Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds for easier on the eyes.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxDarkSoft : ISemanticTheme
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	// Dark colors - using soft backgrounds for lower contrast
	public static readonly Color DarkHard = Color.FromHex("#1d2021");
	public static readonly Color Dark0 = Color.FromHex("#282828");
	public static readonly Color Dark0Soft = Color.FromHex("#32302f");
	public static readonly Color Dark1 = Color.FromHex("#3c3836");
	public static readonly Color Dark2 = Color.FromHex("#504945");
	public static readonly Color Dark3 = Color.FromHex("#665c54");
	public static readonly Color Dark4 = Color.FromHex("#7c6f64");

	// Light colors
	public static readonly Color Light0Hard = Color.FromHex("#f9f5d7");
	public static readonly Color Light0 = Color.FromHex("#fbf1c7");
	public static readonly Color Light0Soft = Color.FromHex("#f2e5bc");
	public static readonly Color Light1 = Color.FromHex("#ebdbb2");
	public static readonly Color Light2 = Color.FromHex("#d5c4a1");
	public static readonly Color Light3 = Color.FromHex("#bdae93");
	public static readonly Color Light4 = Color.FromHex("#a89984");

	// Bright colors
	public static readonly Color BrightRed = Color.FromHex("#fb4934");
	public static readonly Color BrightGreen = Color.FromHex("#b8bb26");
	public static readonly Color BrightYellow = Color.FromHex("#fabd2f");
	public static readonly Color BrightBlue = Color.FromHex("#83a598");
	public static readonly Color BrightPurple = Color.FromHex("#d3869b");
	public static readonly Color BrightAqua = Color.FromHex("#8ec07c");
	public static readonly Color BrightOrange = Color.FromHex("#fe8019");

	// Neutral colors
	public static readonly Color Gray = Color.FromHex("#928374");

	public static Collection<Color> Neutrals =>
	[
		Light1,         // Lightest
		Dark0Soft,      // Darkest - using soft background for reduced contrast
	];

	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => new()
	{
		[SemanticMeaning.Neutral] = Neutrals,
		[SemanticMeaning.Primary] = [BrightOrange],
		[SemanticMeaning.Alternate] = [BrightPurple],
		[SemanticMeaning.Success] = [BrightGreen],
		[SemanticMeaning.CallToAction] = [BrightGreen],
		[SemanticMeaning.Information] = [BrightAqua],
		[SemanticMeaning.Caution] = [BrightBlue],
		[SemanticMeaning.Warning] = [BrightYellow],
		[SemanticMeaning.Error] = [BrightRed],
		[SemanticMeaning.Failure] = [BrightRed],
		[SemanticMeaning.Debug] = [BrightPurple]
	};

	/// <summary>
	/// Gruvbox Dark Soft is a dark theme with reduced contrast
	/// </summary>
	public bool IsDarkTheme => true;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
