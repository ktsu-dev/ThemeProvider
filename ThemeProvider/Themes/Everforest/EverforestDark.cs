// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Dark color palette with official hex values.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDark : ISemanticTheme
{
	// Every accent is shared with the other dark variants; only the dimmest background differs.
	private static readonly SemanticPalette Palette = EverforestPalette.Dark("#232a2e"); // BgDim

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Dark is a dark theme
	/// </summary>
	public bool IsDarkTheme => true;
}
