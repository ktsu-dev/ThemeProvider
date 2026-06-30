// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ktsu.Semantics.Color;

/// <summary>
/// Maps semantic color requests to actual colors using a global lightness-based priority system.
/// All colors with the same priority across different semantic meanings will have similar lightness levels.
/// </summary>
public sealed class SemanticColorMapper
{
	/// <summary>
	/// Maps a collection of semantic color requests to actual colors using the provided theme.
	/// Returns a complete mapping for all priority levels of the semantic meanings that were requested.
	/// Uses a global lightness range divided by priority levels to ensure consistent visual hierarchy.
	/// </summary>
	/// <param name="requests">The collection of semantic color requests to map</param>
	/// <param name="theme">The semantic theme providing available colors for each meaning</param>
	/// <returns>A dictionary mapping each request to its assigned color, including all priority levels for the requested semantic meanings</returns>
	public static IReadOnlyDictionary<SemanticColorRequest, Color> MapColors(
		IEnumerable<SemanticColorRequest> requests,
		ISemanticTheme theme)
	{
		Ensure.NotNull(requests);
		Ensure.NotNull(theme);

		List<SemanticColorRequest> requestsList = [.. requests];
		if (requestsList.Count == 0)
		{
			return new Dictionary<SemanticColorRequest, Color>();
		}

		// Calculate the global lightness range for use by all semantics
		(double globalMinLightness, double globalMaxLightness) = CalculateGlobalLightnessRange(theme);

		// Always use ALL possible priority levels for consistent mapping
#if NET5_0_OR_GREATER
		Priority[] allPriorities = Enum.GetValues<Priority>();
#else
		Priority[] allPriorities = (Priority[])Enum.GetValues(typeof(Priority));
#endif
		List<Priority> priorityLevels = [.. allPriorities.OrderBy(p => p)];

		Dictionary<SemanticColorRequest, Color> result = [];

		// Get all unique semantic meanings from the requests
		HashSet<SemanticMeaning> requestedMeanings = [.. requestsList.Select(r => r.Meaning)];

		// Generate complete mappings for all priority levels of each requested semantic meaning
		foreach (SemanticMeaning meaning in requestedMeanings)
		{
			// Get available colors for this semantic meaning
			if (!theme.SemanticMapping.TryGetValue(meaning, out Collection<Color>? availableColors) ||
				availableColors.Count == 0)
			{
				continue; // Skip if no colors available for this meaning
			}

			// Generate colors for ALL priority levels for this semantic meaning
			foreach (Priority priority in allPriorities)
			{
				SemanticColorRequest fullRequest = new(meaning, priority);

				// Calculate target lightness based on whether this is neutral or non-neutral
				double targetLightness = CalculateTargetLightnessForSemantic(
					priority, meaning, globalMinLightness, globalMaxLightness, theme.IsDarkTheme);

				// Generate the color using interpolation/extrapolation
				Color color = InterpolateToTargetLightness(availableColors, targetLightness);

				result[fullRequest] = color;
			}
		}

		return result;
	}

	/// <summary>
	/// Generates a complete palette containing all possible combinations of semantic meanings and priorities from the theme.
	/// This provides every color that can be requested from the theme, useful for theme exploration, previews, and pre-generation.
	/// </summary>
	/// <param name="theme">The semantic theme to generate the complete palette from</param>
	/// <returns>A dictionary mapping every possible semantic color request to its assigned color</returns>
	public static IReadOnlyDictionary<SemanticColorRequest, Color> MakeCompletePalette(ISemanticTheme theme)
	{
		Ensure.NotNull(theme);

		// Get all available semantic meanings from the theme
		HashSet<SemanticMeaning> availableMeanings = [.. theme.SemanticMapping.Keys];

		if (availableMeanings.Count == 0)
		{
			return new Dictionary<SemanticColorRequest, Color>();
		}

		// Get all possible priorities
#if NET5_0_OR_GREATER
		Priority[] allPriorities = Enum.GetValues<Priority>();
#else
		Priority[] allPriorities = (Priority[])Enum.GetValues(typeof(Priority));
#endif

		// Generate requests for all combinations of meanings and priorities
		List<SemanticColorRequest> allPossibleRequests = [];
		foreach (SemanticMeaning meaning in availableMeanings)
		{
			foreach (Priority priority in allPriorities)
			{
				allPossibleRequests.Add(new SemanticColorRequest(meaning, priority));
			}
		}

		// Use the existing MapColors method to generate the complete palette
		// This ensures consistency with individual color requests
		return MapColors(allPossibleRequests, theme);
	}

