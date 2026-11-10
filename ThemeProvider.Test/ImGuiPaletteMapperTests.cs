// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.Semantics.Color;
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
	/// A hand-written theme with accents but no neutrals is refused rather than mapped to a palette
	/// with no Text or WindowBg, which would leave ImGui's defaults under a themed UI.
	/// </summary>
	[TestMethod]
	public void MapTheme_WithNoNeutrals_Throws()
	{
		NoNeutralTheme theme = new();

		ArgumentException ex = Assert.ThrowsExactly<ArgumentException>(() => new ImGuiPaletteMapper().MapTheme(theme));
		Assert.Contains(nameof(SemanticMeaning.Neutral), ex.Message);
	}

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
	/// Every <see cref="ImGuiCol"/> slot must be assigned. An unassigned slot keeps whatever style was
	/// active before the theme was applied, usually <c>StyleColorsDark()</c>, so a light theme would be
	/// left with a white text cursor and navy dimmed tabs. Comparing themes against each other cannot
	/// catch a slot that every theme is missing.
	/// </summary>
	[TestMethod]
	public void MapTheme_AssignsEveryImGuiColor()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());

			for (int i = 0; i < (int)ImGuiCol.Count; i++)
			{
				ImGuiCol key = (ImGuiCol)i;
				Assert.IsTrue(palette.ContainsKey(key), $"{info.Name}: missing ImGui color {key}");
			}
		}
	}

	/// <summary>
	/// The text cursor is drawn over input frames, so it takes the text color rather than a fixed one.
	/// </summary>
	[TestMethod]
	public void MapTheme_InputTextCursorMatchesText()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());
			Assert.IsTrue(palette.TryGetValue(ImGuiCol.InputTextCursor, out Vector4 cursor), $"{info.Name}: InputTextCursor unmapped");
			Assert.AreEqual(palette[ImGuiCol.Text], cursor, info.Name);
		}
	}

	/// <summary>
	/// The dimming overlays cover the whole viewport, so they must be translucent or they would hide
	/// everything behind a modal or the window switcher.
	/// </summary>
	[TestMethod]
	public void MapTheme_DimBackgroundsAreTranslucent()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());

			foreach (ImGuiCol key in new[] { ImGuiCol.ModalWindowDimBg, ImGuiCol.NavWindowingDimBg })
			{
				Assert.IsTrue(palette.TryGetValue(key, out Vector4 dim), $"{info.Name}: {key} unmapped");
				Assert.IsTrue(dim.W is > 0f and < 1f, $"{info.Name}: {key} alpha is {dim.W}");
			}
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

	/// <summary>
	/// With RowBg on, ImGui fills odd rows with TableRowBgAlt, and an input frame in such a row is drawn
	/// in FrameBg with no border by default. The stripe as drawn over the window must not be the frame's
	/// color, or every other row's frames disappear.
	/// </summary>
	[TestMethod]
	public void MapTheme_AlternateRowDiffersFromFrame()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());
			Vector4 stripe = palette[ImGuiCol.TableRowBgAlt];
			Vector4 window = palette[ImGuiCol.WindowBg];
			Vector3 drawn = (new Vector3(stripe.X, stripe.Y, stripe.Z) * stripe.W) + (new Vector3(window.X, window.Y, window.Z) * (1f - stripe.W));
			Vector4 frame = palette[ImGuiCol.FrameBg];

			Assert.IsGreaterThan(1f / 255f, Vector3.Distance(drawn, new Vector3(frame.X, frame.Y, frame.Z)), $"{info.Name}: an alternate row is drawn in the frame color");
		}
	}

	/// <summary>
	/// ImGui fills a dock node's tab bar with TitleBgActive when focused and TitleBg otherwise, and draws
	/// the selected tab with TabSelected or TabDimmedSelected. Each selected tab must differ from the bar
	/// it sits on, or only the 1px overline shows which tab is open. Hovering must also read differently
	/// from being selected.
	/// </summary>
	[TestMethod]
	public void MapTheme_SelectedTabsDifferFromTheirTabBar()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());

			AssertDistinct(palette, ImGuiCol.TabSelected, ImGuiCol.TitleBgActive, info.Name);
			AssertDistinct(palette, ImGuiCol.TabDimmedSelected, ImGuiCol.TitleBg, info.Name);
			AssertDistinct(palette, ImGuiCol.TabHovered, ImGuiCol.TabSelected, info.Name);
		}
	}

	/// <summary>
	/// ImGui draws a table's outer border and the line under its header row in TableBorderStrong, and
	/// the inner row and column dividers in TableBorderLight. Strong must stand out from the window
	/// more than Light does, or the header no longer separates from the body. Light must also differ
	/// from the row and header fills it is drawn between, or the inner dividers vanish.
	/// </summary>
	[TestMethod]
	public void MapTheme_TableBorderStrongStandsOutMoreThanLight()
	{
		ImGuiPaletteMapper mapper = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<ImGuiCol, Vector4> palette = mapper.MapTheme(info.CreateInstance());

			AssertDistinct(palette, ImGuiCol.TableBorderStrong, ImGuiCol.TableBorderLight, info.Name);
			AssertDistinct(palette, ImGuiCol.TableBorderLight, ImGuiCol.TableRowBg, info.Name);
			AssertDistinct(palette, ImGuiCol.TableBorderLight, ImGuiCol.TableRowBgAlt, info.Name);
			AssertDistinct(palette, ImGuiCol.TableBorderLight, ImGuiCol.TableHeaderBg, info.Name);

			Vector4 window = palette[ImGuiCol.WindowBg];
			float strong = Vector4.Distance(palette[ImGuiCol.TableBorderStrong], window);
			float light = Vector4.Distance(palette[ImGuiCol.TableBorderLight], window);
			Assert.IsGreaterThan(light, strong, $"{info.Name}: TableBorderStrong stands out from WindowBg no more than TableBorderLight");
		}
	}

	private static void AssertDistinct(IReadOnlyDictionary<ImGuiCol, Vector4> palette, ImGuiCol first, ImGuiCol second, string themeName) =>
		Assert.IsGreaterThan(1f / 255f, Vector4.Distance(palette[first], palette[second]), $"{themeName}: {first} is drawn in the {second} color");

	private sealed class NoNeutralTheme : ISemanticTheme
	{
		public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping { get; } = new()
		{
			[SemanticMeaning.Primary] = [Color.FromHex("#89b4fa")],
			[SemanticMeaning.Error] = [Color.FromHex("#f38ba8")],
		};

		public bool IsDarkTheme => true;
	}
}
