// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Light Hard color palette with official hex values.
/// Hard variant uses higher contrast backgrounds (#fffbef) for better readability.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLightHard : ISemanticTheme
{
	// Every accent is shared with the other light variants; only the dimmest background differs.
	private static readonly SemanticPalette Palette = EverforestPalette.Light("#f2efdf"); // BgDim

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Light Hard is a light theme with high contrast
	/// </summary>
	public bool IsDarkTheme => false;
}
