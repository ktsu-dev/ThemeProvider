// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

/// <summary>
/// Interface for mapping semantic themes to UI framework-specific color palettes.
/// Implementations can convert semantic color specifications to concrete framework colors.
/// </summary>
/// <typeparam name="TColorKey">The type used to identify colors in the target framework (e.g., enum, string)</typeparam>
/// <typeparam name="TColorValue">The type used to represent color values in the target framework</typeparam>
public interface IPaletteMapper<TColorKey, TColorValue>
	where TColorKey : notnull
{
	/// <summary>
	/// Gets the name of the target framework this mapper supports.
	/// </summary>
	public string FrameworkName { get; }

	/// <summary>
	/// Maps a semantic theme to a complete color palette for the target framework.
	/// </summary>
	/// <param name="theme">The semantic theme to map from</param>
	/// <returns>A dictionary mapping framework color keys to color values</returns>
	public IReadOnlyDictionary<TColorKey, TColorValue> MapTheme(ISemanticTheme theme);
}
