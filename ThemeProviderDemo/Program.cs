// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProviderDemo;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using Hexa.NET.ImGui;
using ktsu.ImGui.App;
using ktsu.Semantics.Color;
using ktsu.ThemeProvider;
using ktsu.ThemeProvider.ImGui;
using static ktsu.ThemeProvider.ThemeRegistry;

internal static class Program
{
	private static ISemanticTheme theme = null!;
	private static ImGuiPaletteMapper imguiMapper = null!;

	// Theme selection using the centralized registry
	private static readonly ThemeInfo[] availableThemes = [.. AllThemes];
	private static readonly string[] themeNames = [.. AllThemes.Select(t =>
		$"{t.Name} ({(t.IsDark ? "Dark" : "Light")})")];

	private static int selectedThemeIndex;

	// UI State
	private static int selectedSemanticMeaning = (int)SemanticMeaning.Primary;
	private static int selectedPriority = (int)Priority.Medium;
	private static Vector3 selectedColorVec = Vector3.Zero;
	private static Vector3 backgroundColorVec = new(0.1f, 0.1f, 0.1f);
	private static bool isLargeText;

	// Form state
	private static float sliderValue = 0.5f;
	private static bool checkboxValue;
	private static string textValue = "Text Input";

	private static void Main()
	{
		ImGuiApp.Start(new()
		{
			Title = "ThemeProvider Demo - Semantic Color System",
			OnRender = OnRender,
			OnStart = OnStart,
			SaveIniSettings = false,
		});
	}

	private static void OnStart()
	{
		selectedThemeIndex = 3; // Start with Catppuccin Mocha
		theme = availableThemes[selectedThemeIndex].CreateInstance();
		imguiMapper = new ImGuiPaletteMapper();

		// Initialize with theme's primary color
		SemanticColorRequest primaryRequest = new(SemanticMeaning.Primary, Priority.MediumHigh);
		if (GetColorFromTheme(primaryRequest) is { } primaryColor)
		{
			Srgb pcSrgb = primaryColor.ToSrgb();
			selectedColorVec = new Vector3((float)pcSrgb.R, (float)pcSrgb.G, (float)pcSrgb.B);
		}
	}

