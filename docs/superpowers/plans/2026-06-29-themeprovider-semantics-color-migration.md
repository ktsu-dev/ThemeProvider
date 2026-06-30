# ThemeProvider → ktsu.Semantics.Color Migration Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax.

**Goal:** Replace ThemeProvider's in-house color types (`RgbColor`, `SRgbColor`, `OklabColor`, `PerceptualColor`, `ColorMath`, `AccessibilityLevel`, and the color-science half of `ColorRange`) with the `ktsu.Semantics.Color` NuGet package (2.3.0), fixing the long-standing sRGB-as-linear gamma bug in the process.

**Architecture:** The public currency type `PerceptualColor` becomes `ktsu.Semantics.Color.Color` (linear RGB + alpha) across `ISemanticTheme`, `SemanticColorMapper`, the ~40 themes, and `ImGuiPaletteMapper` (breaking → major version bump). Theme base colors are parsed with `Color.FromHex` (correct sRGB decode) and emitted to ImGui via `ToSrgbVector4()`, so **base palette colors render identically** (hex→linear→sRGB round-trips) while the semantic mapper's **derived** colors and accessibility numbers become correct. Bespoke business logic (lightness bucketing, in-gamut chroma fitting, dark/light ordering) is re-homed onto `Color`.

**Tech Stack:** C# / .NET, ktsu.Sdk, multi-target (unchanged — `Semantics.Color` supports netstandard2.0/2.1 + net8–10, so ThemeProvider keeps all its TFMs), Central Package Management, MSTest (new test project), Hexa.NET.ImGui (ImGui package).

**Source map:** see the migration-surface report (mapping table) captured in the session; key public-API signatures cited inline below.

## Global Constraints

- **Gamma rule (the point of this migration):** parse theme hex with `Color.FromHex(...)`; emit to ImGui with `ToSrgbVector4()`. Never `Color.FromLinear(bytes)` and never `ToLinearVector4()` for ImGui output — that would re-introduce the bug.
- File header on every .cs file: `// Copyright (c) ktsu.dev` / `// All rights reserved.` / `// Licensed under the MIT license.`
- Tabs; CRLF; no UTF-8 BOM on .cs files; final newline (.editorconfig). The Write tool emits LF/no-BOM — normalize to CRLF without a BOM after writing; verify first bytes `2f 2f`.
- File-scoped namespaces; usings inside namespace; no `this.`; explicit accessibility; braces everywhere; nullable on; warnings-as-errors.
- Library namespace stays `ktsu.ThemeProvider`. Reference the new package with `using ktsu.Semantics.Color;` — but the type `Color` collides with that namespace segment, so in files under `namespace ktsu.ThemeProvider` use `using ktsu.Semantics.Color;` (no segment clash there) and refer to `Color` directly. (Only test/adapter namespaces that themselves end in a clashing segment need an alias.)
- Breaking change → **major version bump** (commit with `[major]` tag). Regenerate the `CompatibilitySuppressions.xml` ApiCompat baseline.
- `AccessibilityLevel` enum: delete the local one; use `ktsu.Semantics.Color.AccessibilityLevel` (same members Fail/AA/AAA).
- Build a project: `dotnet build <proj>.csproj`. Build all: `dotnet build ThemeProvider.sln`.

## Repo facts

- Projects in `ThemeProvider.sln`: `ThemeProvider` (lib), `ThemeProvider.ImGui` (lib), `ThemeProviderDemo` (exe). **No test project today.**
- CPM in `Directory.Packages.props`. Current version `1.0.23` (VERSION.md is auto-managed — do NOT hand-edit; the `[major]` commit tag drives the bump).
- `ThemeProvider/CompatibilitySuppressions.xml` has an entry for `ColorMath.CreateGradient(...)` — will be stale after deletion.

---

### Task 1: Migrate the `ThemeProvider` library (coordinated type swap)

This task is atomic by necessity — deleting `PerceptualColor` and changing `ISemanticTheme` breaks every theme and the mapper at once. The deliverable is a green `dotnet build ThemeProvider/ThemeProvider.csproj`.

