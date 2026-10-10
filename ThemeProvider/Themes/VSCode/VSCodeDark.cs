// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.VSCode;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the VSCode Dark+ color palette with official hex values.
/// Based on the default VSCode Dark+ theme.
/// </summary>
public class VSCodeDark : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#d4d4d4", "#1e1e1e"], // Foreground, Background
		Primary = "#569cd6", // Keyword
		Alternate = "#c586c0", // Purple
		Success = "#b5cea8", // Number
		CallToAction = "#b5cea8", // Number
		Information = "#59a4f9", // EditorInfo
		Caution = "#ce9178", // StringColor
		Warning = "#cca700", // EditorWarning
		Error = "#f14c4c", // EditorError
		Failure = "#f14c4c", // EditorError
		Debug = "#c586c0", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// VSCode Dark+ is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
