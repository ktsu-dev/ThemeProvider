// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Themes.Everforest;

/// <summary>
/// The two Everforest palettes. Each contrast variant (hard, medium, soft) shares every accent with
/// the others of its brightness and differs only in its dimmest background, so the accents are
/// declared once here rather than once per variant.
/// </summary>
internal static class EverforestPalette
{
	// Hex values are the palette these themes mirror; the trailing comment on each line is
	// that palette's own name for the color.
	private const string DarkRed = "#e67e80";
	private const string DarkAqua = "#83c092";
	private const string DarkPurple = "#d699b6";
	private const string LightRed = "#f85552";
	private const string LightAqua = "#35a77c";
	private const string LightPurple = "#df69ba";

	/// <summary>
	/// Builds the dark palette over the given dimmest background.
	/// </summary>
	/// <param name="backgroundDim">The variant's BgDim color.</param>
	/// <returns>The dark palette.</returns>
	internal static SemanticPalette Dark(HexColor backgroundDim) => new()
	{
		Neutrals = ["#d3c6aa", backgroundDim], // Fg, BgDim
		Primary = "#a7c080", // Green
		Alternate = DarkAqua,
		Success = "#7fbbb3", // Blue
		CallToAction = DarkAqua,
		Information = DarkPurple,
		Caution = "#dbbc7f", // Yellow
		Warning = "#e69875", // Orange
		Error = DarkRed,
		Failure = DarkRed,
		Debug = DarkPurple,
	};

	/// <summary>
	/// Builds the light palette over the given dimmest background.
	/// </summary>
	/// <param name="backgroundDim">The variant's BgDim color.</param>
	/// <returns>The light palette.</returns>
	internal static SemanticPalette Light(HexColor backgroundDim) => new()
	{
		Neutrals = ["#5c6a72", backgroundDim], // Fg, BgDim
		Primary = "#8da101", // Green
		Alternate = LightAqua,
		Success = "#3a94c5", // Blue
		CallToAction = LightAqua,
		Information = LightPurple,
		Caution = "#dfa000", // Yellow
		Warning = "#f57d26", // Orange
		Error = LightRed,
		Failure = LightRed,
		Debug = LightPurple,
	};
}
