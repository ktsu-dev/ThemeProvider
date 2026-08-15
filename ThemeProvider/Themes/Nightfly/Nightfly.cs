// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Nightfly;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Nightfly color palette with official hex values.
/// Based on the Nightfly theme by bluz71.
/// </summary>
public class Nightfly : ISemanticTheme
{
	// Hex values are the palette this theme mirrors; the trailing comment on each line is
	// that palette's own name for the color.
	private static readonly SemanticPalette Palette = new()
	{
		Neutrals = ["#ffffff", "#011627"], // White, Background
		Primary = "#82aaff", // Blue
		Alternate = "#c792ea", // Purple
		Success = "#addb67", // Green
		CallToAction = "#addb67", // Green
		Information = "#7fdbca", // Cyan
		Caution = "#f78c6c", // Orange
		Warning = "#e3d18a", // Yellow
		Error = "#fc514e", // Red
		Failure = "#fc514e", // Red
		Debug = "#c792ea", // Purple
	};

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Nightfly is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