	private static void OnRender(float deltaTime)
	{
		ApplyTheme();

		if (ImGui.BeginTabBar("DemoTabs"))
		{
			if (ImGui.BeginTabItem("Theme Browser"))
			{
				RenderThemeBrowser();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Theme Overview"))
			{
				RenderThemeOverview();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Semantic Colors"))
			{
				RenderSemanticColors();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("ImGui Mapping"))
			{
				RenderImGuiMapping();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Accessibility"))
			{
				RenderAccessibilityDemo();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("UI Preview"))
			{
				RenderUIPreview();
				ImGui.EndTabItem();
			}

			ImGui.EndTabBar();
		}
	}

	private static void ApplyTheme()
	{
		// Use the ImGui palette mapper system
		IReadOnlyDictionary<ImGuiCol, Vector4> imguiColors = imguiMapper.MapTheme(theme);

		ImGuiStylePtr style = ImGui.GetStyle();
		Span<Vector4> colors = style.Colors;

		// Apply all mapped colors with bounds checking
		foreach ((ImGuiCol colorKey, Vector4 colorValue) in imguiColors)
		{
			int colorIndex = (int)colorKey;
			if (colorIndex >= 0 && colorIndex < colors.Length)
			{
				colors[colorIndex] = colorValue;
			}
		}
	}

	private static void RenderThemeBrowser()
	{
		ImGui.TextUnformatted("Browse all available themes from the ThemeRegistry:");
		ImGui.Separator();

		// Show theme statistics
		ImGui.TextUnformatted($"Total Themes: {AllThemes.Count}");
		ImGui.TextUnformatted($"Theme Families: {Families.Count}");
		ImGui.TextUnformatted($"Dark Themes: {DarkThemes.Count}");
		ImGui.TextUnformatted($"Light Themes: {LightThemes.Count}");

		ImGui.Separator();

		// Browse by family
		foreach (string family in Families)
		{
			IReadOnlyList<ThemeInfo> themesInFamily = GetThemesInFamily(family);

			if (ImGui.CollapsingHeader($"{family} ({themesInFamily.Count} variants)", ImGuiTreeNodeFlags.DefaultOpen))
			{
				ImGui.Indent();

				// Show themes in a grid
				if (ImGui.BeginTable($"Family_{family}", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
				{
					ImGui.TableSetupColumn("Theme");
					ImGui.TableSetupColumn("Type");
					ImGui.TableSetupColumn("Description");
					ImGui.TableSetupColumn("Action");
					ImGui.TableHeadersRow();

					for (int i = 0; i < themesInFamily.Count; i++)
					{
						ThemeInfo themeInfo = themesInFamily[i];
						ImGui.TableNextRow();

						ImGui.TableSetColumnIndex(0);
						ImGui.TextUnformatted(themeInfo.Name);

						ImGui.TableSetColumnIndex(1);
						ImGui.TextColored(themeInfo.IsDark ? new Vector4(0.8f, 0.8f, 1.0f, 1.0f) : new Vector4(1.0f, 1.0f, 0.8f, 1.0f),
							themeInfo.IsDark ? "Dark" : "Light");

						ImGui.TableSetColumnIndex(2);
						ImGui.TextWrapped(themeInfo.Description);

						ImGui.TableSetColumnIndex(3);
						if (ImGui.Button($"Preview##{family}_{i}"))
						{
							// Find this theme in the main array and select it
							for (int j = 0; j < availableThemes.Length; j++)
							{
								if (availableThemes[j].Name == themeInfo.Name)
								{
									selectedThemeIndex = j;
									theme = themeInfo.CreateInstance();
									cachedTheme = null;
									cachedCompletePalette = null;
									break;
								}
							}
						}
					}

					ImGui.EndTable();
				}

				ImGui.Unindent();
			}
		}

		ImGui.Separator();
		ImGui.TextUnformatted("Usage Example:");
		ImGui.Text("// Get all themes\nvar allThemes = ThemeRegistry.AllThemes;\n\n// Get themes by family\nvar gruvboxThemes = ThemeRegistry.GetThemesInFamily(\"Gruvbox\");\n\n// Find specific theme\nvar theme = ThemeRegistry.FindTheme(\"Catppuccin Mocha\");\nvar instance = theme?.CreateInstance();");
	}

	private static void RenderThemeOverview()
	{
		// Theme selection
		ImGui.TextUnformatted("Select Theme:");
		if (ImGui.Combo("##ThemeVariant", ref selectedThemeIndex, themeNames, themeNames.Length))
		{
			theme = availableThemes[selectedThemeIndex].CreateInstance();
			// Clear cache when theme changes
			cachedTheme = null;
			cachedCompletePalette = null;
		}

		ImGui.TextUnformatted($"Semantic Color Grid - Current Theme: {themeNames[selectedThemeIndex]}");
		ImGui.Separator();

		// Show complete palette info
		IReadOnlyDictionary<SemanticColorRequest, Color> completePalette = SemanticColorMapper.MakeCompletePalette(theme);
		ImGui.TextUnformatted($"Complete Palette: {completePalette.Count} colors generated");
		ImGui.TextUnformatted($"Available Semantic Meanings: {theme.SemanticMapping.Count}");

		ImGui.Separator();

		// Get all semantic meanings and priorities
		SemanticMeaning[] semanticMeanings = Enum.GetValues<SemanticMeaning>();
		Priority[] sortedPriorities = [Priority.VeryLow, Priority.Low, Priority.MediumLow, Priority.Medium, Priority.MediumHigh, Priority.High, Priority.VeryHigh];

		// Create a single table showing all semantics and priorities in a grid
		if (ImGui.BeginTable("ColorGrid", sortedPriorities.Length + 1, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
		{
			// Setup columns - first column for semantic names, rest for priorities
			ImGui.TableSetupColumn("Semantic", ImGuiTableColumnFlags.WidthFixed, 100);
			foreach (Priority priority in sortedPriorities)
			{
				ImGui.TableSetupColumn(priority.ToString(), ImGuiTableColumnFlags.WidthFixed, 100);
			}
			ImGui.TableHeadersRow();

			// Create a row for each semantic meaning
			foreach (SemanticMeaning meaning in semanticMeanings)
			{
				ImGui.TableNextRow();

				// First column: semantic meaning name
				ImGui.TableSetColumnIndex(0);
				ImGui.TextUnformatted(meaning.ToString());

				// Rest of columns: color swatches for each priority
				for (int i = 0; i < sortedPriorities.Length; i++)
				{
					ImGui.TableSetColumnIndex(i + 1);
					Priority priority = sortedPriorities[i];
					SemanticColorRequest request = new(meaning, priority);

					if (GetColorFromTheme(request) is { } color)
					{
						Srgb colorSrgb = color.ToSrgb();
						Vector4 colorVec = new((float)colorSrgb.R, (float)colorSrgb.G, (float)colorSrgb.B, 1.0f);
						Vector2 swatchSize = new(90, 30);

						if (ImGui.ColorButton($"##{meaning}_{priority}", colorVec, ImGuiColorEditFlags.NoTooltip, swatchSize))
						{
							// Optional: could copy color to clipboard or do something on click
						}

						if (ImGui.IsItemHovered())
						{
							ImGui.SetTooltip($"{meaning} - {priority}\nLightness: {color.ToOklab().L:F2}\nRGB: {colorSrgb.R:F3}, {colorSrgb.G:F3}, {colorSrgb.B:F3}\nHex: {color.ToHex()}");
						}
					}
				}
			}

			ImGui.EndTable();
		}

		ImGui.Spacing();
		ImGui.TextUnformatted("Note: Neutral uses full global range, non-neutral uses 50-90% of global range");
		ImGui.TextUnformatted($"Theme Type: {(theme.IsDarkTheme ? "Dark" : "Light")} - Priority ordering {(theme.IsDarkTheme ? "low to high" : "high to low")} lightness");
	}

	private static void RenderSemanticColors()
	{
		ImGui.TextUnformatted("Explore the semantic color system:");
		ImGui.Separator();

		// Semantic color request builder
		ImGui.TextUnformatted("Build a Semantic Color Request:");

		string[] meaningNames = Enum.GetNames<SemanticMeaning>();
		ImGui.Combo("Semantic Meaning", ref selectedSemanticMeaning, meaningNames, meaningNames.Length);

		string[] priorityNames = Enum.GetNames<Priority>();
		ImGui.Combo("Priority Level", ref selectedPriority, priorityNames, priorityNames.Length);

		// Build the current request
		SemanticColorRequest currentRequest = new(
			(SemanticMeaning)selectedSemanticMeaning,
			(Priority)selectedPriority
		);

		ImGui.Separator();
		ImGui.TextUnformatted($"Current Request: {currentRequest}");

		// Get the color using the semantic color mapper
		if (GetColorFromTheme(currentRequest) is { } mappedColor)
		{
			// Color preview
			float previewSize = 150f;
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();
			Vector2 pos = ImGui.GetCursorScreenPos();
			Srgb mappedSrgb = mappedColor.ToSrgb();
			Oklch mappedOklch = mappedColor.ToOklch();
			Vector4 rgbColor = ToImVec4(mappedColor);
			drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
								   ImGui.ColorConvertFloat4ToU32(rgbColor));
			drawList.AddRect(pos, new Vector2(pos.X + previewSize, pos.Y + previewSize),
							ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.5f)));
			ImGui.Dummy(new Vector2(previewSize, previewSize));

			ImGui.SameLine();
			ImGui.BeginGroup();

			ImGui.TextUnformatted("Mapped Color Properties:");
			ImGui.TextUnformatted($"Hex: {mappedColor.ToHex()}");
			ImGui.TextUnformatted($"RGB: ({mappedSrgb.R:F3}, {mappedSrgb.G:F3}, {mappedSrgb.B:F3})");
			ImGui.TextUnformatted($"Lightness: {mappedColor.ToOklab().L:F2}");
			ImGui.TextUnformatted($"Chroma: {mappedOklch.C:F2}");
			ImGui.TextUnformatted($"Hue: {mappedOklch.H:F2}");

			ImGui.EndGroup();
		}
		else
		{
			ImGui.TextUnformatted("No color available for this semantic meaning.");
		}

		// Show interpolation/extrapolation demonstration
		ImGui.Separator();
		ImGui.TextUnformatted("Priority-Based Lightness Mapping Demonstration:");

		if (ImGui.CollapsingHeader("Lightness-Based Priority System", ImGuiTreeNodeFlags.DefaultOpen))
		{
			// Show how all priorities map to lightness values
			Priority[] allPriorities = Enum.GetValues<Priority>();
			SemanticMeaning currentMeaning = (SemanticMeaning)selectedSemanticMeaning;

			// Get original colors available for this semantic
			if (theme.SemanticMapping.TryGetValue(currentMeaning, out Collection<Color>? availableColors))
			{
				ImGui.TextUnformatted($"Original colors for {currentMeaning}: {availableColors.Count}");

				// Show original color lightness values
				ImGui.Indent();
				foreach (Color originalColor in availableColors)
				{
					ImGui.TextColored(ToImVec4(originalColor), $"• {originalColor.ToHex()} (L: {originalColor.ToOklab().L:F2})");
				}
				ImGui.Unindent();

				ImGui.Separator();

				// Show what each priority maps to
				ImGui.TextUnformatted("Priority → Mapped Lightness (Interpolation/Extrapolation):");

				// Get complete mapping for this semantic meaning (more efficient than individual requests)
				IReadOnlyDictionary<SemanticColorRequest, Color> completeMapping = GetCompleteMappingForSemantic();

				if (ImGui.BeginTable("PriorityMappingTable", 5, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
				{
					ImGui.TableSetupColumn("Priority");
					ImGui.TableSetupColumn("Target L");
					ImGui.TableSetupColumn("Actual L");
					ImGui.TableSetupColumn("Color");
					ImGui.TableSetupColumn("Method");
					ImGui.TableHeadersRow();

					foreach (Priority priority in allPriorities)
					{
						SemanticColorRequest request = new(currentMeaning, priority);
						if (completeMapping.TryGetValue(request, out Color color))
						{
							ImGui.TableNextRow();

							ImGui.TableSetColumnIndex(0);
							ImGui.TextUnformatted(priority.ToString());

							ImGui.TableSetColumnIndex(1);
							// Calculate what the target lightness would be for this priority
							float targetLightness = CalculateTargetLightnessForPriority(priority);
							ImGui.TextUnformatted($"{targetLightness:F2}");

							ImGui.TableSetColumnIndex(2);
							ImGui.TextUnformatted($"{color.ToOklab().L:F2}");

							ImGui.TableSetColumnIndex(3);
							// Small color swatch
							ImDrawListPtr tableDrawList = ImGui.GetWindowDrawList();
							Vector2 tablePos = ImGui.GetCursorScreenPos();
							float swatchSize = 15f;
							tableDrawList.AddRectFilled(tablePos, new Vector2(tablePos.X + swatchSize, tablePos.Y + swatchSize),
														ImGui.ColorConvertFloat4ToU32(ToImVec4(color)));
							ImGui.Dummy(new Vector2(swatchSize, swatchSize));

							ImGui.TableSetColumnIndex(4);
							// Determine if this was interpolated or extrapolated
							string method = DetermineMethod(availableColors, color);
							ImGui.TextUnformatted(method);
						}
					}

					ImGui.EndTable();
				}
			}
		}
	}

	private static float CalculateTargetLightnessForPriority(Priority priority)
	{
		// Calculate the global lightness range
		double minLightness = double.MaxValue;
		double maxLightness = double.MinValue;

		foreach (Collection<Color> colors in theme.SemanticMapping.Values)
		{
			foreach (Color color in colors)
			{
				double l = color.ToOklab().L;
				if (l < minLightness)
				{
					minLightness = l;
				}
				if (l > maxLightness)
				{
					maxLightness = l;
				}
			}
		}

		// Get all priorities and find the position of the current priority
		Priority[] allPriorities = Enum.GetValues<Priority>();
		int priorityIndex = Array.IndexOf(allPriorities, priority);

		if (allPriorities.Length == 1)
		{
			return (float)((minLightness + maxLightness) / 2.0);
		}

		double position = priorityIndex / (double)(allPriorities.Length - 1);
		double lightnessRange = maxLightness - minLightness;

		if (theme.IsDarkTheme)
		{
			return (float)(minLightness + (position * lightnessRange));
		}
		else
		{
			return (float)(maxLightness - (position * lightnessRange));
		}
	}

	private static string DetermineMethod(Collection<Color> availableColors, Color resultColor)
	{
		if (availableColors.Count == 1)
		{
			return availableColors.First().ToOklab().L == resultColor.ToOklab().L ? "Original" : "Extrapolated";
		}

		List<Color> sortedColors = [.. availableColors.OrderBy(c => c.ToOklab().L)];
		double minL = sortedColors.First().ToOklab().L;
		double maxL = sortedColors.Last().ToOklab().L;

		if (resultColor.ToOklab().L < minL - 0.01 || resultColor.ToOklab().L > maxL + 0.01)
		{
			return "Extrapolated";
		}
		else if (availableColors.Any(c => Math.Abs(c.ToOklab().L - resultColor.ToOklab().L) < 0.01))
		{
			return "Original";
		}
		else
		{
			return "Interpolated";
		}
	}

	private static void RenderImGuiMapping()
	{
		ImGui.TextUnformatted("Systematic ImGui palette mapping from semantic theme:");
		ImGui.Separator();

		// Show mapper info
		ImGui.TextUnformatted($"Framework: {imguiMapper.FrameworkName}");

		// Get the mapped colors
		IReadOnlyDictionary<ImGuiCol, Vector4> mappedColors = imguiMapper.MapTheme(theme);

		ImGui.TextUnformatted($"Total Mapped Colors: {mappedColors.Count}");

		ImGui.Separator();
		ImGui.TextUnformatted("Complete ImGui Color Mapping:");

		// Show all mapped colors in a table
		if (ImGui.BeginTable("ImGuiColorTable", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
		{
			ImGui.TableSetupColumn("ImGui Color");
			ImGui.TableSetupColumn("Color Preview");
			ImGui.TableSetupColumn("RGB Values");
			ImGui.TableHeadersRow();

			foreach ((ImGuiCol colorKey, Vector4 colorValue) in mappedColors.OrderBy(kvp => kvp.Key.ToString()))
			{
				ImGui.TableNextRow();

				ImGui.TableSetColumnIndex(0);
				ImGui.TextUnformatted(colorKey.ToString());

				ImGui.TableSetColumnIndex(1);
				// Color swatch
				ImDrawListPtr drawList = ImGui.GetWindowDrawList();
				Vector2 pos = ImGui.GetCursorScreenPos();
				float swatchSize = 20f;
				drawList.AddRectFilled(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
								   ImGui.ColorConvertFloat4ToU32(colorValue));
				drawList.AddRect(pos, new Vector2(pos.X + swatchSize, pos.Y + swatchSize),
								ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 0.3f)));
				ImGui.Dummy(new Vector2(swatchSize, swatchSize));

				ImGui.TableSetColumnIndex(2);
				ImGui.TextUnformatted($"({colorValue.X:F3}, {colorValue.Y:F3}, {colorValue.Z:F3}, {colorValue.W:F3})");
			}

			ImGui.EndTable();
		}

		ImGui.Separator();
		ImGui.TextUnformatted("Benefits of Systematic Palette Mapping:");
		ImGui.BulletText("Consistent semantic color usage across all ImGui elements");
		ImGui.BulletText("Automatic mapping from semantic meanings to ImGui colors");
		ImGui.BulletText("Easy to adapt to other UI frameworks using the same pattern");
		ImGui.BulletText("Priority-based color interpolation within semantic ranges");
		ImGui.BulletText("Theme-aware color ordering (light vs dark themes)");

		ImGui.Separator();
		ImGui.TextUnformatted("Framework Integration Example:");
		ImGui.Text("// Apply complete theme with one line:\nvar colors = mapper.MapTheme(theme);\n\n// Instead of manually mapping dozens of colors:\n// colors[ImGuiCol.Text] = GetColor(...);\n// colors[ImGuiCol.Button] = GetColor(...);\n// ... (50+ more lines)");
	}

	private static void RenderAccessibilityDemo()
	{
		ImGui.TextUnformatted("Test color combinations for WCAG accessibility compliance:");
		ImGui.Separator();

		// Color pickers
		ImGui.TextUnformatted("Foreground Color:");
		ImGui.ColorEdit3("##Foreground", ref selectedColorVec);

		ImGui.TextUnformatted("Background Color:");
		ImGui.ColorEdit3("##Background", ref backgroundColorVec);

		ImGui.Checkbox("Large Text (18pt+ or 14pt+ bold)", ref isLargeText);

		// Convert to Color (from sRGB picker values)
		Color foregroundColor = Color.FromSrgb(selectedColorVec.X, selectedColorVec.Y, selectedColorVec.Z, 1.0);
		Color backgroundColor = Color.FromSrgb(backgroundColorVec.X, backgroundColorVec.Y, backgroundColorVec.Z, 1.0);

		// Calculate accessibility metrics
		double contrastRatio = foregroundColor.ContrastRatio(backgroundColor);
		AccessibilityLevel accessibilityLevel = foregroundColor.AccessibilityLevelAgainst(backgroundColor, isLargeText);

		ImGui.Separator();

		// Results
		ImGui.TextUnformatted($"Contrast Ratio: {contrastRatio:F2}:1");

		// Color-coded accessibility level
		Vector4 levelColor = accessibilityLevel switch
		{
			AccessibilityLevel.AAA => new Vector4(0, 1, 0, 1), // Green
			AccessibilityLevel.AA => new Vector4(1, 1, 0, 1),  // Yellow
			_ => new Vector4(1, 0, 0, 1) // Red
		};

		ImGui.TextColored(levelColor, $"Accessibility Level: {accessibilityLevel}");

		// Requirements
		float aaRequired = isLargeText ? 3.0f : 4.5f;
		float aaaRequired = isLargeText ? 4.5f : 7.0f;

		ImGui.TextUnformatted($"Required for AA: {aaRequired:F1}:1 {(contrastRatio >= aaRequired ? "✓" : "✗")}");
		ImGui.TextUnformatted($"Required for AAA: {aaaRequired:F1}:1 {(contrastRatio >= aaaRequired ? "✓" : "✗")}");

		// Preview text
		ImGui.Separator();
		ImGui.TextUnformatted("Preview:");

		ImDrawListPtr drawList = ImGui.GetWindowDrawList();
		Vector2 pos = ImGui.GetCursorScreenPos();
		Vector2 previewSize = new(400, 100);

		// Background
		drawList.AddRectFilled(pos, new Vector2(pos.X + previewSize.X, pos.Y + previewSize.Y),
							   ImGui.ColorConvertFloat4ToU32(new Vector4(backgroundColorVec, 1)));

		// Text
		ImGui.SetCursorScreenPos(new Vector2(pos.X + 10, pos.Y + 10));
		ImGui.TextColored(new Vector4(selectedColorVec, 1), isLargeText ? "Large Text Sample (18pt+)" : "Normal Text Sample");

		ImGui.Dummy(previewSize);
	}

	private static void RenderUIPreview()
	{
		ImGui.TextUnformatted("Live preview of semantic theme applied to various UI elements:");
		ImGui.Separator();

		// Semantic buttons
		ImGui.TextUnformatted("Semantic Buttons:");

		if (ImGui.Button("Primary"))
		{
			// Action
		}
		ImGui.SameLine();

		if (GetColorFromTheme(new(SemanticMeaning.Alternate, Priority.MediumHigh)) is { } alternateColor)
		{
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(alternateColor));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(alternateColor, 1.1f)));
			if (ImGui.Button("Alternate"))
			{
				// Action
			}
			ImGui.PopStyleColor(2);
			ImGui.SameLine();
		}

		if (GetColorFromTheme(new(SemanticMeaning.CallToAction, Priority.MediumHigh)) is { } ctaColor)
		{
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(ctaColor));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(ctaColor, 1.1f)));
			if (ImGui.Button("Call-to-Action"))
			{
				// Action
			}
			ImGui.PopStyleColor(2);
			ImGui.SameLine();
		}

		if (GetColorFromTheme(new(SemanticMeaning.Caution, Priority.MediumHigh)) is { } cautionColor)
		{
			ImGui.PushStyleColor(ImGuiCol.Button, ToImVec4(cautionColor));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ToImVec4(AdjustBrightness(cautionColor, 1.1f)));
			if (ImGui.Button("Caution"))
			{
				// Action
			}
			ImGui.PopStyleColor(2);
		}

