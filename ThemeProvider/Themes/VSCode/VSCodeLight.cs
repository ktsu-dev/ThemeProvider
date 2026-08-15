// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.VSCode;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the VSCode Light+ color palette with official hex values.
/// Based on the default VSCode Light+ theme.
/// </summary>
public class VSCodeLight : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#000000", "#ffffff"], // Foreground, Background
		Primary = "#0000ff", // Keyword
		Alternate = "#af00db", // Purple
		Success = "#098658", // Number
		CallToAction = "#098658", // Number
		Information = "#316bcd", // Info
		Caution = "#a31515", // StringColor
		Warning = "#bf8803", // Warning
		Error = "#cd3131", // Error
		Failure = "#cd3131", // Error
		Debug = "#af00db", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// VSCode Light+ is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