	/// <summary>
	/// Calculates the lightness range across all available semantics in the theme.
	/// This range will be used as the basis for all semantic color mappings.
	/// </summary>
	private static (double min, double max) CalculateGlobalLightnessRange(ISemanticTheme theme)
	{
		double globalMin = double.MaxValue;
		double globalMax = double.MinValue;

		foreach (KeyValuePair<SemanticMeaning, Collection<Color>> kvp in theme.SemanticMapping)
		{
			Collection<Color> colors = kvp.Value;
			for (int i = 0; i < colors.Count; i++)
			{
				double l = colors[i].ToOklab().L;
				if (l < globalMin)
				{
					globalMin = l;
				}
				if (l > globalMax)
				{
					globalMax = l;
				}
			}
		}

		// Ensure we have a valid range
		if (globalMin == double.MaxValue || globalMax == double.MinValue)
		{
			return (0.0, 1.0); // Fallback range
		}

		return (globalMin, globalMax);
	}

	/// <summary>
	/// Calculates the target lightness for a specific semantic meaning and priority.
	/// Neutral semantics use the full global range, while non-neutral semantics use 50-90% of it.
	/// </summary>
	private static double CalculateTargetLightnessForSemantic(
		Priority priority,
		SemanticMeaning meaning,
		double globalMinLightness,
		double globalMaxLightness,
		bool isDarkTheme)
	{
		// Get all priorities and find the position of the current priority
#if NET5_0_OR_GREATER
		Priority[] allPriorities = Enum.GetValues<Priority>();
#else
		Priority[] allPriorities = (Priority[])Enum.GetValues(typeof(Priority));
#endif
		int priorityIndex = Array.IndexOf(allPriorities, priority);

		if (allPriorities.Length == 1)
		{
			double globalCenter = (globalMinLightness + globalMaxLightness) / 2.0;
			return meaning == SemanticMeaning.Neutral ? globalCenter : globalCenter;
		}

		// Calculate position in range (0.0 to 1.0)
		double position = priorityIndex / (double)(allPriorities.Length - 1);

		// Determine the lightness range to use
		double minLightness, maxLightness;
		if (meaning == SemanticMeaning.Neutral)
		{
			// Neutral uses the full global range
			minLightness = globalMinLightness;
			maxLightness = globalMaxLightness;
		}
		else
		{
			// Non-neutral uses 50-90% of the global range
			double globalRange = globalMaxLightness - globalMinLightness;
			double rangeStart = globalMinLightness + (globalRange * 0.5); // 50%
			double rangeEnd = globalMinLightness + (globalRange * 0.9);   // 90%

			minLightness = rangeStart;
			maxLightness = rangeEnd;
		}

		double lightnessRange = maxLightness - minLightness;

		// Calculate target lightness based on theme type
		double targetLightness;
		if (isDarkTheme)
		{
			// In dark themes, higher priority (later in enum) gets higher lightness (more visible)
			targetLightness = minLightness + (position * lightnessRange);
		}
		else
		{
			// In light themes, higher priority (later in enum) gets lower lightness (more visible)
			targetLightness = maxLightness - (position * lightnessRange);
		}

		return Math.Max(0.0, Math.Min(targetLightness, 1.0));
	}

	/// <summary>
	/// Creates a color with the target lightness by interpolating or extrapolating from available colors.
	/// Preserves hue and chroma characteristics while achieving the exact lightness needed for priority hierarchy.
	/// </summary>
	private static Color InterpolateToTargetLightness(
		Collection<Color> availableColors,
		double targetLightness)
	{
		if (availableColors.Count == 0)
		{
			throw new ArgumentException("No colors available", nameof(availableColors));
		}

		if (availableColors.Count == 1)
		{
			// Single color - extrapolate by adjusting lightness while preserving hue and chroma
			return ExtrapolateColorToLightness(availableColors[0], targetLightness);
		}

		// Multiple colors - find the best interpolation or extrapolation
		// Precompute lightness values to avoid repeated ToOklab() calls in OrderBy
		List<(Color color, double l)> colorsWithL = [.. availableColors.Select(c => (color: c, l: c.ToOklab().L)).OrderBy(t => t.l)];

		double minLightness = colorsWithL[0].l;
		double maxLightness = colorsWithL[^1].l;

		// If target is within the range, interpolate between the closest colors
		if (targetLightness >= minLightness && targetLightness <= maxLightness)
		{
			return InterpolateBetweenColors(colorsWithL, targetLightness);
		}

		// If target is outside the range, extrapolate from the closest end
		if (targetLightness < minLightness)
		{
			// Extrapolate from the darkest color
			return ExtrapolateColorToLightness(colorsWithL[0].color, targetLightness);
		}
		else
		{
			// Extrapolate from the lightest color
			return ExtrapolateColorToLightness(colorsWithL[^1].color, targetLightness);
		}
	}

