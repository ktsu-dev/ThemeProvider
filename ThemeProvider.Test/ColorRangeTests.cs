// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using ktsu.Semantics.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies <see cref="ColorRange"/> ordering, which decides the direction the priority ramp runs in.
/// </summary>
[TestClass]
public class ColorRangeTests
{
	private static readonly Color Dark = Color.FromHex("#11111b");
	private static readonly Color Light = Color.FromHex("#cdd6f4");

	/// <summary>
	/// A range between two different colors reports a positive perceptual distance.
	/// </summary>
	[TestMethod]
	public void Distance_BetweenDistinctColors_IsPositive()
	{
		ColorRange range = new(Dark, Light);

		Assert.IsGreaterThan(0.0, range.Distance);
	}

	/// <summary>
	/// A range whose endpoints are the same color is reported as a single color.
	/// </summary>
	[TestMethod]
	public void IsSingleColor_WhenEndpointsMatch_IsTrue()
	{
		ColorRange range = new(Dark, Dark);

		Assert.IsTrue(range.IsSingleColor);
		Assert.AreEqual(0.0, range.Distance, 1e-9);
	}

	/// <summary>
	/// A range spanning two distinct colors is not a single color.
	/// </summary>
	[TestMethod]
	public void IsSingleColor_WhenEndpointsDiffer_IsFalse()
	{
		ColorRange range = new(Dark, Light);

		Assert.IsFalse(range.IsSingleColor);
	}

	/// <summary>
	/// For a dark theme the range runs dark to light, whichever order the caller supplied.
	/// </summary>
	[TestMethod]
	public void FromColors_DarkTheme_OrdersDarkToLight()
	{
		ColorRange ascending = ColorRange.FromColors(Dark, Light, isDarkTheme: true);
		ColorRange descending = ColorRange.FromColors(Light, Dark, isDarkTheme: true);

		Assert.IsLessThan(ascending.End.ToOklab().L, ascending.Start.ToOklab().L);
		Assert.IsLessThan(descending.End.ToOklab().L, descending.Start.ToOklab().L);
		Assert.AreEqual(ascending, descending, "Argument order must not change the result");
	}

	/// <summary>
	/// For a light theme the range runs light to dark, whichever order the caller supplied.
	/// </summary>
	[TestMethod]
	public void FromColors_LightTheme_OrdersLightToDark()
	{
		ColorRange ascending = ColorRange.FromColors(Dark, Light, isDarkTheme: false);
		ColorRange descending = ColorRange.FromColors(Light, Dark, isDarkTheme: false);

		Assert.IsGreaterThan(ascending.End.ToOklab().L, ascending.Start.ToOklab().L);
		Assert.IsGreaterThan(descending.End.ToOklab().L, descending.Start.ToOklab().L);
		Assert.AreEqual(ascending, descending, "Argument order must not change the result");
	}

	/// <summary>
	/// The two-argument overload defaults to dark-theme ordering.
	/// </summary>
	[TestMethod]
	public void FromColors_DefaultOverload_UsesDarkThemeOrdering() =>
		Assert.AreEqual(
			ColorRange.FromColors(Light, Dark, isDarkTheme: true),
			ColorRange.FromColors(Light, Dark));

	/// <summary>
	/// Ordering two identical colors is stable regardless of theme type.
	/// </summary>
	[TestMethod]
	public void FromColors_IdenticalColors_ReturnsSingleColorRange()
	{
		Assert.IsTrue(ColorRange.FromColors(Dark, Dark, isDarkTheme: true).IsSingleColor);
		Assert.IsTrue(ColorRange.FromColors(Dark, Dark, isDarkTheme: false).IsSingleColor);
	}
}
