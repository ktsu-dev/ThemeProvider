// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System;
using System.Collections.Generic;
using System.Numerics;

using Hexa.NET.ImGui;

using ktsu.Semantics.Color;
using ktsu.ThemeProvider;
using ktsu.ThemeProvider.ImGui;

/// <summary>Applies a theme to Dear ImGui and reads colours out of its semantic palette.</summary>
internal static class ThemeStyle
{
	private static readonly ImGuiPaletteMapper Mapper = new();

	/// <summary>Writes every colour <see cref="ImGuiPaletteMapper"/> maps into the current style, the way an application would.</summary>
	internal static void Apply(ISemanticTheme theme)
	{
		IReadOnlyDictionary<ImGuiCol, Vector4> mapped = Mapper.MapTheme(theme);
		Span<Vector4> colors = ImGui.GetStyle().Colors;
		foreach ((ImGuiCol key, Vector4 value) in mapped)
		{
			int index = (int)key;
			if (index >= 0 && index < colors.Length)
			{
				colors[index] = value;
			}
		}
	}

	/// <summary>Converts a semantic colour to the gamma-encoded RGBA Dear ImGui draws with.</summary>
	internal static Vector4 ToVector4(Color color)
	{
		Srgb srgb = color.ToSrgb();
		return new Vector4((float)srgb.R, (float)srgb.G, (float)srgb.B, (float)color.A);
	}
}
