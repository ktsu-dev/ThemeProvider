// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies the registry's catalog queries and the consistency of the metadata it publishes
/// against the theme instances it creates.
/// </summary>
[TestClass]
public class ThemeRegistryTests
{
	/// <summary>
	/// The registry must expose themes, and each entry must be fully populated.
	/// </summary>
	[TestMethod]
	public void AllThemes_EntriesAreWellFormed()
	{
		Assert.IsNotEmpty(ThemeRegistry.AllThemes);

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			Assert.IsFalse(string.IsNullOrWhiteSpace(info.Name), "Theme name must be set");
			Assert.IsFalse(string.IsNullOrWhiteSpace(info.Family), $"{info.Name}: family must be set");
			Assert.IsFalse(string.IsNullOrWhiteSpace(info.Description), $"{info.Name}: description must be set");
			Assert.IsNotNull(info.CreateInstance, $"{info.Name}: factory must be set");
		}
	}

	/// <summary>
	/// Theme names are the registry's lookup key, so they must be unique.
	/// </summary>
	[TestMethod]
	public void AllThemes_NamesAreUnique()
	{
		List<string> duplicates = [.. ThemeRegistry.AllThemes
			.GroupBy(t => t.Name, StringComparer.OrdinalIgnoreCase)
			.Where(g => g.Count() > 1)
			.Select(g => g.Key)];

		Assert.IsEmpty(duplicates, $"Duplicate theme names: {string.Join(", ", duplicates)}");
	}

	/// <summary>
	/// The <c>IsDark</c> flag published as metadata must agree with what the theme instance reports.
	/// A mismatch would make the registry's dark/light queries lie about the themes they return.
	/// </summary>
	[TestMethod]
	public void AllThemes_MetadataIsDarkMatchesInstance()
	{
		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			ISemanticTheme instance = info.CreateInstance();
			Assert.AreEqual(info.IsDark, instance.IsDarkTheme, $"{info.Name}: IsDark metadata disagrees with instance");
		}
	}

	/// <summary>
	/// The dark and light partitions must together account for every theme exactly once.
	/// </summary>
	[TestMethod]
	public void DarkAndLightThemes_PartitionAllThemes()
	{
		Assert.AreEqual(
			ThemeRegistry.AllThemes.Count,
			ThemeRegistry.DarkThemes.Count + ThemeRegistry.LightThemes.Count,
			"Dark and light themes must partition the catalog");

		Assert.IsTrue(ThemeRegistry.DarkThemes.All(t => t.IsDark), "DarkThemes contains a light theme");
		Assert.IsTrue(ThemeRegistry.LightThemes.All(t => !t.IsDark), "LightThemes contains a dark theme");
	}

	/// <summary>
	/// Grouping by family must cover every theme and match the distinct family list.
	/// </summary>
	[TestMethod]
	public void ThemesByFamily_CoversEveryThemeAndMatchesFamilies()
	{
		Assert.HasCount(ThemeRegistry.Families.Count, ThemeRegistry.ThemesByFamily);
		Assert.AreEqual(
			ThemeRegistry.AllThemes.Count,
			ThemeRegistry.ThemesByFamily.Values.Sum(v => v.Count),
			"Family grouping must account for every theme");

		foreach (string family in ThemeRegistry.Families)
		{
			Assert.IsTrue(ThemeRegistry.ThemesByFamily.ContainsKey(family), $"Missing family group: {family}");
		}
	}

	/// <summary>
	/// Families are published sorted so callers can render a stable list.
	/// </summary>
	[TestMethod]
	public void Families_AreSortedAndDistinct()
	{
		Assert.AreSequenceEqual(
			ThemeRegistry.Families.Order(StringComparer.Ordinal),
			ThemeRegistry.Families,
			"Families must be sorted");

		Assert.HasCount(ThemeRegistry.Families.Distinct().Count(), ThemeRegistry.Families);
	}

	/// <summary>
	/// Lookup by name is case-insensitive.
	/// </summary>
	[TestMethod]
	public void FindTheme_IsCaseInsensitive()
	{
		string name = ThemeRegistry.AllThemes[0].Name;

		Assert.IsNotNull(ThemeRegistry.FindTheme(name));
		Assert.IsNotNull(ThemeRegistry.FindTheme(name.ToUpperInvariant()));
		Assert.IsNotNull(ThemeRegistry.FindTheme(name.ToLowerInvariant()));
	}

	/// <summary>
	/// An unknown name yields null rather than throwing.
	/// </summary>
	[TestMethod]
	public void FindTheme_UnknownName_ReturnsNull() =>
		Assert.IsNull(ThemeRegistry.FindTheme("no such theme"));

	/// <summary>
	/// Every theme returned for a family must actually belong to it.
	/// </summary>
	[TestMethod]
	public void GetThemesInFamily_ReturnsOnlyThatFamily()
	{
		foreach (string family in ThemeRegistry.Families)
		{
			IReadOnlyList<ThemeRegistry.ThemeInfo> themes = ThemeRegistry.GetThemesInFamily(family);
			Assert.IsNotEmpty(themes, $"{family}: expected at least one theme");
			Assert.IsTrue(themes.All(t => t.Family == family), $"{family}: returned a theme from another family");
		}
	}

	/// <summary>
	/// An unknown family yields an empty list rather than throwing.
	/// </summary>
	[TestMethod]
	public void GetThemesInFamily_UnknownFamily_ReturnsEmpty() =>
		Assert.IsEmpty(ThemeRegistry.GetThemesInFamily("no such family"));

	/// <summary>
	/// The bulk factory must instantiate every registered theme.
	/// </summary>
	[TestMethod]
	public void CreateAllThemeInstances_InstantiatesEveryTheme()
	{
		IReadOnlyList<ISemanticTheme> instances = ThemeRegistry.CreateAllThemeInstances();

		Assert.HasCount(ThemeRegistry.AllThemes.Count, instances);
		Assert.IsTrue(instances.All(i => i is not null), "Every theme must instantiate");
	}

	/// <summary>
	/// The per-family factory must instantiate exactly that family's themes.
	/// </summary>
	[TestMethod]
	public void CreateThemeInstancesInFamily_MatchesFamilySize()
	{
		foreach (string family in ThemeRegistry.Families)
		{
			Assert.HasCount(
				ThemeRegistry.GetThemesInFamily(family).Count,
				ThemeRegistry.CreateThemeInstancesInFamily(family),
				$"{family}: instance count must match metadata count");
		}
	}

	/// <summary>
	/// The factory must hand out a fresh instance per call, so a caller mutating the mapping it
	/// receives cannot affect anyone else's copy of the theme.
	/// </summary>
	[TestMethod]
	public void CreateInstance_ReturnsDistinctInstances()
	{
		ThemeRegistry.ThemeInfo info = ThemeRegistry.AllThemes[0];

		Assert.AreNotSame(info.CreateInstance(), info.CreateInstance());
	}
}
