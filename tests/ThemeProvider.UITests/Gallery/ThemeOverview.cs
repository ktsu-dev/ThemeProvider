// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Hexa.NET.ImGui;

using ktsu.Semantics.Color;
using ktsu.ThemeProvider;

/// <summary>
/// Every theme on one page: a strip per theme in its own background colour, carrying its name in its
/// own text colour, its neutral ramp, and one chip per accent meaning.
/// </summary>
internal static class ThemeOverview
{
	private const int Columns = 2;
	private const float Margin = 12f;
	private const float RowHeight = 24f;
	private const float RowGap = 4f;
	private const float ColumnWidth = 520f;
	private const float NameWidth = 200f;
	private const float RampSwatch = 14f;
	private const float Chip = 16f;
	private const float ChipGap = 3f;

	private static readonly SemanticMeaning[] Accents = [.. Enum.GetValues<SemanticMeaning>().Where(meaning => meaning != SemanticMeaning.Neutral)];

	private static readonly Lazy<IReadOnlyList<(ThemeRegistry.ThemeInfo Info, IReadOnlyDictionary<SemanticColorRequest, Color> Palette)>> Themes = new(() =>
		[.. ThemeRegistry.AllThemes.Select(info => (info, SemanticColorMapper.MakeCompletePalette(info.CreateInstance())))]);

	private static int RowsPerColumn => (ThemeRegistry.AllThemes.Count + Columns - 1) / Columns;

	/// <summary>Gets the width of the picture.</summary>
	internal static int Width => (int)((Margin * (Columns + 1)) + (ColumnWidth * Columns));

	/// <summary>Gets the height of the picture.</summary>
	internal static int Height => (int)((Margin * 2) + (RowsPerColumn * (RowHeight + RowGap)) - RowGap);

	/// <summary>Draws the overview over the whole display. Call once per frame.</summary>
	internal static void Draw()
	{
		ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
		ImGui.SetNextWindowSize(new Vector2(Width, Height), ImGuiCond.Always);
		ImGui.SetNextWindowFocus();
		ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
		ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.10f, 0.10f, 0.11f, 1f));
		if (ImGui.Begin("##overview", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoSavedSettings))
		{
			ImDrawListPtr drawList = ImGui.GetWindowDrawList();

			int index = 0;
			foreach ((ThemeRegistry.ThemeInfo info, IReadOnlyDictionary<SemanticColorRequest, Color> palette) in Themes.Value)
			{
				int column = index / RowsPerColumn;
				int row = index % RowsPerColumn;
				Vector2 origin = new(Margin + (column * (ColumnWidth + Margin)), Margin + (row * (RowHeight + RowGap)));
				DrawStrip(drawList, origin, info, palette);
				index++;
			}
		}

		ImGui.End();
		ImGui.PopStyleColor();
		ImGui.PopStyleVar();
	}

	private static void DrawStrip(ImDrawListPtr drawList, Vector2 origin, ThemeRegistry.ThemeInfo info, IReadOnlyDictionary<SemanticColorRequest, Color> palette)
	{
		uint background = Lookup(palette, SemanticMeaning.Neutral, Priority.VeryLow);
		uint text = Lookup(palette, SemanticMeaning.Neutral, Priority.VeryHigh);
		drawList.AddRectFilled(origin, origin + new Vector2(ColumnWidth, RowHeight), background, 4f);

		float textY = origin.Y + ((RowHeight - ImGui.GetFontSize()) / 2f);
		drawList.AddText(new Vector2(origin.X + 8f, textY), text, info.Name);

		float x = origin.X + NameWidth;
		float rampY = origin.Y + ((RowHeight - RampSwatch) / 2f);
		foreach (Priority priority in Enum.GetValues<Priority>())
		{
			drawList.AddRectFilled(new Vector2(x, rampY), new Vector2(x + RampSwatch, rampY + RampSwatch), Lookup(palette, SemanticMeaning.Neutral, priority));
			x += RampSwatch;
		}

		x += 14f;
		float chipY = origin.Y + ((RowHeight - Chip) / 2f);
		foreach (SemanticMeaning meaning in Accents)
		{
			drawList.AddRectFilled(new Vector2(x, chipY), new Vector2(x + Chip, chipY + Chip), Lookup(palette, meaning, Priority.Medium), 3f);
			x += Chip + ChipGap;
		}
	}

	private static uint Lookup(IReadOnlyDictionary<SemanticColorRequest, Color> palette, SemanticMeaning meaning, Priority priority) =>
		palette.TryGetValue(new SemanticColorRequest(meaning, priority), out Color color)
			? ImGui.ColorConvertFloat4ToU32(ThemeStyle.ToVector4(color))
			: 0u;
}
