// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Covers the boundary conditions of <see cref="SemanticColorMapper"/>: themes that supply no
/// colors, meanings with a single color to extrapolate from, and requests for meanings a theme
/// does not define.
/// </summary>
[TestClass]
public class SemanticColorMapperEdgeCaseTests
{
	/// <summary>
	/// A theme built from an explicit mapping, used to drive cases the shipped themes cannot reach.
	/// </summary>
	private sealed class StubTheme(Dictionary<SemanticMeaning, Collection<Color>> mapping, bool isDark)
		: ISemanticTheme
	{
		public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping { get; } = mapping;

		public bool IsDarkTheme { get; } = isDark;
	}

	/// <summary>
	/// Mapping with no requests short-circuits to an empty result.
	/// </summary>
	[TestMethod]
	public void MapColors_WithNoRequests_ReturnsEmpty()
	{
		IReadOnlyDictionary<SemanticColorRequest, Color> result =
			SemanticColorMapper.MapColors([], ThemeRegistry.AllThemes[0].CreateInstance());

		Assert.IsEmpty(result);
	}

	/// <summary>
	/// A null request collection is rejected.
	/// </summary>
	[TestMethod]
	public void MapColors_WithNullRequests_Throws() =>
		Assert.ThrowsExactly<ArgumentNullException>(() =>
			SemanticColorMapper.MapColors(null!, ThemeRegistry.AllThemes[0].CreateInstance()));

	/// <summary>
	/// A null theme is rejected.
	/// </summary>
	[TestMethod]
	public void MapColors_WithNullTheme_Throws() =>
		Assert.ThrowsExactly<ArgumentNullException>(() =>
			SemanticColorMapper.MapColors([new(SemanticMeaning.Primary, Priority.Medium)], null!));

	/// <summary>
	/// A null theme is rejected when generating a complete palette.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_WithNullTheme_Throws() =>
		Assert.ThrowsExactly<ArgumentNullException>(() => SemanticColorMapper.MakeCompletePalette(null!));

	/// <summary>
	/// A theme that defines no meanings yields an empty palette rather than throwing.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_WithEmptyTheme_ReturnsEmpty()
	{
		StubTheme theme = new([], isDark: true);

		Assert.IsEmpty(SemanticColorMapper.MakeCompletePalette(theme));
	}

	/// <summary>
	/// Requesting a meaning the theme does not define skips it instead of failing, so a caller can
	/// ask for the full semantic set against a partial theme.
	/// </summary>
	[TestMethod]
	public void MapColors_ForUndefinedMeaning_SkipsIt()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Primary] = [Color.FromHex("#89b4fa")],
			},
			isDark: true);

		IReadOnlyDictionary<SemanticColorRequest, Color> result = SemanticColorMapper.MapColors(
			[
				new(SemanticMeaning.Primary, Priority.Medium),
				new(SemanticMeaning.Error, Priority.Medium),
			],
			theme);

		Assert.IsTrue(result.ContainsKey(new(SemanticMeaning.Primary, Priority.Medium)));
		Assert.IsFalse(result.ContainsKey(new(SemanticMeaning.Error, Priority.Medium)));
	}

	/// <summary>
	/// A meaning present but with no colors is skipped the same way.
	/// </summary>
	[TestMethod]
	public void MapColors_ForMeaningWithNoColors_SkipsIt()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Primary] = [],
			},
			isDark: true);

		Assert.IsEmpty(SemanticColorMapper.MapColors([new(SemanticMeaning.Primary, Priority.Medium)], theme));
	}

	/// <summary>
	/// A request for one meaning still produces every priority level for it, because callers rely on
	/// the mapper to fill out a whole ramp from a single ask.
	/// </summary>
	[TestMethod]
	public void MapColors_ForOneRequest_ReturnsEveryPriorityOfThatMeaning()
	{
		ISemanticTheme theme = ThemeRegistry.AllThemes[0].CreateInstance();

		IReadOnlyDictionary<SemanticColorRequest, Color> result =
			SemanticColorMapper.MapColors([new(SemanticMeaning.Primary, Priority.Medium)], theme);

		foreach (Priority priority in Enum.GetValues<Priority>())
		{
			Assert.IsTrue(
				result.ContainsKey(new(SemanticMeaning.Primary, priority)),
				$"Missing Primary at {priority}");
		}
	}

	/// <summary>
	/// A meaning with a single source color is extrapolated across the priority ramp, producing
	/// distinct lightness levels rather than repeating that one color.
	/// </summary>
	[TestMethod]
	public void MapColors_WithSingleSourceColor_ExtrapolatesDistinctLightness()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Neutral] = [Color.FromHex("#cdd6f4"), Color.FromHex("#11111b")],
				[SemanticMeaning.Primary] = [Color.FromHex("#89b4fa")],
			},
			isDark: true);

		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);

		double lowest = palette[new(SemanticMeaning.Primary, Priority.VeryLow)].ToOklab().L;
		double highest = palette[new(SemanticMeaning.Primary, Priority.VeryHigh)].ToOklab().L;

		Assert.IsGreaterThan(lowest, highest, "A dark theme must brighten as priority rises");
	}

	/// <summary>
	/// A light theme must run the ramp the other way: higher priority means darker.
	/// </summary>
	[TestMethod]
	public void MapColors_ForLightTheme_DarkensAsPriorityRises()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Neutral] = [Color.FromHex("#dce0e8"), Color.FromHex("#4c4f69")],
				[SemanticMeaning.Primary] = [Color.FromHex("#1e66f5")],
			},
			isDark: false);

		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);

		double lowest = palette[new(SemanticMeaning.Primary, Priority.VeryLow)].ToOklab().L;
		double highest = palette[new(SemanticMeaning.Primary, Priority.VeryHigh)].ToOklab().L;

		Assert.IsLessThan(lowest, highest, "A light theme must darken as priority rises");
	}

	/// <summary>
	/// Every color the mapper emits for every registered theme must be inside the sRGB gamut.
	/// The mapper reduces chroma to stay in gamut, and this pins that it always succeeds.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_ForEveryTheme_StaysInGamut()
	{
		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			IReadOnlyDictionary<SemanticColorRequest, Color> palette =
				SemanticColorMapper.MakeCompletePalette(info.CreateInstance());

			Assert.IsNotEmpty(palette);

			foreach (KeyValuePair<SemanticColorRequest, Color> entry in palette)
			{
				Color c = entry.Value;
				string label = $"{info.Name}/{entry.Key}";
				Assert.IsTrue(c.R is >= 0.0 and <= 1.0, $"{label}: red out of gamut ({c.R:F4})");
				Assert.IsTrue(c.G is >= 0.0 and <= 1.0, $"{label}: green out of gamut ({c.G:F4})");
				Assert.IsTrue(c.B is >= 0.0 and <= 1.0, $"{label}: blue out of gamut ({c.B:F4})");
			}
		}
	}

	/// <summary>
	/// A complete palette must contain one color per meaning-and-priority combination the theme
	/// defines.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_HasOneEntryPerMeaningAndPriority()
	{
		ISemanticTheme theme = ThemeRegistry.AllThemes[0].CreateInstance();
		int priorities = Enum.GetValues<Priority>().Length;

		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);

		Assert.HasCount(theme.SemanticMapping.Count * priorities, palette);
	}
}
