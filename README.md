# ktsu.ThemeProvider

> A semantic color theming library for .NET applications with 44+ themes, intelligent color mapping, and framework integration.

[![License](https://img.shields.io/github/license/ktsu-dev/ThemeProvider.svg?label=License&logo=nuget)](LICENSE.md)
[![NuGet Version](https://img.shields.io/nuget/v/ktsu.ThemeProvider?label=Stable&logo=nuget)](https://nuget.org/packages/ktsu.ThemeProvider)
[![NuGet Version](https://img.shields.io/nuget/vpre/ktsu.ThemeProvider?label=Latest&logo=nuget)](https://nuget.org/packages/ktsu.ThemeProvider)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.ThemeProvider?label=Downloads&logo=nuget)](https://nuget.org/packages/ktsu.ThemeProvider)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/m/ktsu-dev/ThemeProvider?label=Commits&logo=github)](https://github.com/ktsu-dev/ThemeProvider/commits/main)
[![GitHub contributors](https://img.shields.io/github/contributors/ktsu-dev/ThemeProvider?label=Contributors&logo=github)](https://github.com/ktsu-dev/ThemeProvider/graphs/contributors)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ktsu-dev/ThemeProvider/dotnet.yml?label=Build&logo=github)](https://github.com/ktsu-dev/ThemeProvider/actions)

## Introduction

`ktsu.ThemeProvider` is a comprehensive theming system that uses semantic color specifications rather than arbitrary color names. Instead of hardcoding colors like "blue" or "red", you define colors by their purpose (Primary, Error, Warning) and priority level, and the library generates consistent, accessible color palettes. It includes 44 carefully crafted themes from popular color schemes and provides built-in Dear ImGui integration with an extensible architecture for other UI frameworks.

## Features

- **Semantic Color System**: Define colors by purpose (Primary, Error, Warning, Neutral) and priority level rather than specific hues, enabling consistent theming across any UI framework
- **44 Built-in Themes**: Includes Catppuccin, Tokyo Night, Gruvbox, Everforest, Nightfox, Kanagawa, PaperColor, Nord, Dracula, VSCode, One Dark, Monokai, and Nightfly theme families
- **Centralized Theme Registry**: Discover, filter, and instantiate themes by name, family, or light/dark classification with rich metadata
- **Dear ImGui Integration**: Companion package `ktsu.ThemeProvider.ImGui` provides complete ImGui color palette mapping via `ImGuiPaletteMapper`
- **Perceptual Color Science**: Uses Oklab perceptual color space for uniform color interpolation, extrapolation, and lightness-based priority mapping
- **WCAG Accessibility**: Built-in contrast ratio calculations, accessibility level checking (AA/AAA), and automatic color adjustment to meet WCAG standards
- **Extensible Framework Mappers**: Implement `IPaletteMapper<TColorKey, TColorValue>` to integrate with any UI framework
- **Priority-Based Color Hierarchy**: Seven priority levels (VeryLow to VeryHigh) automatically mapped to appropriate lightness values, with theme-aware ordering for dark and light themes
- **Multi-Target Support**: Targets .NET 5.0 through 10.0, plus .NET Standard 2.0 and 2.1

## Installation

### Package Manager Console

```powershell
Install-Package ktsu.ThemeProvider
```

### .NET CLI

```bash
dotnet add package ktsu.ThemeProvider
```

### Package Reference

```xml
<PackageReference Include="ktsu.ThemeProvider" Version="x.y.z" />
```

For Dear ImGui integration, also install:

```bash
dotnet add package ktsu.ThemeProvider.ImGui
```

## Usage Examples

### Basic Example

```csharp
using ktsu.ThemeProvider;
using static ktsu.ThemeProvider.ThemeRegistry;

// Create a theme directly
var theme = new Themes.Catppuccin.Mocha();

// Or find and create via the registry
ThemeInfo? themeInfo = FindTheme("Catppuccin Mocha");
ISemanticTheme? registryTheme = themeInfo?.CreateInstance();

// Map semantic color requests to actual colors
var requests = new[]
{
    new SemanticColorRequest(SemanticMeaning.Primary, Priority.Medium),
    new SemanticColorRequest(SemanticMeaning.Error, Priority.High),
    new SemanticColorRequest(SemanticMeaning.Neutral, Priority.VeryLow),
};
IReadOnlyDictionary<SemanticColorRequest, Color> colors =
    SemanticColorMapper.MapColors(requests, theme);
```

### Theme Discovery with the Registry

```csharp
using ktsu.ThemeProvider;
using static ktsu.ThemeProvider.ThemeRegistry;

// Browse all themes
IReadOnlyList<ThemeInfo> allThemes = AllThemes;
IReadOnlyList<ThemeInfo> darkThemes = DarkThemes;
IReadOnlyList<ThemeInfo> lightThemes = LightThemes;

// Browse by family
IReadOnlyList<string> families = Families;
IReadOnlyList<ThemeInfo> catppuccinThemes = GetThemesInFamily("Catppuccin");

// Find a specific theme by name (case-insensitive)
ThemeInfo? themeInfo = FindTheme("Tokyo Night Storm");
ISemanticTheme? theme = themeInfo?.CreateInstance();

// Create all theme instances at once
IReadOnlyList<ISemanticTheme> allInstances = CreateAllThemeInstances();
IReadOnlyList<ISemanticTheme> gruvboxInstances = CreateThemeInstancesInFamily("Gruvbox");
```

### Complete Palette Generation

```csharp
using ktsu.ThemeProvider;
using ktsu.Semantics.Color;

var theme = new Themes.Nord.Nord();

// Generate the complete palette (all meaning + priority combinations)
IReadOnlyDictionary<SemanticColorRequest, Color> completePalette =
    SemanticColorMapper.MakeCompletePalette(theme);

// Access any color from the palette
var primaryMedium = completePalette[new SemanticColorRequest(SemanticMeaning.Primary, Priority.Medium)];
string hex = primaryMedium.ToHex();
```

### Dear ImGui Integration

```csharp
using ktsu.ThemeProvider;
using ktsu.ThemeProvider.ImGui;
using Hexa.NET.ImGui;

// Create theme and mapper
var theme = new Themes.Catppuccin.Mocha();
var mapper = new ImGuiPaletteMapper();

// Get complete ImGui color palette
IReadOnlyDictionary<ImGuiCol, Vector4> imguiColors = mapper.MapTheme(theme);

// Apply to ImGui style
var style = ImGui.GetStyle();
foreach ((ImGuiCol colorKey, Vector4 colorValue) in imguiColors)
{
    style.Colors[(int)colorKey] = colorValue;
}
```

### Accessibility Checking

```csharp
using ktsu.ThemeProvider;
using ktsu.Semantics.Color;

Color foreground = Color.FromHex("#FFFFFF");
Color background = Color.FromHex("#1E1E2E");

// Calculate contrast ratio (1.0 .. 21.0)
double contrastRatio = foreground.ContrastRatio(background);

// Check WCAG compliance
AccessibilityLevel level = foreground.AccessibilityLevelAgainst(background, largeText: false);

// Adjust a color to meet accessibility requirements
Color adjusted = foreground.AdjustForContrast(background, AccessibilityLevel.AA);

// Create perceptually uniform gradients
IReadOnlyList<Color> gradient = foreground.Gradient(background, steps: 10);
```

## Advanced Usage

### Creating Custom Framework Mappers

Implement `IPaletteMapper<TColorKey, TColorValue>` to integrate with any UI framework:

```csharp
using ktsu.ThemeProvider;

public class MyFrameworkMapper : IPaletteMapper<MyColorEnum, MyColorType>
{
    public string FrameworkName => "My UI Framework";

    public IReadOnlyDictionary<MyColorEnum, MyColorType> MapTheme(ISemanticTheme theme)
    {
        var requests = new Dictionary<MyColorEnum, SemanticColorRequest>
        {
            { MyColorEnum.Button, new(SemanticMeaning.Primary, Priority.Medium) },
            { MyColorEnum.Background, new(SemanticMeaning.Neutral, Priority.VeryLow) },
            { MyColorEnum.ErrorText, new(SemanticMeaning.Error, Priority.High) },
        };

        var palette = SemanticColorMapper.MapColors(requests.Values, theme);

        var result = new Dictionary<MyColorEnum, MyColorType>();
        foreach (var kvp in requests)
        {
            if (palette.TryGetValue(kvp.Value, out var color))
            {
                result[kvp.Key] = ConvertToMyColor(color.RgbValue);
            }
        }
        return result;
    }
}
```

### Creating Custom Themes

Implement `ISemanticTheme` with colors from your palette:

```csharp
using ktsu.ThemeProvider;
using ktsu.Semantics.Color;
using System.Collections.ObjectModel;

public class MyCustomTheme : ISemanticTheme
{
    private static readonly Color Background = Color.FromHex("#1A1B26");
    private static readonly Color Foreground = Color.FromHex("#C0CAF5");
    private static readonly Color Blue = Color.FromHex("#7AA2F7");
    private static readonly Color Green = Color.FromHex("#9ECE6A");
    private static readonly Color Red = Color.FromHex("#F7768E");

    public bool IsDarkTheme => true;

    public Dictionary<SemanticMeaning, Collection<Color>> SemanticMapping { get; } = new()
    {
        { SemanticMeaning.Neutral, new() { Background, Foreground } },
        { SemanticMeaning.Primary, new() { Blue } },
        { SemanticMeaning.Success, new() { Green } },
        { SemanticMeaning.Error, new() { Red } },
    };
}
```

## API Reference

### `SemanticMeaning` (enum)

Defines semantic color purposes.

| Value | Description |
|-------|-------------|
| `Neutral` | Backgrounds, borders, inactive elements |
| `Primary` | Main brand/accent colors |
| `Alternate` | Secondary accent, binary choice emphasis |
| `Success` | Successful operations, confirmations |
| `CallToAction` | Important buttons and highlights demanding attention |
| `Information` | Informational content, help text |
| `Caution` | Cautionary content needing attention |
| `Warning` | Warning states, potentially problematic |
| `Error` | Error states, incorrect conditions |
| `Failure` | Failed operations (distinct from error) |
| `Debug` | Debug/development information |

### `Priority` (enum)

Controls color intensity and lightness within a semantic meaning.

| Value | Description |
|-------|-------------|
| `VeryLow` | Lowest intensity (backgrounds in dark themes, lightest in light themes) |
| `Low` | Low intensity |
| `MediumLow` | Below-medium intensity |
| `Medium` | Default intensity level |
| `MediumHigh` | Above-medium intensity |
| `High` | High intensity |
| `VeryHigh` | Highest intensity (foreground text in dark themes, darkest in light themes) |

### `SemanticColorRequest`

A readonly record struct combining a `SemanticMeaning` and `Priority` to specify a color.

| Property | Type | Description |
|----------|------|-------------|
| `Meaning` | `SemanticMeaning` | The semantic purpose of the color |
| `Priority` | `Priority` | The intensity/lightness level |

### `SemanticColorMapper`

Static class that maps semantic color requests to actual colors.

#### Methods

| Name | Return Type | Description |
|------|-------------|-------------|
| `MapColors(requests, theme)` | `IReadOnlyDictionary<SemanticColorRequest, Color>` | Maps a collection of requests to colors using the theme |
| `MakeCompletePalette(theme)` | `IReadOnlyDictionary<SemanticColorRequest, Color>` | Generates all possible meaning+priority combinations for a theme |

### `ThemeRegistry`

Static class providing centralized theme discovery and management.

#### Properties

| Name | Type | Description |
|------|------|-------------|
| `AllThemes` | `IReadOnlyList<ThemeInfo>` | All 44 registered themes with metadata |
| `DarkThemes` | `IReadOnlyList<ThemeInfo>` | All dark themes |
| `LightThemes` | `IReadOnlyList<ThemeInfo>` | All light themes |
| `Families` | `IReadOnlyList<string>` | All theme family names |
| `ThemesByFamily` | `IReadOnlyDictionary<string, IReadOnlyList<ThemeInfo>>` | Themes grouped by family |

#### Methods

| Name | Return Type | Description |
|------|-------------|-------------|
| `FindTheme(name)` | `ThemeInfo?` | Finds a theme by name (case-insensitive) |
| `GetThemesInFamily(family)` | `IReadOnlyList<ThemeInfo>` | Gets all themes in a family |
| `CreateAllThemeInstances()` | `IReadOnlyList<ISemanticTheme>` | Creates instances of all themes |
| `CreateThemeInstancesInFamily(family)` | `IReadOnlyList<ISemanticTheme>` | Creates instances of themes in a family |

### Color types (`ktsu.Semantics.Color`)

Colors are represented by the `Color` type from the [`ktsu.Semantics.Color`](https://www.nuget.org/packages/ktsu.Semantics.Color) package (linear RGB + alpha, gamma-correct). It is the currency type for themes (`ISemanticTheme.SemanticMapping`) and `SemanticColorMapper`.

Common members:

| Member | Description |
|--------|-------------|
| `Color.FromHex(hex)` | Creates a color from an sRGB hex string (proper sRGB→linear decode) |
| `ToHex()` / `ToBytes()` | Converts back to an sRGB hex string / 8-bit channels |
| `ToSrgbVector4()` | sRGB-encoded `Vector4` for UI frameworks (e.g. ImGui) |
| `ToOklab()` / `ToOklch()` | Perceptual (Oklab / polar LCh) representations |
| `ContrastRatio(other)` | WCAG contrast ratio (1:1 .. 21:1) |
| `AccessibilityLevelAgainst(bg, largeText)` | WCAG AA/AAA compliance (returns `AccessibilityLevel`) |
| `AdjustForContrast(bg, level, largeText)` | Adjusts lightness to meet a WCAG level |
| `DistanceTo(other)` | Perceptual (Oklab) distance |
| `MixOklab(other, t)` / `Gradient(to, steps)` | Perceptual blend / uniform gradient |

> **Migration note (v2.0):** ThemeProvider's in-house `RgbColor`, `SRgbColor`, `OklabColor`, `PerceptualColor`, and `ColorMath` types were removed in favour of `ktsu.Semantics.Color`. This also fixed a long-standing sRGB-as-linear gamma bug — base theme colors render identically, but the semantic mapper's derived colors and accessibility numbers are now computed correctly.

### `IPaletteMapper<TColorKey, TColorValue>`

Interface for mapping semantic themes to framework-specific color palettes.

#### Properties

| Name | Type | Description |
|------|------|-------------|
| `FrameworkName` | `string` | The name of the target UI framework |

#### Methods

| Name | Return Type | Description |
|------|-------------|-------------|
| `MapTheme(theme)` | `IReadOnlyDictionary<TColorKey, TColorValue>` | Maps a theme to a framework-specific palette |

## Available Themes

| Family | Variants | Description |
|--------|----------|-------------|
| **Catppuccin** | Latte, Frappe, Macchiato, Mocha | Warm pastel themes with excellent readability |
| **Tokyo Night** | Night, Storm, Day | Clean themes inspired by Tokyo's neon nights |
| **Gruvbox** | Dark, Dark Hard, Dark Soft, Light, Light Hard, Light Soft | Retro groove colors with warm backgrounds |
| **Everforest** | Dark, Dark Hard, Dark Soft, Light, Light Hard, Light Soft | Green forest colors for comfortable viewing |
| **Nightfox** | Nightfox, Dayfox, Duskfox, Nordfox, Terafox, Carbonfox, Dawnfox | Fox-inspired vibrant themes |
| **Kanagawa** | Wave, Dragon, Lotus | Japanese-inspired themes |
| **PaperColor** | Light, Dark | Material Design inspired themes |
| **VSCode** | Dark, Light | Microsoft VSCode default themes |
| **Nord** | - | Arctic-inspired theme with cool blue tones |
| **Dracula** | - | Gothic theme with purple and pink accents |
| **One Dark** | - | Atom's iconic One Dark theme |
| **Monokai** | - | Classic Monokai with vibrant colors |
| **Nightfly** | - | Dark blue theme inspired by night flying |

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License. See the [LICENSE.md](LICENSE.md) file for details.
