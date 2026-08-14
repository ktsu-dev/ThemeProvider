// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.ThemeProvider.Analysis;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

/// <summary>
/// Command-line entry point. Analyzes the registered themes, writes a markdown report, and returns a
/// non-zero exit code when any theme fails a gate (so it doubles as a regression check).
/// </summary>
internal static class Program
{
	private static int Main(string[] args)
	{
		string outputPath = "theme-analysis.md";
		string? themeFilter = null;
		bool strict = false;

		for (int i = 0; i < args.Length; i++)
		{
			switch (args[i])
			{
				case "--output" or "-o" when i + 1 < args.Length:
					outputPath = args[++i];
					break;
				case "--theme" or "-t" when i + 1 < args.Length:
					themeFilter = args[++i];
					break;
				case "--strict":
					strict = true;
					break;
				case "--help" or "-h":
					PrintUsage();
					return 0;
				default:
					Console.Error.WriteLine(Inv($"Unknown or incomplete argument: {args[i]}"));
					PrintUsage();
					return 2;
			}
		}

		IReadOnlyList<ThemeRegistry.ThemeInfo> themes = themeFilter is null
			? ThemeRegistry.AllThemes
			: [.. ThemeRegistry.AllThemes.Where(t => t.Name.Contains(themeFilter, StringComparison.OrdinalIgnoreCase))];

		if (themes.Count == 0)
		{
			Console.Error.WriteLine(Inv($"No themes matched \"{themeFilter}\"."));
			return 2;
		}

		List<ThemeAnalysis> analyses = [.. themes.Select(PaletteAnalyzer.Analyze)];
		string report = MarkdownReport.Build(analyses);
		File.WriteAllText(outputPath, report);

		int failed = analyses.Count(a => a.Status == CheckStatus.Fail);
		int warned = analyses.Count(a => a.Status == CheckStatus.Warn);
		int passed = analyses.Count - failed - warned;

		Console.WriteLine(Inv($"Analyzed {analyses.Count} theme(s): {passed} pass, {warned} warn, {failed} fail."));
		Console.WriteLine(Inv($"Report written to {Path.GetFullPath(outputPath)}"));

		bool regressed = failed > 0 || (strict && warned > 0);
		return regressed ? 1 : 0;
	}

	private static void PrintUsage()
	{
		Console.WriteLine("Usage: ThemeProvider.Analysis [--output <path>] [--theme <name>] [--strict]");
		Console.WriteLine("  --output, -o   Markdown report path (default: theme-analysis.md)");
		Console.WriteLine("  --theme,  -t   Analyze only themes whose name contains this text");
		Console.WriteLine("  --strict       Exit non-zero on warnings as well as failures");
	}

	private static string Inv(FormattableString text) => text.ToString(CultureInfo.InvariantCulture);
}
