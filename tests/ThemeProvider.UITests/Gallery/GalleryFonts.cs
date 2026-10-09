// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;

using Hexa.NET.ImGui;

using ktsu.ImGui.App;

/// <summary>Gives the gallery the font an <c>ImGuiApp</c> application draws in.</summary>
/// <remarks>
/// The harness never builds <c>ImGuiApp</c>'s own fonts, so it draws in Dear ImGui's built-in bitmap
/// font. That is wrong for pictures meant to show what a themed application looks like, so this loads
/// the Nerd Font <c>ImGuiApp</c> ships and makes it the default, which is what ImGuiApp's own widget
/// gallery does for the same reason.
/// </remarks>
internal static class GalleryFonts
{
	/// <summary>The size <c>ImGuiApp</c> draws its interface at, at a scale of one.</summary>
	private const float Pixels = 14f;

	private static readonly Lazy<byte[]?> NerdFont = new(() =>
	{
		try
		{
			ResourceManager resources = new("ktsu.ImGui.App.Resources.Resources", typeof(ImGuiApp).Assembly);
			return resources.GetObject("NerdFont", CultureInfo.InvariantCulture) as byte[];
		}
		catch (MissingManifestResourceException)
		{
			return null;
		}
	});

	/// <summary>Adds the application's font and makes it the default. Call between frames.</summary>
	/// <returns>True when the font was loaded; false leaves the built-in font in place.</returns>
	[SuppressMessage("Major Code Smell", "S6640:Make sure that using \"unsafe\" is safe here", Justification = "FontHelper's glyph range helper returns a pointer ImGui owns for the atlas's lifetime; it is passed straight through and never dereferenced here.")]
	internal static bool Load()
	{
		if (NerdFont.Value is not byte[] data)
		{
			return false;
		}

		ImGuiIOPtr io = ImGui.GetIO();
		unsafe
		{
			ImFontPtr? font = FontHelper.AddCustomFont(io, data, Pixels, FontHelper.GetExtendedUnicodeRanges(io.Fonts));
			if (font is not ImFontPtr loaded)
			{
				return false;
			}

			io.FontDefault = loaded;
		}

		return true;
	}
}
