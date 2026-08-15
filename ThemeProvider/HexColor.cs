// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

using ktsu.Semantics.Color;

/// <summary>
/// A color written the way theme authors publish it: as a hex string such as <c>#1e1e2e</c>.
/// </summary>
/// <remarks>
/// Themes declare their palettes in the notation used by the upstream project they mirror, so the
/// checked-in values can be diffed directly against that project's published specification. The
/// implicit conversion from <see cref="string"/> keeps <see cref="SemanticPalette"/> initializers
/// terse; conversion to a <see cref="Color"/> happens once, when the palette is first resolved.
/// </remarks>
/// <param name="Value">The hex notation of the color, for example <c>#1e1e2e</c>.</param>
public readonly record struct HexColor(string Value)
{
	/// <summary>
	/// Converts a hex string such as <c>#1e1e2e</c> into a <see cref="HexColor"/>.
	/// </summary>
	/// <param name="value">The hex notation of the color.</param>
	public static implicit operator HexColor(string value) => new(value);

	/// <summary>
	/// Converts a hex string such as <c>#1e1e2e</c> into a <see cref="HexColor"/>.
	/// </summary>
	/// <param name="value">The hex notation of the color.</param>
	/// <returns>The equivalent <see cref="HexColor"/>.</returns>
	public static HexColor FromString(string value) => new(value);

	/// <summary>
	/// Parses this value into a <see cref="Color"/>.
	/// </summary>
	/// <returns>The parsed color.</returns>
	/// <exception cref="InvalidOperationException">The value was never assigned.</exception>
	public Color ToColor() => Value is null
		? throw new InvalidOperationException("Hex color was not assigned a value.")
		: Color.FromHex(Value);

	/// <summary>
	/// Returns the hex notation of this color.
	/// </summary>
	/// <returns>The hex notation, or an empty string if the value was never assigned.</returns>
	public override string ToString() => Value ?? string.Empty;
}
