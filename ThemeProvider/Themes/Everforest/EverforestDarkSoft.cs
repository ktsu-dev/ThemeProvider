// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;

/// <summary>
/// Provides the Everforest Dark Soft color palette with official hex values.
/// Soft variant uses lower contrast backgrounds (#333c43) for reduced eye strain.
/// Based on the Everforest theme by sainnhe.
/// </summary>
public class EverforestDarkSoft : ISemanticTheme
{
	// Every accent is shared with the other dark variants; only the dimmest background differs.
	private static readonly SemanticPalette Palette = EverforestPalette.Dark("#293136"); // BgDim

	/// <inheritdoc />
	public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping => Palette.ToSemanticMapping();

	/// <summary>
	/// Everforest Dark Soft is a dark theme with reduced contrast for eye comfort
	/// </summary>
	public bool IsDarkTheme => true;
}
