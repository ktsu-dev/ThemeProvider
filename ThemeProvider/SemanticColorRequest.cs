// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

/// <summary>
///
/// </summary>
public readonly record struct SemanticColorRequest
{
	/// <summary>The semantic meaning determining both hue and role</summary>
	public SemanticMeaning Meaning { get; init; }

	/// <summary>The priority level for color assignment ordering</summary>
	public Priority Priority { get; init; }

	/// <summary>
	/// Creates a new semantic color specification.
	/// </summary>
	/// <param name="meaning">The semantic meaning determining both hue and role</param>
	/// <param name="priority">The priority level for color assignment ordering</param>
	public SemanticColorRequest(SemanticMeaning meaning, Priority priority)
	{
		Meaning = meaning;
		Priority = priority;
	}

	/// <summary>
	/// Converts to a readable string representation.
	/// </summary>
	public override string ToString() => $"{Meaning} ({Priority})";
}
