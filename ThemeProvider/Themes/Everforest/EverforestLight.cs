// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Light color palette with official hex values.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestLight : ISemanticTheme
{
	// Every accent is shared with the other light variants; only the dimmest background differs.
	private static readonly SemanticPalette Palette = EverforestPalette.Light("#efebd4"); // BgDim

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Light is a light theme
	/// </summary>
	public bool IsDarkTheme => false;
}
