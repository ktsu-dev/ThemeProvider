// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Analysis;

using System.Collections.Generic;
using System.Linq;

/// <summary>The outcome of a single check.</summary>
internal enum CheckStatus
{
	/// <summary>The check met its gate.</summary>
	Pass,

	/// <summary>The check is within a soft tolerance but worth attention.</summary>
	Warn,

	/// <summary>The check failed its gate.</summary>
	Fail,
}

/// <summary>A single issue surfaced by a check (only warnings and failures are recorded).</summary>
/// <param name="Category">The check that produced the finding.</param>
/// <param name="Status">The severity.</param>
/// <param name="Message">A human-readable description with the measured value and gate.</param>
internal sealed record Finding(string Category, CheckStatus Status, string Message);

/// <summary>The headline numbers shown in the per-theme summary row. Arrows in the field
/// names indicate the direction that is worse (lower contrast, higher drift, etc.).</summary>
/// <param name="MinTextContrast">Lowest text-over-surface contrast ratio.</param>
/// <param name="MinTextPair">The foreground/background pair that produced it.</param>
/// <param name="MinGlyphContrast">Lowest UI-glyph contrast ratio.</param>
/// <param name="MinGlyphPair">The foreground/background pair that produced it.</param>
/// <param name="MinStateDelta">Smallest Oklab lightness delta between adjacent interaction states.</param>
/// <param name="MinStatePair">The interaction-state pair that produced it.</param>
/// <param name="MaxHueDrift">Largest accent hue drift from the source palette, in degrees.</param>
/// <param name="MinChromaRetention">Smallest accent chroma retention ratio.</param>
/// <param name="UnusedMeanings">Semantic meanings the theme defines but the ImGui mapper never consumes.</param>
internal sealed record ThemeMetrics(
	double MinTextContrast,
	string MinTextPair,
	double MinGlyphContrast,
	string MinGlyphPair,
	double MinStateDelta,
	string MinStatePair,
	double MaxHueDrift,
	double MinChromaRetention,
	IReadOnlyList<SemanticMeaning> UnusedMeanings);

/// <summary>The full analysis result for one theme.</summary>
/// <param name="Name">The theme's display name.</param>
/// <param name="Family">The theme family.</param>
/// <param name="IsDark">Whether the theme is dark.</param>
/// <param name="Metrics">The headline numbers.</param>
/// <param name="Findings">The warnings and failures.</param>
internal sealed record ThemeAnalysis(
	string Name,
	string Family,
	bool IsDark,
	ThemeMetrics Metrics,
	IReadOnlyList<Finding> Findings)
{
	/// <summary>The worst status across all findings.</summary>
	public CheckStatus Status =>
		Findings.Any(f => f.Status == CheckStatus.Fail) ? CheckStatus.Fail
		: Findings.Any(f => f.Status == CheckStatus.Warn) ? CheckStatus.Warn
		: CheckStatus.Pass;
}
