# ThemeProvider.ImGui

**Semantic theme mapping for Dear ImGui applications**

This library provides seamless integration between ThemeProvider's semantic color system and Dear ImGui, automatically generating complete ImGui color palettes from semantic themes.

## Features

- **🎨 Complete ImGui Coverage**: Maps all 50+ ImGui colors systematically
- **🧠 Semantic Intelligence**: Uses semantic specifications (meaning, role, importance) rather than arbitrary color names
- **⚡ One-Line Integration**: Replace manual color mapping with a single method call
- **🎯 Smart Defaults**: Automatic fallbacks for unmapped colors
- **🔧 Interactive States**: Brightness adjustments for hover/active states
- **♿ Accessibility Aware**: Maintains proper contrast ratios

## Installation

Add a reference to the `ThemeProvider.ImGui` package:

```xml
<PackageReference Include="ktsu.ThemeProvider.ImGui" />
```

## Quick Start

```csharp
using ktsu.ThemeProvider.ImGui;
using ktsu.ThemeProvider.Themes.Catppuccin;

// Create your semantic theme
var theme = CatppuccinMocha.CreateTheme();

// Create the mapper
var imguiMapper = new ImGuiPaletteMapper();

// Get complete ImGui color palette
var imguiColors = imguiMapper.MapTheme(theme);

// Apply to ImGui (in your render loop)
var style = ImGui.GetStyle();
foreach ((ImGuiCol colorKey, Vector4 colorValue) in imguiColors)
{
    style.Colors[(int)colorKey] = colorValue;
}
```

## Semantic Mappings

The mapper intelligently maps semantic specifications to ImGui colors:

| ImGui Element | Semantic Mapping |
|---------------|------------------|
| `ImGuiCol.Text` | Normal/Text/Critical |
| `ImGuiCol.Button` | CallToAction/Widget/Critical |
| `ImGuiCol.WindowBg` | Normal/Background/Low |
| `ImGuiCol.FrameBg` | Normal/Surface/Low |
| `ImGuiCol.PlotHistogram` | Success/Widget/High |
| `ImGuiCol.TextSelectedBg` | CallToAction/Surface/Medium |

## Advanced Usage

### Custom Framework Integration

```csharp
// Implement IPaletteMapper for other UI frameworks
public class MyFrameworkMapper : IPaletteMapper<MyFrameworkColor, MyColorType>
{
    public string FrameworkName => "My UI Framework";
    
    public ImmutableDictionary<MyFrameworkColor, MyColorType> MapTheme(ThemeDefinition theme)
    {
        // Your mapping logic here
    }
}
```

### Metadata and Diagnostics

```csharp
var metadata = imguiMapper.GetMappingMetadata(theme);
Console.WriteLine($"Mapped {metadata["mapped_colors_count"]} colors");
Console.WriteLine($"Theme: {metadata["theme_name"]} ({metadata["theme_type"]})");
```

## Benefits Over Manual Mapping

**Before** (Manual approach):
```csharp
// Tedious manual mapping of 50+ colors
colors[ImGuiCol.Text] = theme.GetColor(new SemanticColorSpec(...));
colors[ImGuiCol.Button] = theme.GetColor(new SemanticColorSpec(...));
colors[ImGuiCol.WindowBg] = theme.GetColor(new SemanticColorSpec(...));
// ... 50+ more lines
// Missing colors = broken UI
// Inconsistent semantic usage
```

**After** (Systematic approach):
```csharp
// One line for complete, consistent theming
var imguiColors = imguiMapper.MapTheme(theme);
// All colors mapped systematically
// Automatic fallbacks
// Semantic consistency guaranteed
```

## Examples

See the [ThemeProvider.Demo](../ThemeProvider.Demo/) project for a complete working example showcasing:

- Theme overview and color exploration
- Semantic color specification builder
- Live palette generation
- ImGui mapping demonstration
- Accessibility testing

## Contributing

This library is part of the broader ThemeProvider ecosystem. Contributions are welcome! Please follow the semantic color principles:

1. **Semantic First**: Colors should be defined by purpose, not appearance
2. **Systematic**: Mappings should follow consistent patterns
3. **Accessible**: Always consider contrast and readability
4. **Extensible**: Design for easy adaptation to other frameworks

## License

Licensed under the MIT License. See [LICENSE](../LICENSE.md) for details. 
