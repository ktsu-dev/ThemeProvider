// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System.Collections.Generic;
using System.Linq;

using Hexa.NET.ImGui;

using ktsu.ThemeProvider;

/// <summary>The pictures in the gallery, in the order the index shows them.</summary>
/// <remarks>
/// One overview of every theme, then one picture per <see cref="ThemeRegistry.AllThemes"/> entry in
/// registry order. A theme added to the registry gets a picture with no change here.
/// </remarks>
internal static class GalleryCatalog
{
	/// <summary>Gets every entry.</summary>
	internal static IReadOnlyList<GalleryEntry> Entries { get; } =
	[
		new GalleryEntry(
			"Overview",
			"Every theme on one page, each drawn in its own background colour with its name in its own text colour, "
				+ "followed by its neutral ramp from very low to very high priority and one chip for each accent meaning "
				+ "(primary, alternate, success, call to action, information, caution, warning, error, failure and debug) at medium priority.",
			ThemeOverview.Width,
			ThemeOverview.Height,
			ImGui.StyleColorsDark,
			ThemeOverview.Draw),
		.. ThemeRegistry.AllThemes.Select(ForTheme),
	];

	private static GalleryEntry ForTheme(ThemeRegistry.ThemeInfo info)
	{
		SampleWindow window = new(info);
		return new GalleryEntry(
			info.Name,
			$"{info.Description.TrimEnd('.')}. {info.Family} family, {(info.IsDark ? "dark" : "light")}.",
			SampleWindow.Width,
			SampleWindow.Height,
			() => ThemeStyle.Apply(info.CreateInstance()),
			window.Draw);
	}
}
