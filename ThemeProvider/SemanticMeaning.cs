// Copyright (c) 2023-2026 ktsu-dev contributors

namespace ktsu.ThemeProvider;

/// <summary>
/// Defines semantic color meanings based on purpose rather than specific hue.
/// This determines hue assignment in the semantic color system.
/// </summary>
public enum SemanticMeaning
{
	/// <summary>
	/// Nominal state, neutral interactions that do not require special attention.
	/// </summary>
	Neutral,

	/// <summary>
	/// Primary interactions that the user can choose to interact with.
	/// </summary>
	Primary,

	/// <summary>
	/// Alternate interactions, alternate option when given a binary choice, or to emphasize a context not covered by other semantics.
	/// </summary>
	Alternate,

	/// <summary>
	/// Successful operations, confirmations.
	/// </summary>
	Success,

	/// <summary>
	/// Context sensitive interactions that demand immediate user attention or interaction.
	/// </summary>
	CallToAction,

	/// <summary>
	/// Informational content, help text.
	/// </summary>
	Information,

	/// <summary>
	/// Cautionary content, warnings that need attention.
	/// </summary>
	Caution,

	/// <summary>
	/// Warning states, potentially problematic.
	/// </summary>
	Warning,

	/// <summary>
	/// Error states, something has been detected as incorrect.
	/// </summary>
	Error,

	/// <summary>
	/// Failed operations, an action failed to complete successfully (distinct from error).
	/// </summary>
	Failure,

	/// <summary>
	/// Debug/development information.
	/// </summary>
	Debug
}

