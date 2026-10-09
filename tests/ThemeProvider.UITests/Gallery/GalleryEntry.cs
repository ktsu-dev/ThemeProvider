// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider.UITests.Gallery;

using System;
using System.Text;

/// <summary>One picture in the gallery: what it is called, what it shows, and how it is drawn.</summary>
/// <param name="Name">The heading the picture is captioned with.</param>
/// <param name="Description">The caption under the heading.</param>
/// <param name="Width">The width of the picture, which is also the width of the display it is drawn on.</param>
/// <param name="Height">The height of the picture, which is also the height of the display it is drawn on.</param>
/// <param name="Prepare">Runs once between frames before anything is drawn, to set the style the picture is taken in.</param>
/// <param name="Draw">Draws the picture, once per frame.</param>
internal sealed record GalleryEntry(string Name, string Description, int Width, int Height, Action Prepare, Action Draw)
{
	/// <summary>Gets the file name the picture is written to, without its extension.</summary>
	internal string Slug => MakeSlug(Name);

	/// <summary>Turns a name into a lower-case, hyphenated file name.</summary>
	internal static string MakeSlug(string name)
	{
		StringBuilder slug = new(name.Length);
		bool pendingHyphen = false;
		foreach (char character in name)
		{
			if (char.IsAsciiLetterOrDigit(character))
			{
				if (pendingHyphen && slug.Length > 0)
				{
					slug.Append('-');
				}

				slug.Append(char.ToLowerInvariant(character));
				pendingHyphen = false;
			}
			else
			{
				pendingHyphen = true;
			}
		}

		return slug.ToString();
	}
}
