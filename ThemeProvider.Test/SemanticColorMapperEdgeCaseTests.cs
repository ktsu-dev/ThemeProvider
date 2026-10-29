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
	/// A theme that defines no meanings has no neutrals either, so it is refused like any other theme
	/// without them rather than mapped to an empty palette.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_WithEmptyTheme_Throws()
	{
		StubTheme theme = new([], isDark: true);

		ArgumentException ex = Assert.ThrowsExactly<ArgumentException>(() => SemanticColorMapper.MakeCompletePalette(theme));
		Assert.Contains(nameof(SemanticMeaning.Neutral), ex.Message);
	}

	/// <summary>
	/// A hand-written theme with accents but no Neutral key would map to a palette with no text or
	/// window background, so it is refused, naming the missing meaning.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_WithNoNeutralKey_Throws()
	{
		StubTheme theme = new(new() { [SemanticMeaning.Primary] = [Color.FromHex("#89b4fa")] }, isDark: true);

		ArgumentException ex = Assert.ThrowsExactly<ArgumentException>(() => SemanticColorMapper.MakeCompletePalette(theme));
		Assert.Contains(nameof(SemanticMeaning.Neutral), ex.Message);
	}

	/// <summary>
	/// An empty Neutral collection is the same gap as a missing key, and is refused the same way.
	/// </summary>
	[TestMethod]
	public void MakeCompletePalette_WithEmptyNeutrals_Throws()
	{
		StubTheme theme = new(
			new()
			{
				[SemanticMeaning.Neutral] = [],
				[SemanticMeaning.Primary] = [Color.FromHex("#89b4fa")],
			},
			isDark: true);

		ArgumentException ex = Assert.ThrowsExactly<ArgumentException>(() => SemanticColorMapper.MakeCompletePalette(theme));
		Assert.Contains(nameof(SemanticMeaning.Neutral), ex.Message);
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
	/// A meaning with a single source color keeps that color's alpha, the same way an interpolated
	/// neutral ramp does, rather than coming out opaque.
	/// </summary>
	[TestMethod]
	public void MapColors_WithSingleTranslucentSourceColor_KeepsItsAlpha()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Neutral] = [Color.FromHex("#10101080"), Color.FromHex("#f0f0f080")],
				[SemanticMeaning.Primary] = [Color.FromHex("#3366cc80")],
			},
			isDark: true);

		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);
		double sourceAlpha = Color.FromHex("#3366cc80").A;

		foreach (Priority priority in Enum.GetValues<Priority>())
		{
			Assert.AreEqual(
				sourceAlpha,
				palette[new(SemanticMeaning.Primary, priority)].A,
				1e-9,
				$"Primary at {priority} lost its source alpha");
		}
	}

	/// <summary>
	/// Extrapolating a color to its own lightness returns that color unchanged. Saturated sRGB
	/// primaries sit exactly on the gamut boundary, so the Oklab round trip leaves them a hair
	/// outside it, and an exact gamut test would wrongly shave their chroma (#ff0000 to #ff0201).
	/// </summary>
	/// <param name="hex">The source color.</param>
	[TestMethod]
	[DataRow("#FF0000")]
	[DataRow("#00FF00")]
	[DataRow("#0000FF")]
	[DataRow("#FFFF00")]
	[DataRow("#00FFFF")]
	[DataRow("#FF00FF")]
	[DataRow("#FFFFFF")]
	[DataRow("#000000")]
	[DataRow("#3366CC80")]
	public void ExtrapolateColorToLightness_AtItsOwnLightness_ReturnsTheSameColor(string hex)
	{
		Color source = Color.FromHex(hex);

		Color result = SemanticColorMapper.ExtrapolateColorToLightness(source, source.ToOklab().L);

		Assert.AreEqual(hex, result.ToHex());
	}

	/// <summary>
	/// Extrapolating to the very top or bottom of the lightness range lands on white or black. No
	/// chroma above zero is in gamut there, so the chroma search finds nothing, and it must fall
	/// back to the achromatic color rather than to the out-of-gamut target, whose clamp would tint
	/// it and pull it off the target lightness.
	/// </summary>
	/// <param name="hex">The source color.</param>
	/// <param name="targetLightness">The lightness to extrapolate to.</param>
	/// <param name="expected">The color that lightness must produce.</param>
	[TestMethod]
	[DataRow("#CDD6F4", 1.0, "#FFFFFF")]
	[DataRow("#FF0000", 1.0, "#FFFFFF")]
	[DataRow("#1E1E2E", 0.0, "#000000")]
	[DataRow("#FF0000", 0.0, "#000000")]
	public void ExtrapolateColorToLightness_ToTheEndOfTheRange_ReturnsWhiteOrBlack(
		string hex, double targetLightness, string expected)
	{
		Color result = SemanticColorMapper.ExtrapolateColorToLightness(Color.FromHex(hex), targetLightness);

		Assert.AreEqual(expected, result.ToHex());
	}

	/// <summary>
	/// A single white anywhere in a custom theme stretches the global lightness range to 1, so the
	/// top of the neutral ramp must come out as white at that lightness, not a tinted, darker color.
	/// </summary>
	[TestMethod]
	public void MapColors_WhenThemeContainsWhite_NeutralVeryHighIsWhite()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Neutral] = [Color.FromHex("#1e1e2e"), Color.FromHex("#cdd6f4")],
				[SemanticMeaning.Primary] = [Color.FromHex("#89b4fa")],
				[SemanticMeaning.Information] = [Color.FromHex("#ffffff")],
			},
			isDark: true);

		Color veryHigh = SemanticColorMapper.MakeCompletePalette(theme)[new(SemanticMeaning.Neutral, Priority.VeryHigh)];

		Assert.AreEqual("#FFFFFF", veryHigh.ToHex());
		Assert.AreEqual(1.0, veryHigh.ToOklab().L, 1e-3);
	}

	/// <summary>
	/// A single saturated neutral in a theme that also reaches black and white is extrapolated to
	/// both ends of the range, and its lightness must still rise with priority.
	/// </summary>
	[TestMethod]
	public void MapColors_SaturatedNeutralBetweenBlackAndWhite_LightnessIsMonotonic()
	{
		StubTheme theme = new(
			new Dictionary<SemanticMeaning, Collection<Color>>
			{
				[SemanticMeaning.Neutral] = [Color.FromHex("#ff0000")],
				[SemanticMeaning.Primary] = [Color.FromHex("#000000")],
				[SemanticMeaning.Information] = [Color.FromHex("#ffffff")],
			},
			isDark: true);

		IReadOnlyDictionary<SemanticColorRequest, Color> palette = SemanticColorMapper.MakeCompletePalette(theme);
		Priority[] priorities = Enum.GetValues<Priority>();

		for (int i = 1; i < priorities.Length; i++)
		{
			double lower = palette[new(SemanticMeaning.Neutral, priorities[i - 1])].ToOklab().L;
			double higher = palette[new(SemanticMeaning.Neutral, priorities[i])].ToOklab().L;
			Assert.IsTrue(
				higher >= lower,
				$"Neutral {priorities[i]} (L={higher:F4}) is darker than {priorities[i - 1]} (L={lower:F4})");
		}

		Assert.AreEqual("#000000", palette[new(SemanticMeaning.Neutral, Priority.VeryLow)].ToHex());
		Assert.AreEqual("#FFFFFF", palette[new(SemanticMeaning.Neutral, Priority.VeryHigh)].ToHex());
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
