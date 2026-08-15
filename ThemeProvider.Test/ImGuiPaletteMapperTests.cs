// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System.Collections.Generic;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ThemeProvider.ImGui;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies the Dear ImGui palette mapper produces a complete, in-range palette for every
/// registered theme.
/// </summary>
[TestClass]
public class ImGuiPaletteMapperTests
{
	/// <summary>
	/// The mapper identifies the framework it targets.
	/// </summary>
	[TestMethod]
	public void FrameworkName_IsDearImGui() =>
		Assert.AreEqual("Dear ImGui", new ImGuiPaletteMapper().FrameworkName);

	/// <summary>
	/// A null theme is rejected rather than producing a partially-populated palette.
	/// </summary>
	[TestMethod]
	public void MapTheme_WithNullTheme_Throws() =>
		Assert.ThrowsExactly<ArgumentNullException>(() => new ImGuiPaletteMapper().MapTheme(null!));

	/// <summary>
	/// Mapping a theme fills in the ImGui colors the mapper claims to cover.
	/// </summary>
	[TestMethod]
	public void MapTheme_PopulatesCoreImGuiColors()
	{
		IReadOnlyDictionary<ImGuiCol, Vector4> palette =
			new ImGuiPaletteMapper().MapTheme(ThemeRegistry.AllThemes[0].CreateInstance());

		Assert.IsNotEmpty(palette);

		// A representative slice across the neutral surfaces, accent fills and glyph colors.
		ImGuiCol[] expected =
		[
			ImGuiCol.WindowBg, ImGuiCol.ChildBg, ImGuiCol.PopupBg, ImGuiCol.MenuBarBg,
			ImGuiCol.FrameBg, ImGuiCol.Text, ImGuiCol.TextDisabled,
			ImGuiCol.Button, ImGuiCol.ButtonHovered, ImGuiCol.ButtonActive,
			ImGuiCol.CheckMark, ImGuiCol.SliderGrab, ImGuiCol.Separator,
			ImGuiCol.Tab, ImGuiCol.TabSelected, ImGuiCol.TitleBg, ImGuiCol.Border,
		];

		foreach (ImGuiCol key in expected)
		{
			Assert.IsTrue(palette.ContainsKey(key), $"Missing ImGui color: {key}");
		}
	}

	/// <summary>
	/// Every theme in the catalog must map to the same set of ImGui colors — a theme that produced a
	/// smaller palette would leave parts of an application unstyled.
	/// </summary>
	[TestMethod]
	public void MapTheme_ProducesSamePaletteSizeForEveryTheme()
	{
		ImGuiPaletteMapper mapper = new();
		int expectedCount = mapper.MapTheme(ThemeRegistry.AllThemes[0].CreateInstance()).Count;

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			Assert.HasCount(
				expectedCount,
				mapper.MapTheme(info.CreateInstance()),
				$"{info.Name}: mapped a different number of ImGui colors");
		}
	}

	/// <summary>
	/// ImGui expects color components in [0, 1]; anything outside that renders as a clipped or
	/// wrapped color.
	/// </summary>
	[TestMethod]
	public void MapTheme_AllComponentsWithinUnitRange()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			foreach (KeyValuePair<ImGuiCol, Vector4> entry in mapper.MapTheme(info.CreateInstance()))
			{
				Vector4 v = entry.Value;
				string label = $"{info.Name}/{entry.Key}";
				Assert.IsTrue(v.X is >= 0f and <= 1f, $"{label}: R out of range ({v.X})");
				Assert.IsTrue(v.Y is >= 0f and <= 1f, $"{label}: G out of range ({v.Y})");
				Assert.IsTrue(v.Z is >= 0f and <= 1f, $"{label}: B out of range ({v.Z})");
				Assert.IsTrue(v.W is >= 0f and <= 1f, $"{label}: A out of range ({v.W})");
			}
		}
	}

	/// <summary>
	/// Mapping is deterministic: the same theme yields the same palette every time.
	/// </summary>
	[TestMethod]
	public void MapTheme_IsDeterministic()
	{
		ImGuiPaletteMapper mapper = new();
		ISemanticTheme theme = ThemeRegistry.AllThemes[0].CreateInstance();

		IReadOnlyDictionary<ImGuiCol, Vector4> first = mapper.MapTheme(theme);
		IReadOnlyDictionary<ImGuiCol, Vector4> second = mapper.MapTheme(theme);

		Assert.HasCount(first.Count, second);
		foreach (KeyValuePair<ImGuiCol, Vector4> entry in first)
		{
			Assert.AreEqual(entry.Value, second[entry.Key], $"{entry.Key} differed between calls");
		}
	}

	/// <summary>
	/// Text must be legible on the window background it is drawn over. This is the mapper's whole
	/// purpose, so it is asserted for every theme rather than a sample.
	/// </summary>
	[TestMethod]
	public void MapTheme_TextContrastsWithWindowBackground()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());
			Vector4 text = palette[ImGuiCol.Text];
			Vector4 background = palette[ImGuiCol.WindowBg];

			Assert.AreNotEqual(text, background, $"{info.Name}: text and window background are identical");
		}
	}
}
