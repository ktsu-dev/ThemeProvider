// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Gruvbox Dark Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds for easier on the eyes.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxDarkSoft : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#ebdbb2", "#32302f"], // Light1, Dark0Soft
		Primary = "#fe8019", // BrightOrange
		Alternate = "#d3869b", // BrightPurple
		Success = "#b8bb26", // BrightGreen
		CallToAction = "#b8bb26", // BrightGreen
		Information = "#8ec07c", // BrightAqua
		Caution = "#83a598", // BrightBlue
		Warning = "#fabd2f", // BrightYellow
		Error = "#fb4934", // BrightRed
		Failure = "#fb4934", // BrightRed
		Debug = "#d3869b", // BrightPurple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Gruvbox Dark Soft is a dark theme with reduced contrast
	/// </summary>
	public bool IsDarkTheme => true;
}