	/// <summary>
	/// Extrapolates a single color to a target lightness while preserving its hue and chroma.
	/// </summary>
	private static Color ExtrapolateColorToLightness(Color baseColor, double targetLightness)
	{
		// Work in Oklab space for perceptually uniform lightness adjustment
		Oklab baseOklab = baseColor.ToOklab();

		// Start with the target lightness and original chroma
		Oklab targetOklab = new(
			L: Math.Max(0.0, Math.Min(targetLightness, 1.0)),
			A: baseOklab.A,
			B: baseOklab.B
		);

		// Convert to Color to check if it's in gamut
		Color targetColor = Color.FromOklab(targetOklab);

		// Check if RGB values are within valid range
		bool inGamut = targetColor.R >= 0.0 && targetColor.R <= 1.0 &&
				   targetColor.G >= 0.0 && targetColor.G <= 1.0 &&
				   targetColor.B >= 0.0 && targetColor.B <= 1.0;

		if (inGamut)
		{
			// Color is in gamut, use it directly
			return targetColor;
		}

		// Color is out of gamut, we need to find the best in-gamut color
		// by reducing chroma while maintaining the target lightness
		double originalChroma = Math.Sqrt((baseOklab.A * baseOklab.A) + (baseOklab.B * baseOklab.B));
		double hue = Math.Atan2(baseOklab.B, baseOklab.A);

		// Binary search for the maximum chroma that keeps us in gamut
		double minChroma = 0.0;
		double maxChroma = originalChroma;
		const double tolerance = 0.001;
		const int maxIterations = 20;

		Oklab bestOklab = targetOklab;

		for (int i = 0; i < maxIterations && (maxChroma - minChroma) > tolerance; i++)
		{
			double testChroma = (minChroma + maxChroma) / 2.0;

			// Create color with reduced chroma
			Oklab testOklab = new(
				L: targetLightness,
				A: testChroma * Math.Cos(hue),
				B: testChroma * Math.Sin(hue)
			);

			Color testColor = Color.FromOklab(testOklab);

			bool testInGamut = testColor.R >= 0.0 && testColor.R <= 1.0 &&
						   testColor.G >= 0.0 && testColor.G <= 1.0 &&
						   testColor.B >= 0.0 && testColor.B <= 1.0;

			if (testInGamut)
			{
				// This chroma works, try higher
				minChroma = testChroma;
				bestOklab = testOklab;
			}
			else
			{
				// This chroma is too high, try lower
				maxChroma = testChroma;
			}
		}

		// Convert the best in-gamut color to the final Color
		Color bestColor = Color.FromOklab(bestOklab);

		// Final safety clamp (should not be needed if binary search worked correctly)
		return Color.FromLinear(
			Math.Max(0.0, Math.Min(bestColor.R, 1.0)),
			Math.Max(0.0, Math.Min(bestColor.G, 1.0)),
			Math.Max(0.0, Math.Min(bestColor.B, 1.0)),
			bestColor.A
		);
	}

	/// <summary>
	/// Interpolates between colors in a sorted list to achieve a target lightness.
	/// </summary>
	private static Color InterpolateBetweenColors(List<(Color color, double l)> sortedColors, double targetLightness)
	{
		// Find the two colors that bracket the target lightness
		(Color color, double l) lower = sortedColors[0];
		(Color color, double l) upper = sortedColors[^1];

		for (int i = 0; i < sortedColors.Count - 1; i++)
		{
			if (targetLightness >= sortedColors[i].l && targetLightness <= sortedColors[i + 1].l)
			{
				lower = sortedColors[i];
				upper = sortedColors[i + 1];
				break;
			}
		}

		// Calculate interpolation factor
		double lightnessRange = upper.l - lower.l;
		double t = lightnessRange == 0 ? 0.5 : (targetLightness - lower.l) / lightnessRange;
		t = Math.Max(0.0, Math.Min(t, 1.0));

		// Interpolate in Oklab space
		Color interpolated = lower.color.MixOklab(upper.color, t);

		// Clamp RGB values to valid range
		return Color.FromLinear(
			Math.Max(0.0, Math.Min(interpolated.R, 1.0)),
			Math.Max(0.0, Math.Min(interpolated.G, 1.0)),
			Math.Max(0.0, Math.Min(interpolated.B, 1.0)),
			interpolated.A
		);
	}
}
