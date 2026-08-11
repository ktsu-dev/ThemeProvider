// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

/// <summary>
/// Provides centralized access to all available themes with metadata.
/// </summary>
public static class ThemeRegistry
{
	/// <summary>
	/// Represents metadata information about a theme.
	/// </summary>
	/// <param name="Name">The display name of the theme</param>
	/// <param name="Family">The theme family (e.g., "Catppuccin", "Tokyo Night")</param>
	/// <param name="Variant">The variant within the family (e.g., "Mocha", "Storm", "Day")</param>
	/// <param name="IsDark">Whether this is a dark theme</param>
	/// <param name="Description">A brief description of the theme</param>
	/// <param name="CreateInstance">Factory function to create an instance of the theme</param>
	public record ThemeInfo(
		string Name,
		string Family,
		string? Variant,
		bool IsDark,
		string Description,
		Func<ISemanticTheme> CreateInstance
	);

	/// <summary>
	/// Gets all registered themes with their metadata.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1506:AvoidExcessiveClassCoupling",
		Justification = "Theme registry is designed to reference all available themes")]
	public static IReadOnlyList<ThemeInfo> AllThemes { get; } = [
		// Catppuccin Family (4 variants)
		new ThemeInfo("Catppuccin Latte", "Catppuccin", "Latte", false, "Warm light theme with pastel colors", () => new Themes.Catppuccin.Latte()),
		new ThemeInfo("Catppuccin Frappe", "Catppuccin", "Frappe", true, "Soft dark theme with muted pastels", () => new Themes.Catppuccin.Frappe()),
		new ThemeInfo("Catppuccin Macchiato", "Catppuccin", "Macchiato", true, "Medium-dark theme with vibrant accents", () => new Themes.Catppuccin.Macchiato()),
		new ThemeInfo("Catppuccin Mocha", "Catppuccin", "Mocha", true, "Rich dark theme with warm undertones", () => new Themes.Catppuccin.Mocha()),

		// Tokyo Night Family (3 variants)
		new ThemeInfo("Tokyo Night", "Tokyo Night", null, true, "Clean dark theme inspired by Tokyo's neon nights", () => new Themes.TokyoNight.TokyoNight()),
		new ThemeInfo("Tokyo Night Storm", "Tokyo Night", "Storm", true, "Softer contrast variant of Tokyo Night", () => new Themes.TokyoNight.TokyoNightStorm()),
		new ThemeInfo("Tokyo Night Day", "Tokyo Night", "Day", false, "Light variant with Tokyo Night's aesthetic", () => new Themes.TokyoNight.TokyoNightDay()),

		// Gruvbox Family (6 variants)
		new ThemeInfo("Gruvbox Dark", "Gruvbox", "Dark", true, "Retro groove colors with warm dark background", () => new Themes.Gruvbox.GruvboxDark()),
		new ThemeInfo("Gruvbox Dark Hard", "Gruvbox", "Dark Hard", true, "High contrast dark variant of Gruvbox", () => new Themes.Gruvbox.GruvboxDarkHard()),
		new ThemeInfo("Gruvbox Dark Soft", "Gruvbox", "Dark Soft", true, "Low contrast dark variant of Gruvbox", () => new Themes.Gruvbox.GruvboxDarkSoft()),
		new ThemeInfo("Gruvbox Light", "Gruvbox", "Light", false, "Retro groove colors with warm light background", () => new Themes.Gruvbox.GruvboxLight()),
		new ThemeInfo("Gruvbox Light Hard", "Gruvbox", "Light Hard", false, "High contrast light variant of Gruvbox", () => new Themes.Gruvbox.GruvboxLightHard()),
		new ThemeInfo("Gruvbox Light Soft", "Gruvbox", "Light Soft", false, "Low contrast light variant of Gruvbox", () => new Themes.Gruvbox.GruvboxLightSoft()),

		// Everforest Family (6 variants)
		new ThemeInfo("Everforest Dark", "Everforest", "Dark", true, "Green forest colors with comfortable dark background", () => new Themes.Everforest.EverforestDark()),
		new ThemeInfo("Everforest Dark Hard", "Everforest", "Dark Hard", true, "High contrast dark forest theme", () => new Themes.Everforest.EverforestDarkHard()),
		new ThemeInfo("Everforest Dark Soft", "Everforest", "Dark Soft", true, "Soft contrast dark forest theme", () => new Themes.Everforest.EverforestDarkSoft()),
		new ThemeInfo("Everforest Light", "Everforest", "Light", false, "Green forest colors with bright background", () => new Themes.Everforest.EverforestLight()),
		new ThemeInfo("Everforest Light Hard", "Everforest", "Light Hard", false, "High contrast light forest theme", () => new Themes.Everforest.EverforestLightHard()),
		new ThemeInfo("Everforest Light Soft", "Everforest", "Light Soft", false, "Soft contrast light forest theme", () => new Themes.Everforest.EverforestLightSoft()),

		// Nightfox Family (7 variants)
		new ThemeInfo("Nightfox", "Nightfox", null, true, "Vibrant dark theme with fox-inspired colors", () => new Themes.Nightfox.Nightfox()),
		new ThemeInfo("Dayfox", "Nightfox", "Dayfox", false, "Warm light variant of Nightfox", () => new Themes.Nightfox.Dayfox()),
		new ThemeInfo("Duskfox", "Nightfox", "Duskfox", true, "Muted dark variant with purple undertones", () => new Themes.Nightfox.Duskfox()),
		new ThemeInfo("Nordfox", "Nightfox", "Nordfox", true, "Nord-inspired arctic theme", () => new Themes.Nightfox.Nordfox()),
		new ThemeInfo("Terafox", "Nightfox", "Terafox", true, "Earthy terra-inspired variant", () => new Themes.Nightfox.Terafox()),
		new ThemeInfo("Carbonfox", "Nightfox", "Carbonfox", true, "Minimalist carbon-inspired theme", () => new Themes.Nightfox.Carbonfox()),
		new ThemeInfo("Dawnfox", "Nightfox", "Dawnfox", false, "Soft morning light variant", () => new Themes.Nightfox.Dawnfox()),

		// Kanagawa Family (3 variants)
		new ThemeInfo("Kanagawa Wave", "Kanagawa", "Wave", true, "Japanese-inspired theme based on 'The Great Wave'", () => new Themes.Kanagawa.KanagawaWave()),
		new ThemeInfo("Kanagawa Dragon", "Kanagawa", "Dragon", true, "Darker variant inspired by Japanese dragons", () => new Themes.Kanagawa.KanagawaDragon()),
		new ThemeInfo("Kanagawa Lotus", "Kanagawa", "Lotus", false, "Light zen garden inspired theme", () => new Themes.Kanagawa.KanagawaLotus()),

		// PaperColor Family (2 variants)
		new ThemeInfo("PaperColor Light", "PaperColor", "Light", false, "Material Design inspired light theme", () => new Themes.PaperColor.PaperColorLight()),
		new ThemeInfo("PaperColor Dark", "PaperColor", "Dark", true, "Material Design inspired dark theme", () => new Themes.PaperColor.PaperColorDark()),

		// Single-variant themes
		new ThemeInfo("Nord", "Nord", null, true, "Arctic-inspired theme with cool blue tones", () => new Themes.Nord.Nord()),
		new ThemeInfo("Dracula", "Dracula", null, true, "Gothic theme with purple and pink accents", () => new Themes.Dracula.Dracula()),
		new ThemeInfo("VSCode Dark", "VSCode", "Dark", true, "Microsoft VSCode's default dark theme", () => new Themes.VSCode.VSCodeDark()),
		new ThemeInfo("VSCode Light", "VSCode", "Light", false, "Microsoft VSCode's default light theme", () => new Themes.VSCode.VSCodeLight()),
		new ThemeInfo("One Dark", "One Dark", null, true, "Atom's iconic One Dark theme", () => new Themes.OneDark.OneDark()),
		new ThemeInfo("Monokai", "Monokai", null, true, "Classic Monokai theme with vibrant colors", () => new Themes.Monokai.Monokai()),
		new ThemeInfo("Nightfly", "Nightfly", null, true, "Dark blue theme inspired by night flying", () => new Themes.Nightfly.Nightfly()),
	];

	/// <summary>
	/// Gets themes grouped by family.
	/// </summary>
	public static IReadOnlyDictionary<string, IReadOnlyList<ThemeInfo>> ThemesByFamily { get; } =
		AllThemes.GroupBy(t => t.Family)
			.ToDictionary<IGrouping<string, ThemeInfo>, string, IReadOnlyList<ThemeInfo>>(
				g => g.Key,
				g => [.. g]);

	/// <summary>
	/// Gets all dark themes.
	/// </summary>
	public static IReadOnlyList<ThemeInfo> DarkThemes { get; } =
		[.. AllThemes.Where(t => t.IsDark)];

	/// <summary>
	/// Gets all light themes.
	/// </summary>
	public static IReadOnlyList<ThemeInfo> LightThemes { get; } =
		[.. AllThemes.Where(t => !t.IsDark)];

	/// <summary>
	/// Gets all theme families.
	/// </summary>
	public static IReadOnlyList<string> Families { get; } =
		[.. AllThemes.Select(t => t.Family).Distinct().OrderBy(f => f)];

	/// <summary>
	/// Finds a theme by name (case-insensitive).
	/// </summary>
	/// <param name="name">The theme name to search for</param>
	/// <returns>The theme info if found, null otherwise</returns>
	public static ThemeInfo? FindTheme(string name)
	{
		return AllThemes.FirstOrDefault(t =>
			string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
	}

	/// <summary>
	/// Gets all themes in a specific family.
	/// </summary>
	/// <param name="family">The family name</param>
	/// <returns>Array of themes in the family</returns>
	public static IReadOnlyList<ThemeInfo> GetThemesInFamily(string family)
	{
		if (ThemesByFamily.TryGetValue(family, out IReadOnlyList<ThemeInfo>? themes) && themes is not null)
		{
			return [.. themes];
		}
		return [];
	}

	/// <summary>
	/// Creates instances of all themes.
	/// </summary>
	/// <returns>Array of all theme instances</returns>
	public static IReadOnlyList<ISemanticTheme> CreateAllThemeInstances() =>
		[.. AllThemes.Select(t => t.CreateInstance())];

	/// <summary>
	/// Creates theme instances for a specific family.
	/// </summary>
	/// <param name="family">The family name</param>
	/// <returns>Array of theme instances in the family</returns>
	public static IReadOnlyList<ISemanticTheme> CreateThemeInstancesInFamily(string family) =>
		[.. GetThemesInFamily(family).Select(t => t.CreateInstance())];
}