**Files:**
- Modify: `Directory.Packages.props`, `ThemeProvider/ThemeProvider.csproj`
- Delete: `ThemeProvider/RgbColor.cs`, `SRgbColor.cs`, `OklabColor.cs`, `PerceptualColor.cs`, `ColorMath.cs`, `AccessibilityLevel.cs`
- Modify: `ThemeProvider/ISemanticTheme.cs`, `SemanticColorMapper.cs`, `ColorRange.cs`, all `ThemeProvider/Themes/**/*.cs` (~40 files)

**Steps:**

- [ ] **Step 1: Add the dependency.** In `Directory.Packages.props` add `<PackageVersion Include="ktsu.Semantics.Color" Version="2.3.0" />`. In `ThemeProvider/ThemeProvider.csproj` add `<PackageReference Include="ktsu.Semantics.Color" />`.
- [ ] **Step 2: Delete the six in-house type files** listed above.
- [ ] **Step 3: Migrate `ISemanticTheme.cs`** — `Dictionary<SemanticMeaning, Collection<PerceptualColor>> SemanticMapping` → `Collection<Color>`; add `using ktsu.Semantics.Color;`.
- [ ] **Step 4: Migrate the ~40 theme files** (mechanical, uniform): `using ktsu.Semantics.Color;`; `PerceptualColor` → `Color`; `PerceptualColor.FromRgb("#hex")` → `Color.FromHex("#hex")`. The `SemanticMapping` dictionary literals and `Neutrals` collections are unchanged except the generic arg.
- [ ] **Step 5: Rewrite `SemanticColorMapper.cs` onto `Color`.** Replacements (preserve the algorithm exactly — only swap the color primitives):
  - `color.Lightness` → `color.ToOklab().L`. **Perf:** in `CalculateGlobalLightnessRange`, `InterpolateBetweenColors`, and any `OrderBy(c => c.Lightness)`, precompute `(Color color, double l)` pairs once (don't call `ToOklab()` inside `OrderBy`/inner loops).
  - `PerceptualColor.SemanticDistanceTo` → `Color.DistanceTo`.
  - `ColorMath.OklabToRgb(new OklabColor(targetL, a, b))` → `Color.FromOklab(new Oklab(targetL, a, b))`; hue/chroma via `Color.ToOklch()` (`.H`/`.C`) or `ToOklab()` (`.A`/`.B`).
  - The in-gamut **chroma-reduction binary search** in `ExtrapolateColorToLightness` is bespoke — keep it; test gamut by whether `Color.FromOklab(...)`'s `R/G/B` ∈ [0,1] (use `.Clamp()` only for the final emit, not the gamut test).
  - `OklabColor.Lerp` → `Color.MixOklab(other, t)`.
  - Any `ColorMath.GetContrastRatio/GetRelativeLuminance/GetAccessibilityLevel/AdjustForAccessibility` → `Color.ContrastRatio/RelativeLuminance/AccessibilityLevelAgainst/AdjustForContrast`.
  - `MapColors`/`MakeCompletePalette` return `IReadOnlyDictionary<SemanticColorRequest, Color>`.
- [ ] **Step 6: Rewrite `ColorRange.cs` onto `Color`** — `SemanticDistanceTo` → `DistanceTo`; lightness ordering via the precomputed Oklab L. Keep its ordering business logic.
- [ ] **Step 7: Build.** `dotnet build ThemeProvider/ThemeProvider.csproj` → 0 warnings / 0 errors across all TFMs.
- [ ] **Step 8: Commit** (`feat(color)!: migrate ThemeProvider core to ktsu.Semantics.Color`).

---

### Task 2: Migrate `ThemeProvider.ImGui`

**Files:** Modify `ThemeProvider.ImGui/ImGuiPaletteMapper.cs`.

- [ ] **Step 1:** `IReadOnlyDictionary<SemanticColorRequest, PerceptualColor>` → `...Color`. The per-color emit `new Vector4(color.RgbValue.R, .G, .B, 1f)` → `color.ToSrgbVector4()` (gamma-correct — this is the consumer-side half of the fix). Add `using ktsu.Semantics.Color;`.
- [ ] **Step 2:** `dotnet build ThemeProvider.ImGui/ThemeProvider.ImGui.csproj` green.
- [ ] **Step 3:** Commit.

---

### Task 3: Migrate `ThemeProviderDemo`

**Files:** Modify `ThemeProviderDemo/Program.cs`.

- [ ] **Step 1:** `PerceptualColor` → `Color`; palette dict type updated. Replace the ~10 `if (color != default)` "not found" sentinels — `default(Color)` is `(0,0,0,0)`, a valid-looking color, so the idiom is unsafe. Use `TryGetValue(...)` results / a `Color?` rather than `!= default` (look up via the dictionary's `TryGetValue` and branch on the bool).
- [ ] **Step 2:** `dotnet build ThemeProvider.sln` → whole solution green.
- [ ] **Step 3:** Commit.

---

### Task 4: Add a characterization/invariant test project

**Files:** Create `ThemeProvider.Test/ThemeProvider.Test.csproj` (MSTest.Sdk + ktsu.Sdk, net10.0), `ThemeProvider.Test/*.cs`; add to `ThemeProvider.sln` via `dotnet sln add`.

- [ ] **Step 1: Base-color round-trip invariant** — for a representative sample of theme base colors (e.g. Catppuccin Mocha, Gruvbox Dark, Nord, TokyoNight), assert `Color.FromHex(hex).ToSrgbVector4()` channels equal `originalBytes/255` within 1/255 (proves base colors render unchanged — the gamma fix is display-safe).
- [ ] **Step 2: Semantic mapper sanity** — for a couple of themes, `SemanticColorMapper.MakeCompletePalette(theme)` returns colors that are all in gamut (R/G/B ∈ [0,1]) and that requested-priority lightness ordering is monotonic.
- [ ] **Step 3: Accessibility sanity** — a known fg/bg pair yields the expected `AccessibilityLevel`; `AdjustForContrast` reaches the requested level.
- [ ] **Step 4:** `dotnet test ThemeProvider.Test/ThemeProvider.Test.csproj` → all pass.
- [ ] **Step 5:** Commit.

---

### Task 5: ApiCompat baseline, docs, version bump

**Files:** `ThemeProvider/CompatibilitySuppressions.xml`, `README.md`, `CLAUDE.md`, `DESCRIPTION.md`, `TAGS.md`.

- [ ] **Step 1:** Regenerate / clear `CompatibilitySuppressions.xml` for the major break (remove the stale `ColorMath.CreateGradient` entry; the major version bump permits the API removals). If the ktsu.Sdk ApiCompat target fails the build, follow its documented baseline-update step.
- [ ] **Step 2:** Update `README.md` (the color examples ~118-162 and the API table ~357), `CLAUDE.md` (lines ~34-37 referencing `ColorMath.cs`/`SRgbColor.cs`), `DESCRIPTION.md`, `TAGS.md` — remove deleted types, point at `ktsu.Semantics.Color`. (Do NOT hand-edit VERSION.md/CHANGELOG.md/LICENSE.md.)
- [ ] **Step 3:** Build the whole solution once more; ensure clean.
- [ ] **Step 4:** Commit with a `[major]` tag in the message (drives the version bump).

---

## Self-Review checklist
- Gamma rule applied everywhere (FromHex in / ToSrgbVector4 out); no `FromLinear(bytes)` or `ToLinearVector4()` on the ImGui path.
- All six in-house color files deleted; no dangling references; `AccessibilityLevel` now from Semantics.Color.
- Public API: `PerceptualColor` gone from every signature (ISemanticTheme, SemanticColorMapper, ImGuiPaletteMapper, theme statics).
- Mapper algorithm unchanged except primitives; hot loops cache Oklab L.
- Demo `!= default` sentinels replaced with TryGetValue/`Color?`.
- Multi-target build clean; ApiCompat baseline updated; docs updated; `[major]` commit.

## Risks
- **Mapper rewrite (Task 1, Step 5)** is the only judgment-heavy part — the gamut chroma search and lightness bucketing must be preserved verbatim, only the color primitives swapped. Review this diff closely against the original.
- **No pre-existing tests** — Task 4's invariants are the first safety net; they pin the display-stability guarantee and gamut/accessibility sanity, but cannot prove the derived-color shift is "right" (it's the intended correctness change).
