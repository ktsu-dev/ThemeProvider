// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ktsu.Semantics.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies <see cref="HexColor"/> conversion and the mapping <see cref="SemanticPalette"/> builds
/// from it — the path every theme in the library now goes through.
/// </summary>
[TestClass]
public class SemanticPaletteTests
{
	private static SemanticPalette CreatePalette() => new()
	{
		Neutrals = ["#cdd6f4", "#11111b"],
		Primary = "#89b4fa",
		Alternate = "#f5c2e7",
		Success = "#a6e3a1",
		CallToAction = "#a6e3a1",
		Information = "#74c7ec",
		Caution = "#eba0ac",
		Warning = "#fab387",
		Error = "#f38ba8",
		Failure = "#f38ba8",
		Debug = "#cba6f7",
	};

	/// <summary>
	/// A hex string converts implicitly, and the named alternative agrees with it.
	/// </summary>
	[TestMethod]
	public void HexColor_ImplicitConversionAndFromString_Agree()
	{
		HexColor implicitly = "#89b4fa";

		Assert.AreEqual(HexColor.FromString("#89b4fa"), implicitly);
		Assert.AreEqual("#89b4fa", implicitly.Value);
	}

	/// <summary>
	/// Conversion to a color must round-trip back to the same hex notation.
	/// </summary>
	[TestMethod]
	public void HexColor_ToColor_RoundTripsThroughHex()
	{
		HexColor hex = "#89b4fa";

		Assert.AreEqual("#89b4fa", hex.ToColor().ToHex().ToLowerInvariant());
	}

	/// <summary>
	/// Rendering an assigned value yields the hex notation; an unassigned one yields an empty string
	/// rather than throwing, so diagnostics can print a palette that is still being built.
	/// </summary>
	[TestMethod]
	public void HexColor_ToString_ReturnsValueOrEmpty()
	{
		Assert.AreEqual("#89b4fa", HexColor.FromString("#89b4fa").ToString());
		Assert.AreEqual(string.Empty, default(HexColor).ToString());
	}

	/// <summary>
	/// Converting a never-assigned value is a programming error in a theme definition, so it fails
	/// loudly instead of silently producing black.
	/// </summary>
	[TestMethod]
	public void HexColor_ToColor_WhenUnassigned_Throws() =>
		Assert.ThrowsExactly<InvalidOperationException>(() => default(HexColor).ToColor());

	/// <summary>
	/// The palette must produce an entry for every semantic meaning.
	/// </summary>
	[TestMethod]
	public void ToSemanticMapping_CoversEverySemanticMeaning()
	{
		Dictionary<SemanticMeaning, Collection<Color>> mapping = CreatePalette().ToSemanticMapping();

		foreach (SemanticMeaning meaning in Enum.GetValues<SemanticMeaning>())
		{
			Assert.IsTrue(mapping.ContainsKey(meaning), $"Missing mapping for {meaning}");
			Assert.IsNotEmpty(mapping[meaning], $"{meaning} mapped to an empty collection");
		}
	}

	/// <summary>
	/// The neutral ramp keeps the order the theme declared it in; the mapper relies on nothing more
	/// than that both endpoints are present, but callers render them in order.
	/// </summary>
	[TestMethod]
	public void ToSemanticMapping_PreservesNeutralRampOrder()
	{
		Collection<Color> neutrals = CreatePalette().ToSemanticMapping()[SemanticMeaning.Neutral];

		Assert.HasCount(2, neutrals);
		Assert.AreEqual("#cdd6f4", neutrals[0].ToHex().ToLowerInvariant());
		Assert.AreEqual("#11111b", neutrals[1].ToHex().ToLowerInvariant());
	}

	/// <summary>
	/// Non-neutral meanings map to exactly the accent they were given.
	/// </summary>
	[TestMethod]
	public void ToSemanticMapping_MapsAccentsToDeclaredColors()
	{
		Dictionary<SemanticMeaning, Collection<Color>> mapping = CreatePalette().ToSemanticMapping();

		Assert.AreEqual("#89b4fa", mapping[SemanticMeaning.Primary][0].ToHex().ToLowerInvariant());
		Assert.AreEqual("#cba6f7", mapping[SemanticMeaning.Debug][0].ToHex().ToLowerInvariant());
	}

	/// <summary>
	/// Each call must hand back collections the caller owns. A shared instance would let one consumer
	/// mutating its mapping silently corrupt the theme for everyone else.
	/// </summary>
	[TestMethod]
	public void ToSemanticMapping_ReturnsIndependentCollectionsPerCall()
	{
		SemanticPalette palette = CreatePalette();

		Dictionary<SemanticMeaning, Collection<Color>> first = palette.ToSemanticMapping();
		Dictionary<SemanticMeaning, Collection<Color>> second = palette.ToSemanticMapping();

		Assert.AreNotSame(first, second);
		Assert.AreNotSame(first[SemanticMeaning.Neutral], second[SemanticMeaning.Neutral]);

		first[SemanticMeaning.Neutral].Clear();

		Assert.HasCount(2, second[SemanticMeaning.Neutral], "Mutating one mapping must not affect another");
	}

	/// <summary>
	/// Repeated calls must agree, which also exercises the cached-resolution path taken after the
	/// first call has parsed the hex values.
	/// </summary>
	[TestMethod]
	public void ToSemanticMapping_IsStableAcrossCalls()
	{
		SemanticPalette palette = CreatePalette();

		Dictionary<SemanticMeaning, Collection<Color>> first = palette.ToSemanticMapping();
		Dictionary<SemanticMeaning, Collection<Color>> second = palette.ToSemanticMapping();

		foreach (SemanticMeaning meaning in first.Keys)
		{
			Assert.AreSequenceEqual(
				first[meaning],
				second[meaning],
				$"{meaning} differed between calls");
		}
	}

	/// <summary>
	/// A palette with an empty neutral ramp still maps, leaving the mapper to fall back to its
	/// default lightness range.
	/// </summary>
	[TestMethod]
	public void ToSemanticMapping_WithEmptyNeutrals_ProducesEmptyNeutralCollection()
	{
		SemanticPalette palette = new()
		{
			Primary = "#89b4fa",
			Alternate = "#f5c2e7",
			Success = "#a6e3a1",
			CallToAction = "#a6e3a1",
			Information = "#74c7ec",
			Caution = "#eba0ac",
			Warning = "#fab387",
			Error = "#f38ba8",
			Failure = "#f38ba8",
			Debug = "#cba6f7",
		};

		Assert.IsEmpty(palette.ToSemanticMapping()[SemanticMeaning.Neutral]);
	}
}
