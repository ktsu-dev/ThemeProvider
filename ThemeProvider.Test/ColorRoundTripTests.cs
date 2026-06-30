// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Test;

using System;
using System.Numerics;
using ktsu.Semantics.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Verifies that <see cref="Color.FromHex"/> followed by <see cref="Color.ToSrgbVector4"/> reproduces
/// the original sRGB byte values within one LSB (1/255). This pins the display-stability guarantee:
/// the gamma fix in ktsu.Semantics.Color leaves base theme colors visually identical.
/// </summary>
[TestClass]
public class ColorRoundTripTests
{
	private const float Tolerance = 1f / 255f;

	private static void AssertRoundTrip(string hex)
	{
		Color color = Color.FromHex(hex);
		Vector4 srgb = color.ToSrgbVector4();

		string h = hex.StartsWith('#') ? hex[1..] : hex;
		byte expectedR = Convert.ToByte(h[..2], 16);
		byte expectedG = Convert.ToByte(h.Substring(2, 2), 16);
		byte expectedB = Convert.ToByte(h.Substring(4, 2), 16);

		Assert.AreEqual(expectedR / 255.0, srgb.X, Tolerance, $"Red channel mismatch for {hex}");
		Assert.AreEqual(expectedG / 255.0, srgb.Y, Tolerance, $"Green channel mismatch for {hex}");
		Assert.AreEqual(expectedB / 255.0, srgb.Z, Tolerance, $"Blue channel mismatch for {hex}");
	}

	[TestMethod]
	public void MochaBase_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#1e1e2e");
	}

	[TestMethod]
	public void MochaText_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#cdd6f4");
	}

	[TestMethod]
	public void MochaCrust_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#11111b");
	}

	[TestMethod]
	public void MochaBlue_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#89b4fa");
	}

	[TestMethod]
	public void GruvboxDark0_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#282828");
	}

	[TestMethod]
	public void GruvboxLight1_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#ebdbb2");
	}

	[TestMethod]
	public void GruvboxBrightRed_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#fb4934");
	}

	[TestMethod]
	public void NordBase_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#2e3440");
	}

	[TestMethod]
	public void NordSnowStorm6_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#eceff4");
	}

	[TestMethod]
	public void NordAuroraRed_FromHex_SrgbRoundTripWithinOnePart255()
	{
		AssertRoundTrip("#bf616a");
	}
}
