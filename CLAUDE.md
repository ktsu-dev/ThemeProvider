# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Restore, build, and test (standard workflow)
dotnet restore
dotnet build

# Build specific project
dotnet build ThemeProvider/ThemeProvider.csproj

# Build specific configuration
dotnet build -c Release

# Run the demo application (requires .NET 10)
dotnet run --project ThemeProviderDemo
```

## Project Structure

This is a .NET library (`ktsu.ThemeProvider`) providing a semantic color theming system with 44 built-in themes and framework integration. The solution uses:

- **ktsu.Sdk** - Custom SDK providing shared build configuration
- Multi-targeting: net10.0;net9.0;net8.0;net7.0;net6.0;net5.0;netstandard2.0;netstandard2.1

### Key Files

- `ThemeProvider/ISemanticTheme.cs` - Core theme interface (SemanticMapping + IsDarkTheme)
- `ThemeProvider/SemanticColorMapper.cs` - Maps semantic color requests to actual colors using Oklab color space
- `ThemeProvider/ThemeRegistry.cs` - Central registration of all 44 themes with metadata and factory functions
- Color types (`Color`, `Srgb`, `Oklab`/`Oklch`, `AccessibilityLevel`, color-space conversions, WCAG, gradients) come from the **`ktsu.Semantics.Color`** package — there are no in-house color types (they were removed in v2.0)
- `ThemeProvider/IPaletteMapper.cs` - Generic interface for framework-specific color mapping
- `ThemeProvider/SemanticColorRequest.cs` - Readonly record struct combining SemanticMeaning + Priority
- `ThemeProvider/SemanticMeaning.cs` - Enum of semantic color purposes (Neutral, Primary, Error, etc.)
- `ThemeProvider/Priority.cs` - Enum of 7 priority levels (VeryLow to VeryHigh)
- `ThemeProvider/ColorRange.cs` - Color range interpolation helper (built on `ktsu.Semantics.Color`)
- `ThemeProvider/Themes/` - 44 theme implementations organized by family
- `ThemeProvider.ImGui/ImGuiPaletteMapper.cs` - Dear ImGui integration mapping ImGuiCol to Vector4
- `ThemeProviderDemo/Program.cs` - Interactive demo application using ktsu.ImGuiApp

### Dependencies

- **ktsu.Semantics.Color** - Color value types and color science (`Color`, Oklab/Oklch/HSL/HSV, WCAG, gradients)
- **Polyfill** - Backfill support for newer .NET APIs on older targets
- **System.Numerics.Vectors** - Vector types for netstandard2.0 target
- **Hexa.NET.ImGui** - Dear ImGui bindings (ThemeProvider.ImGui project)
- **ktsu.ImGui.App** - ImGui application framework (ThemeProviderDemo only)

## Architecture

### Semantic Color System

The library uses meaning-based color specifications instead of hardcoded colors:

1. **ISemanticTheme** provides a `Dictionary<SemanticMeaning, Collection<Color>>` mapping (`Color` from `ktsu.Semantics.Color`)
2. **SemanticColorMapper** maps `SemanticColorRequest` (meaning + priority) to actual colors
3. The mapper calculates a global lightness range across all theme colors, then interpolates/extrapolates in Oklab space to achieve target lightness for each priority level
4. Dark themes: higher priority = higher lightness; Light themes: higher priority = lower lightness
5. Non-neutral meanings use 50-90% of the global lightness range; neutral uses the full range

### Adding New Themes

1. Create a new class in `ThemeProvider/Themes/{Family}/{ThemeName}.cs`
2. Implement `ISemanticTheme` with static `Color` fields from hex values using `Color.FromHex("#hex")` (`Color` from `ktsu.Semantics.Color`)
3. Map semantic meanings to color collections (Neutral typically gets multiple colors; other meanings get single accent colors)
4. Register in `ThemeRegistry.AllThemes` with a `ThemeInfo` record

### Framework Integration

Framework mappers implement `IPaletteMapper<TColorKey, TColorValue>` to convert semantic themes to framework-specific color dictionaries. `ImGuiPaletteMapper` is the built-in implementation for Dear ImGui.

## CI/CD

Uses `scripts/PSBuild.psm1` PowerShell module for CI pipeline. Version increments are controlled by commit message tags: `[major]`, `[minor]`, `[patch]`, `[pre]`.

## Code Quality

Do not add global suppressions for warnings. Use explicit suppression attributes with justifications when needed, with preprocessor defines only as fallback. Make the smallest, most targeted suppressions possible.
