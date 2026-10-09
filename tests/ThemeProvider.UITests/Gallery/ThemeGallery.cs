// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ktsu.ImGui.App;
using ktsu.ImGui.App.Testing;

/// <summary>
/// Photographs every theme for <c>docs/gallery</c>: one picture per <see cref="GalleryCatalog"/>
/// entry, and an index that captions them.
/// </summary>
/// <remarks>
/// <para>
/// These run with the rest of the UI tests, so every pull request proves each picture can still be
/// drawn. Pictures are written to a temporary directory unless <c>THEMEPROVIDER_GALLERY_OUT</c> names
/// one, which is how the gallery workflow, and anyone regenerating by hand, sends them to
/// <c>docs/gallery</c>.
/// </para>
/// <para>
/// Each entry starts its own harness, so a picture never depends on the one taken before it. The
/// display is exactly the size of the picture and the mouse is parked off it, so nothing is hovered.
/// </para>
/// </remarks>
[TestClass]
public sealed class ThemeGallery
{
	/// <summary>The environment variable naming the directory the gallery is written to.</summary>
	internal const string OutputVariable = "THEMEPROVIDER_GALLERY_OUT";

	/// <summary>Frames drawn before the picture is taken, so tab bars and tables have settled their layout.</summary>
	private const int SettleFrames = 4;

	private static readonly Lazy<string> TemporaryOutput = new(() =>
		Path.Combine(Path.GetTempPath(), $"themeprovider-gallery-{Guid.NewGuid():N}"));

	/// <summary>Gets or sets the context of the running test.</summary>
	public TestContext TestContext { get; set; } = null!;

	/// <summary>Gets every entry's name, one test case each.</summary>
	public static IEnumerable<object[]> EntryNames => GalleryCatalog.Entries.Select(entry => new object[] { entry.Name });

	/// <summary>Gets the directory pictures are written to.</summary>
	internal static string OutputDirectory =>
		Environment.GetEnvironmentVariable(OutputVariable) is string output && output.Length > 0
			? Path.GetFullPath(output)
			: TemporaryOutput.Value;

	/// <summary>Removes the temporary directory, when pictures went there rather than to a named one.</summary>
	[ClassCleanup]
	public static void DeleteTemporaryOutput()
	{
		if (TemporaryOutput.IsValueCreated && Directory.Exists(TemporaryOutput.Value))
		{
			Directory.Delete(TemporaryOutput.Value, recursive: true);
		}
	}

	[TestMethod]
	[DynamicData(nameof(EntryNames))]
	public void Photograph(string name)
	{
		GalleryEntry entry = GalleryCatalog.Entries.Single(candidate => candidate.Name == name);

		ImGuiAppConfig config = new()
		{
			Title = "Theme gallery",
			OnRender = _ => entry.Draw(),
		};

		using ImGuiAppHarness harness = ImGuiAppHarness.Start(config, new HarnessOptions { Width = entry.Width, Height = entry.Height });
		Assert.IsTrue(GalleryFonts.Load(), "ImGuiApp's own font could not be found, so the pictures would not look like an application.");
		entry.Prepare();
		harness.Mouse.MoveTo(-100f, -100f);
		harness.Step(SettleFrames);

		Bitmap32 picture = harness.Target;
		Assert.AreEqual(entry.Width, picture.Width);
		Assert.AreEqual(entry.Height, picture.Height);

		Directory.CreateDirectory(OutputDirectory);
		string path = Path.Combine(OutputDirectory, entry.Slug + ".png");
		picture.SavePng(path);
		TestContext.WriteLine($"Wrote {path} ({picture.Width}x{picture.Height}).");
	}

	[TestMethod]
	public void WriteTheIndex()
	{
		string[] slugs = [.. GalleryCatalog.Entries.Select(entry => entry.Slug)];
		Assert.HasCount(slugs.Length, slugs.Distinct(StringComparer.Ordinal), "Two gallery entries would write the same file.");
		Assert.IsTrue(slugs.All(slug => slug.Length > 0), "An entry's name has no letters or digits to name its file after.");

		Directory.CreateDirectory(OutputDirectory);
		File.WriteAllText(Path.Combine(OutputDirectory, "README.md"), GalleryIndex.Render(GalleryCatalog.Entries));
	}

	[TestMethod]
	public void EveryThemeHasAPicture()
	{
		string[] names = [.. GalleryCatalog.Entries.Select(entry => entry.Name)];
		foreach (ThemeRegistry.ThemeInfo info in ThemeRegistry.AllThemes)
		{
			Assert.Contains(info.Name, names, $"{info.Name} is in the registry but not in the gallery.");
		}
	}
}
