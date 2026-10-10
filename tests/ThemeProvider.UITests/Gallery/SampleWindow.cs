// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System;
using System.Collections.Generic;
using System.Numerics;

using Hexa.NET.ImGui;

using ktsu.Semantics.Color;
using ktsu.ThemeProvider;

/// <summary>
/// The window every theme is photographed in: a fixed selection of widgets on the left, and the
/// theme's semantic palette on the right.
/// </summary>
/// <remarks>
/// Every value shown is a constant, so nothing in a picture depends on the clock, the machine or the
/// frame it was taken on. Only the theme changes from one picture to the next.
/// </remarks>
internal sealed class SampleWindow(ThemeRegistry.ThemeInfo info)
{
	/// <summary>The width of the window, and of the picture.</summary>
	internal const int Width = 760;

	/// <summary>The height of the window, and of the picture.</summary>
	internal const int Height = 448;

	private const float SwatchSize = 16f;
	private const float PaletteColumnWidth = 240f;

	private static readonly string[] Modes = ["Automatic", "Manual", "Scheduled"];

	private static readonly (string Stage, string Status, string Time)[] Stages =
	[
		("Restore", "Passed", "12 s"),
		("Build", "Passed", "48 s"),
		("Test", "Running", "1 m 30 s"),
		("Package", "Queued", "-"),
	];

	private static readonly (SemanticMeaning Meaning, string Text)[] Messages =
	[
		(SemanticMeaning.Success, "Success: all 214 tests passed"),
		(SemanticMeaning.Information, "Information: 3 packages can be updated"),
		(SemanticMeaning.Warning, "Warning: the cache is nearly full"),
		(SemanticMeaning.Error, "Error: the connection was refused"),
	];

	private readonly IReadOnlyDictionary<SemanticColorRequest, Color> palette =
		SemanticColorMapper.MakeCompletePalette(info.CreateInstance());

	/// <summary>Draws the window over the whole display. Call once per frame.</summary>
	internal void Draw()
	{
		ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
		ImGui.SetNextWindowSize(new Vector2(Width, Height), ImGuiCond.Always);
		ImGui.SetNextWindowFocus();
		if (ImGui.Begin(info.Name, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoSavedSettings))
		{
			ImGui.TextDisabled(string.IsNullOrEmpty(info.Variant)
				? $"{info.Family} / {(info.IsDark ? "dark" : "light")}"
				: $"{info.Family} / {info.Variant} / {(info.IsDark ? "dark" : "light")}");
			ImGui.Separator();

			if (ImGui.BeginTable("##layout", 2, ImGuiTableFlags.None))
			{
				ImGui.TableSetupColumn("##widgets", ImGuiTableColumnFlags.WidthStretch);
				ImGui.TableSetupColumn("##palette", ImGuiTableColumnFlags.WidthFixed, PaletteColumnWidth);
				ImGui.TableNextRow();
				ImGui.TableNextColumn();
				DrawWidgets();
				ImGui.TableNextColumn();
				DrawPalette();
				ImGui.EndTable();
			}
		}

		ImGui.End();
	}

	private void DrawWidgets()
	{
		ImGui.PushItemWidth(260f);
		if (ImGui.BeginTabBar("##tabs"))
		{
			if (ImGui.BeginTabItem("Controls"))
			{
				DrawControls();
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Data"))
			{
				ImGui.EndTabItem();
			}

			if (ImGui.BeginTabItem("Settings"))
			{
				ImGui.EndTabItem();
			}

			ImGui.EndTabBar();
		}

		ImGui.PopItemWidth();

		if (ImGui.CollapsingHeader("Pipeline", ImGuiTreeNodeFlags.DefaultOpen))
		{
			DrawTable();
		}

		ImGui.Spacing();
		foreach ((SemanticMeaning meaning, string text) in Messages)
		{
			if (palette.TryGetValue(new SemanticColorRequest(meaning, Priority.High), out Color color))
			{
				ImGui.TextColored(ThemeStyle.ToVector4(color), text);
			}
		}
	}

	private static void DrawControls()
	{
		ImGui.Button("Save");
		ImGui.SameLine();
		ImGui.Button("Cancel");
		ImGui.SameLine();
		ImGui.SmallButton("Help");

		bool synchronise = true;
		ImGui.Checkbox("Synchronise", ref synchronise);
		ImGui.SameLine();
		bool notify = false;
		ImGui.Checkbox("Notify", ref notify);

		int choice = 1;
		ImGui.RadioButton("Light", ref choice, 0);
		ImGui.SameLine();
		ImGui.RadioButton("Dark", ref choice, 1);
		ImGui.SameLine();
		ImGui.RadioButton("System", ref choice, 2);

		float opacity = 0.62f;
		ImGui.SliderFloat("Opacity", ref opacity, 0f, 1f);

		string name = "ThemeProvider";
		ImGui.InputText("Name", ref name, 64);

		int mode = 0;
		ImGui.Combo("Mode", ref mode, Modes, Modes.Length);

		ImGui.ProgressBar(0.65f, new Vector2(260f, 0f), "65%");
	}

	private static void DrawTable()
	{
		const ImGuiTableFlags flags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp;
		if (!ImGui.BeginTable("##pipeline", 3, flags))
		{
			return;
		}

		ImGui.TableSetupColumn("Stage");
		ImGui.TableSetupColumn("Status");
		ImGui.TableSetupColumn("Time");
		ImGui.TableHeadersRow();

		foreach ((string stage, string status, string time) in Stages)
		{
			ImGui.TableNextRow();
			ImGui.TableNextColumn();
			ImGui.TextUnformatted(stage);
			ImGui.TableNextColumn();
			ImGui.TextUnformatted(status);
			ImGui.TableNextColumn();
			ImGui.TextUnformatted(time);
		}

		ImGui.EndTable();
	}

	private void DrawPalette()
	{
		ImGui.TextUnformatted("Semantic palette");
		ImGui.TextDisabled("very low to very high");
		ImGui.Spacing();

		Vector2 size = new(SwatchSize, SwatchSize);
		foreach (SemanticMeaning meaning in Enum.GetValues<SemanticMeaning>())
		{
			foreach (Priority priority in Enum.GetValues<Priority>())
			{
				string id = $"##{meaning}{priority}";
				if (palette.TryGetValue(new SemanticColorRequest(meaning, priority), out Color color))
				{
					ImGui.ColorButton(id, ThemeStyle.ToVector4(color), ImGuiColorEditFlags.NoTooltip | ImGuiColorEditFlags.NoDragDrop, size);
				}
				else
				{
					ImGui.Dummy(size);
				}

				ImGui.SameLine(0f, 2f);
			}

			ImGui.SameLine(0f, 8f);
			ImGui.TextUnformatted(meaning.ToString());
		}
	}
}
