// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Dark Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#272e33).
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDarkHard : ISemanticTheme
{
	// Every accent is shared with the other dark variants; only the dimmest background differs.
	private static readonly SemanticPalette Palette = EverforestPalette.Dark("#1e2326"); // BgDim

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Dark Hard is a dark theme with high contrast
	/// </summary>
	public bool IsDarkTheme => true;
}
