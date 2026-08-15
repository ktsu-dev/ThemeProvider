// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

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
	/// darkest surface colors; <see cref="SemanticColorMapper"/> interpolates between them.
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

	private KeyValuePair<SemanticMeaning, Color[]>[] Resolve() =>
	[
		new(SemanticMeaning.Neutral, [.. Neutrals.Select(n => n.ToColor())]),
		new(SemanticMeaning.Primary, [Primary.ToColor()]),
		new(SemanticMeaning.Alternate, [Alternate.ToColor()]),
		new(SemanticMeaning.Success, [Success.ToColor()]),
		new(SemanticMeaning.CallToAction, [CallToAction.ToColor()]),
		new(SemanticMeaning.Information, [Information.ToColor()]),
		new(SemanticMeaning.Caution, [Caution.ToColor()]),
		new(SemanticMeaning.Warning, [Warning.ToColor()]),
		new(SemanticMeaning.Error, [Error.ToColor()]),
		new(SemanticMeaning.Failure, [Failure.ToColor()]),
		new(SemanticMeaning.Debug, [Debug.ToColor()]),
	];
}