		ImGui.Separator();

		// Form elements
		ImGui.TextUnformatted("Form Elements:");
		ImGui.SliderFloat("Slider", ref sliderValue, 0f, 1f);
		ImGui.Checkbox("Checkbox", ref checkboxValue);
		ImGui.InputText("Input", ref textValue, 100);

		ImGui.Separator();

		// Progress bars with semantic colors
		ImGui.TextUnformatted("Progress Indicators:");

		if (GetColorFromTheme(new(SemanticMeaning.Success, Priority.High)) is { } successWidget)
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(successWidget));
			ImGui.ProgressBar(0.7f, new Vector2(0, 0), "70% Complete");
			ImGui.PopStyleColor();
		}

		if (GetColorFromTheme(new(SemanticMeaning.Warning, Priority.High)) is { } warningWidget)
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(warningWidget));
			ImGui.ProgressBar(0.4f, new Vector2(0, 0), "40% Warning");
			ImGui.PopStyleColor();
		}

		if (GetColorFromTheme(new(SemanticMeaning.Error, Priority.High)) is { } errorWidget)
		{
			ImGui.PushStyleColor(ImGuiCol.PlotHistogram, ToImVec4(errorWidget));
			ImGui.ProgressBar(0.2f, new Vector2(0, 0), "20% Critical");
			ImGui.PopStyleColor();
		}

		ImGui.Separator();

		// Semantic text
		ImGui.TextUnformatted("Semantic Text:");

		if (GetColorFromTheme(new(SemanticMeaning.Success, Priority.VeryHigh)) is { } successText)
		{
			ImGui.TextColored(ToImVec4(successText), "Success: Operation completed successfully");
		}

		if (GetColorFromTheme(new(SemanticMeaning.Warning, Priority.VeryHigh)) is { } warningText)
		{
			ImGui.TextColored(ToImVec4(warningText), "Warning: Please review your input");
		}

		if (GetColorFromTheme(new(SemanticMeaning.Error, Priority.VeryHigh)) is { } errorText)
		{
			ImGui.TextColored(ToImVec4(errorText), "Error: Operation failed");
		}

		if (GetColorFromTheme(new(SemanticMeaning.Information, Priority.VeryHigh)) is { } infoText)
		{
			ImGui.TextColored(ToImVec4(infoText), "Information: Additional details available");
		}
	}

	// Cache for complete theme palette
	private static ISemanticTheme? cachedTheme;
	private static IReadOnlyDictionary<SemanticColorRequest, Color>? cachedCompletePalette;

	private static Color? GetColorFromTheme(SemanticColorRequest request)
	{
		// Check if we have cached complete palette for this theme
		if (cachedTheme != theme || cachedCompletePalette == null)
		{
			// Generate complete palette for entire theme (more efficient than individual requests)
			cachedCompletePalette = SemanticColorMapper.MakeCompletePalette(theme);
			cachedTheme = theme;
		}

		return cachedCompletePalette.TryGetValue(request, out Color color) ? color : null;
	}

	private static IReadOnlyDictionary<SemanticColorRequest, Color> GetCompleteMappingForSemantic()
	{
		// Check if we have cached complete palette for this theme
		if (cachedTheme != theme || cachedCompletePalette == null)
		{
			// Generate complete palette for entire theme
			cachedCompletePalette = SemanticColorMapper.MakeCompletePalette(theme);
			cachedTheme = theme;
		}

		return cachedCompletePalette;
	}

	private static Vector4 ToImVec4(Color color, float alpha = 1.0f)
	{
		Srgb srgb = color.ToSrgb();
		return new Vector4((float)srgb.R, (float)srgb.G, (float)srgb.B, alpha);
	}

	private static Color AdjustBrightness(Color color, float factor)
	{
		Srgb srgb = color.ToSrgb();
		return Color.FromSrgb(
			Math.Clamp(srgb.R * factor, 0.0, 1.0),
			Math.Clamp(srgb.G * factor, 0.0, 1.0),
			Math.Clamp(srgb.B * factor, 0.0, 1.0),
			color.A
		);
	}
}
