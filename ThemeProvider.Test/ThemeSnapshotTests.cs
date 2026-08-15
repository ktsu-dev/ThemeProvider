// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.Test;

using System.Collections.ObjectModel;
using System.Text;
using ktsu.Semantics.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Pins the exact colors every registered theme declares.
/// <para>
/// Theme palettes are data, and the value of that data is the specific hex codes upstream projects
/// published. A snapshot is the only assertion that actually catches a mistyped digit, so this test
/// renders every theme to text and compares it against a checked-in golden file. Any refactor of how
/// themes are stored must leave this output byte-identical.
/// </para>
/// </summary>
[TestClass]
public class ThemeSnapshotTests
{
	private const string SnapshotFileName = "ThemeSnapshot.approved.txt";

	/// <summary>
	/// Every theme in the registry must produce exactly the palette recorded in the golden file.
	/// </summary>
	[TestMethod]
	public void AllThemes_MatchApprovedSnapshot()
	{
		string actual = RenderSnapshot();
		string expected = ReadApprovedSnapshot();

		Assert.AreEqual(
			expected.ReplaceLineEndings("\n"),
			actual.ReplaceLineEndings("\n"),
			$"Theme palettes changed. If this is intentional, update {SnapshotFileName}.");
	}

	/// <summary>
	/// Renders every registered theme as deterministic text: one line per semantic meaning,
	/// listing the hex value of each color in declaration order.
	/// </summary>
	internal static string RenderSnapshot()
	{
		StringBuilder builder = new();

		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			ISemanticTheme theme = info.CreateInstance();

			builder.Append(info.Name)
				.Append(" [family=").Append(info.Family)
				.Append(", variant=").Append(info.Variant ?? "-")
				.Append(", isDark=").Append(info.IsDark ? "true" : "false")
				.Append(", themeIsDark=").Append(theme.IsDarkTheme ? "true" : "false")
				.Append(']')
				.Append('\n');

			// Enumerate meanings in enum order rather than dictionary order so the snapshot does not
			// depend on insertion order.
			foreach (SemanticMeaning meaning in GetSemanticMeanings())
			{
				if (!theme.SemanticMapping.TryGetValue(meaning, out Collection<Color>? colors))
				{
					continue;
				}

				builder.Append("  ").Append(meaning.ToString()).Append(" =");
				foreach (Color color in colors)
				{
					builder.Append(' ').Append(color.ToHex().ToUpperInvariant());
				}
				builder.Append('\n');
			}
		}

		return builder.ToString();
	}

	private static SemanticMeaning[] GetSemanticMeanings()
	{
		SemanticMeaning[] meanings = Enum.GetValues<SemanticMeaning>();
		Array.Sort(meanings, (a, b) => ((int)a).CompareTo((int)b));
		return meanings;
	}

	private static string ReadApprovedSnapshot()
	{
		string path = Path.Combine(AppContext.BaseDirectory, SnapshotFileName);
		Assert.IsTrue(
			File.Exists(path),
			FormattableString.Invariant($"Approved snapshot not found at {path}."));
		return File.ReadAllText(path, Encoding.UTF8);
	}
}
