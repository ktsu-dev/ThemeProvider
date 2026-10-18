// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ktsu.Semantics.Color;

/// <summary>
/// The colors a theme assigns to each <see cref="SemanticMeaning"/>, declared as hex values.
/// </summary>
/// <remarks>
/// Every theme is the same shape: one neutral ramp plus one accent per non-neutral meaning. Themes
/// declare that shape once here rather than each hand-building the
/// <see cref="ISemanticTheme.SemanticMapping"/> dictionary, which keeps a theme definition to the
/// part that is actually specific to it — its colors.
/// </remarks>
public sealed class SemanticPalette
{
	/// <summary>
	/// Gets the neutral ramp, ordered as the theme publishes it. Conventionally the lightest and
	/// darkest surface colors; <see cref="SemanticColorMapper"/> interpolates between them. It must
	/// not be empty: the neutrals supply a theme's text and background.
	/// </summary>
	public IReadOnlyList<HexColor> Neutrals { get; init; } = [];

	/// <summary>Gets the theme's principal accent color.</summary>
	public HexColor Primary { get; init; }

	/// <summary>Gets the secondary accent used to contrast with <see cref="Primary"/>.</summary>
	public HexColor Alternate { get; init; }

	/// <summary>Gets the color denoting a successful outcome.</summary>
	public HexColor Success { get; init; }

	/// <summary>Gets the color denoting the action the user is being steered toward.</summary>
	public HexColor CallToAction { get; init; }

	/// <summary>Gets the color denoting neutral informational content.</summary>
	public HexColor Information { get; init; }

	/// <summary>Gets the color denoting something that warrants care but is not yet a problem.</summary>
	public HexColor Caution { get; init; }

	/// <summary>Gets the color denoting a warning.</summary>
	public HexColor Warning { get; init; }

	/// <summary>Gets the color denoting an error.</summary>
	public HexColor Error { get; init; }

	/// <summary>Gets the color denoting an unrecoverable failure.</summary>
	public HexColor Failure { get; init; }

	/// <summary>Gets the color denoting diagnostic or debug output.</summary>
	public HexColor Debug { get; init; }

	// Hex parsing is done once per palette; SemanticMapping is read repeatedly (once per semantic
	// meaning inside SemanticColorMapper.MapColors alone), so re-parsing on every access would be
	// pure waste.
	private IReadOnlyList<KeyValuePair<SemanticMeaning, Color[]>>? resolved;

	/// <summary>
	/// Builds the semantic mapping this palette describes.
	/// </summary>
	/// <returns>
	/// A fresh, caller-owned dictionary. Each call returns new collections so that a caller mutating
	/// the result cannot corrupt the theme.
	/// </returns>
	/// <exception cref="InvalidOperationException">
	/// <see cref="Neutrals"/> is empty, or an accent was never assigned. The message names the member.
	/// </exception>
	public Dictionary<SemanticMeaning, Collection<Color>> ToSemanticMapping()
	{
		IReadOnlyList<KeyValuePair<SemanticMeaning, Color[]>> entries = resolved ??= Resolve();

		Dictionary<SemanticMeaning, Collection<Color>> mapping = new(entries.Count);
		foreach (KeyValuePair<SemanticMeaning, Color[]> entry in entries)
		{
			mapping[entry.Key] = [.. entry.Value];
		}

		return mapping;
	}

	private KeyValuePair<SemanticMeaning, Color[]>[] Resolve()
	{
		// Without neutrals the mapper silently drops Text, WindowBg and the rest of the surface colors,
		// leaving the toolkit's defaults under an otherwise themed UI, so this is refused rather than
		// mapped.
		if (Neutrals.Count == 0)
		{
			throw new InvalidOperationException($"{nameof(SemanticPalette)}.{nameof(Neutrals)} is empty; a palette needs at least one neutral color.");
		}

		return
		[
			new(SemanticMeaning.Neutral, [.. Neutrals.Select(n => n.ToColor())]),
			new(SemanticMeaning.Primary, [Accent(Primary, nameof(Primary))]),
			new(SemanticMeaning.Alternate, [Accent(Alternate, nameof(Alternate))]),
			new(SemanticMeaning.Success, [Accent(Success, nameof(Success))]),
			new(SemanticMeaning.CallToAction, [Accent(CallToAction, nameof(CallToAction))]),
			new(SemanticMeaning.Information, [Accent(Information, nameof(Information))]),
			new(SemanticMeaning.Caution, [Accent(Caution, nameof(Caution))]),
			new(SemanticMeaning.Warning, [Accent(Warning, nameof(Warning))]),
			new(SemanticMeaning.Error, [Accent(Error, nameof(Error))]),
			new(SemanticMeaning.Failure, [Accent(Failure, nameof(Failure))]),
			new(SemanticMeaning.Debug, [Accent(Debug, nameof(Debug))]),
		];
	}

	private static Color Accent(HexColor value, string member) => value.Value is null
		? throw new InvalidOperationException($"{nameof(SemanticPalette)}.{member} was not assigned a value.")
		: value.ToColor();
}
