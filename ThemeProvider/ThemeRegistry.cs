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

	// Family names, each shared by every variant it groups.
	private const string CatppuccinFamily = "Catppuccin";
	private const string TokyoNightFamily = "Tokyo Night";
	private const string GruvboxFamily = "Gruvbox";
	private const string EverforestFamily = "Everforest";
	private const string NightfoxFamily = "Nightfox";
	private const string KanagawaFamily = "Kanagawa";
	private const string PaperColorFamily = "PaperColor";
	private const string VSCodeFamily = "VSCode";

	// Variant labels shared across several families.
	private const string DarkVariant = "Dark";
	private const string LightVariant = "Light";

	/// <summary>
	/// Gets all registered themes with their metadata.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1506:AvoidExcessiveClassCoupling",
		Justification = "Theme registry is designed to reference all available themes")]
	public static IReadOnlyList<ThemeInfo> AllThemes { get; } = [
		// Catppuccin Family (4 variants)
		new ThemeInfo("Catppuccin Latte", CatppuccinFamily, "Latte", false, "Warm light theme with pastel colors", () => new Themes.Catppuccin.Latte()),
		new ThemeInfo("Catppuccin Frappe", CatppuccinFamily, "Frappe", true, "Soft dark theme with muted pastels", () => new Themes.Catppuccin.Frappe()),
		new ThemeInfo("Catppuccin Macchiato", CatppuccinFamily, "Macchiato", true, "Medium-dark theme with vibrant accents", () => new Themes.Catppuccin.Macchiato()),
		new ThemeInfo("Catppuccin Mocha", CatppuccinFamily, "Mocha", true, "Rich dark theme with warm undertones", () => new Themes.Catppuccin.Mocha()),

		// Tokyo Night Family (3 variants)
		new ThemeInfo(TokyoNightFamily, TokyoNightFamily, null, true, "Clean dark theme inspired by Tokyo's neon nights", () => new Themes.TokyoNight.TokyoNight()),
		new ThemeInfo("Tokyo Night Storm", TokyoNightFamily, "Storm", true, "Softer contrast variant of Tokyo Night", () => new Themes.TokyoNight.TokyoNightStorm()),
		new ThemeInfo("Tokyo Night Day", TokyoNightFamily, "Day", false, "Light variant with Tokyo Night's aesthetic", () => new Themes.TokyoNight.TokyoNightDay()),

		// Gruvbox Family (6 variants)
		new ThemeInfo("Gruvbox Dark", GruvboxFamily, DarkVariant, true, "Retro groove colors with warm dark background", () => new Themes.Gruvbox.GruvboxDark()),
		new ThemeInfo("Gruvbox Dark Hard", GruvboxFamily, "Dark Hard", true, "High contrast dark variant of Gruvbox", () => new Themes.Gruvbox.GruvboxDarkHard()),
		new ThemeInfo("Gruvbox Dark Soft", GruvboxFamily, "Dark Soft", true, "Low contrast dark variant of Gruvbox", () => new Themes.Gruvbox.GruvboxDarkSoft()),
		new ThemeInfo("Gruvbox Light", GruvboxFamily, LightVariant, false, "Retro groove colors with warm light background", () => new Themes.Gruvbox.GruvboxLight()),
		new ThemeInfo("Gruvbox Light Hard", GruvboxFamily, "Light Hard", false, "High contrast light variant of Gruvbox", () => new Themes.Gruvbox.GruvboxLightHard()),
		new ThemeInfo("Gruvbox Light Soft", GruvboxFamily, "Light Soft", false, "Low contrast light variant of Gruvbox", () => new Themes.Gruvbox.GruvboxLightSoft()),

		// Everforest Family (6 variants)
		new ThemeInfo("Everforest Dark", EverforestFamily, DarkVariant, true, "Green forest colors with comfortable dark background", () => new Themes.Everforest.EverforestDark()),
		new ThemeInfo("Everforest Dark Hard", EverforestFamily, "Dark Hard", true, "High contrast dark forest theme", () => new Themes.Everforest.EverforestDarkHard()),
		new ThemeInfo("Everforest Dark Soft", EverforestFamily, "Dark Soft", true, "Soft contrast dark forest theme", () => new Themes.Everforest.EverforestDarkSoft()),
		new ThemeInfo("Everforest Light", EverforestFamily, LightVariant, false, "Green forest colors with bright background", () => new Themes.Everforest.EverforestLight()),
		new ThemeInfo("Everforest Light Hard", EverforestFamily, "Light Hard", false, "High contrast light forest theme", () => new Themes.Everforest.EverforestLightHard()),
		new ThemeInfo("Everforest Light Soft", EverforestFamily, "Light Soft", false, "Soft contrast light forest theme", () => new Themes.Everforest.EverforestLightSoft()),

		// Nightfox Family (7 variants)
		new ThemeInfo(NightfoxFamily, NightfoxFamily, null, true, "Vibrant dark theme with fox-inspired colors", () => new Themes.Nightfox.Nightfox()),
		new ThemeInfo("Dayfox", NightfoxFamily, "Dayfox", false, "Warm light variant of Nightfox", () => new Themes.Nightfox.Dayfox()),
		new ThemeInfo("Duskfox", NightfoxFamily, "Duskfox", true, "Muted dark variant with purple undertones", () => new Themes.Nightfox.Duskfox()),
		new ThemeInfo("Nordfox", NightfoxFamily, "Nordfox", true, "Nord-inspired arctic theme", () => new Themes.Nightfox.Nordfox()),
		new ThemeInfo("Terafox", NightfoxFamily, "Terafox", true, "Earthy terra-inspired variant", () => new Themes.Nightfox.Terafox()),
		new ThemeInfo("Carbonfox", NightfoxFamily, "Carbonfox", true, "Minimalist carbon-inspired theme", () => new Themes.Nightfox.Carbonfox()),
		new ThemeInfo("Dawnfox", NightfoxFamily, "Dawnfox", false, "Soft morning light variant", () => new Themes.Nightfox.Dawnfox()),

		// Kanagawa Family (3 variants)
		new ThemeInfo("Kanagawa Wave", KanagawaFamily, "Wave", true, "Japanese-inspired theme based on 'The Great Wave'", () => new Themes.Kanagawa.KanagawaWave()),
		new ThemeInfo("Kanagawa Dragon", KanagawaFamily, "Dragon", true, "Darker variant inspired by Japanese dragons", () => new Themes.Kanagawa.KanagawaDragon()),
		new ThemeInfo("Kanagawa Lotus", KanagawaFamily, "Lotus", false, "Light zen garden inspired theme", () => new Themes.Kanagawa.KanagawaLotus()),

		// PaperColor Family (2 variants)
		new ThemeInfo("PaperColor Light", PaperColorFamily, LightVariant, false, "Material Design inspired light theme", () => new Themes.PaperColor.PaperColorLight()),
		new ThemeInfo("PaperColor Dark", PaperColorFamily, DarkVariant, true, "Material Design inspired dark theme", () => new Themes.PaperColor.PaperColorDark()),

		// Single-variant themes
		new ThemeInfo("Nord", "Nord", null, true, "Arctic-inspired theme with cool blue tones", () => new Themes.Nord.Nord()),
		new ThemeInfo("Dracula", "Dracula", null, true, "Gothic theme with purple and pink accents", () => new Themes.Dracula.Dracula()),
		new ThemeInfo("VSCode Dark", VSCodeFamily, DarkVariant, true, "Microsoft VSCode's default dark theme", () => new Themes.VSCode.VSCodeDark()),
		new ThemeInfo("VSCode Light", VSCodeFamily, LightVariant, false, "Microsoft VSCode's default light theme", () => new Themes.VSCode.VSCodeLight()),
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
