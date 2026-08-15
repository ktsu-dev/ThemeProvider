// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Gruvbox;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Gruvbox Dark Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds.
/// Based on the Gruvbox theme by morhetz.
/// </summary>
public class GruvboxDarkHard : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#ebdbb2", "#1d2021"], // Light1, DarkHard
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
	/// Gruvbox Dark Hard is a dark theme with high contrast
	/// </summary>
	public bool IsDarkTheme => true;
}
