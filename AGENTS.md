# Agent Quick Reference

Project-specific guidance for agents working on SpdxModel - a C# library for serializing
and deserializing SPDX SBOMs into an in-memory representation.

## Standards Application (ALL Agents Must Follow)

Before performing any work, agents must read and apply the relevant standards from `.github/standards/`:

- **`csharp-language.md`** - For C# code development (literate programming, XML docs, dependency injection)
- **`csharp-testing.md`** - For C# test development (AAA pattern, naming, MSTest anti-patterns)
- **`reqstream-usage.md`** - For requirements management (traceability, semantic IDs, source filters)
- **`reviewmark-usage.md`** - For file review management (review-sets, file patterns, enforcement)
- **`software-items.md`** - For software categorization (system/subsystem/unit/OTS classification)
- **`technical-documentation.md`** - For documentation creation and maintenance (structure, Pandoc, README best practices)

Load only the standards relevant to your specific task scope and apply their
quality checks and guidelines throughout your work.

## Agent Delegation Guidelines

The default agent should handle simple, straightforward tasks directly.
Delegate to specialized agents only for specific scenarios:

- **Light development work** (small fixes, simple features) → Call @developer agent
- **Light quality checking** (linting, basic validation) → Call @quality agent
- **Formal feature implementation** (complex, multi-step) → Call the `@implementation` agent
- **Formal bug resolution** (complex debugging, systematic fixes) → Call the `@implementation` agent
- **Formal reviews** (compliance verification, detailed analysis) → Call @code-review agent
- **Template consistency** (downstream repository alignment) → Call @repo-consistency agent

## Available Specialized Agents

- **code-review** - Agent for performing formal reviews using standardized
  review processes
- **developer** - General-purpose software development agent that applies
  appropriate standards based on the work being performed
- **implementation** - Orchestrator agent that manages quality implementations
  through a formal RESEARCH → DEVELOPMENT → QUALITY state machine workflow
- **quality** - Quality assurance agent that grades developer work against DEMA
  Consulting standards and Continuous Compliance practices
- **repo-consistency** - Ensures SpdxModel remains consistent with the
  [TemplateDotNetLibrary][template] template patterns and best practices

## Quality Gate Enforcement (ALL Agents Must Verify)

Configuration files and scripts are self-documenting with their design intent and
modification policies in header comments.

1. **Linting Standards**: `./lint.sh` (Unix) or `lint.bat` (Windows) - comprehensive linting suite
2. **Build Quality**: Zero warnings (`TreatWarningsAsErrors=true`)
3. **Static Analysis**: SonarCloud/CodeQL passing with no blockers
4. **Requirements Traceability**: `dotnet reqstream --enforce` passing
5. **Test Coverage**: All requirements linked to passing tests
6. **Documentation Currency**: All docs current and generated

## Tech Stack

- C# 12, .NET 8.0/9.0/10.0, dotnet CLI, NuGet

## Key Files

- **`requirements.yaml`** - All requirements with test linkage (enforced via `dotnet reqstream --enforce`)
- **`.editorconfig`** - Code style (file-scoped namespaces, 4-space indent, UTF-8, LF endings)
- **`.cspell.json`, `.markdownlint-cli2.jsonc`, `.yamllint.yaml`** - Linting configs

## Requirements

- All requirements MUST be linked to tests
- Not all tests need to be linked to requirements (tests may exist for corner cases, design testing,
  failure-testing, etc.)
- Enforced in CI: `dotnet reqstream --requirements requirements.yaml --tests "artifacts/**/*.trx" --enforce`
- When adding features: add requirement + link to test

## Test Source Filters

Test links in `requirements.yaml` can include a source filter prefix to restrict which test results count as
evidence. This is critical for platform and framework requirements - **do not remove these filters**.

- `windows@TestName` - proves the test passed on a Windows platform
- `ubuntu@TestName` - proves the test passed on a Linux (Ubuntu) platform
- `macos@TestName` - proves the test passed on a macOS platform
- `net8.0@TestName` - proves the test passed under the .NET 8 runtime
- `net9.0@TestName` - proves the test passed under the .NET 9 runtime
- `net10.0@TestName` - proves the test passed under the .NET 10 runtime

Without the source filter, a test result from any platform/framework satisfies the requirement. Adding the filter
ensures the CI evidence comes specifically from the required environment.

## Testing

- **Test Naming**: `ClassName_MethodUnderTest_Scenario_ExpectedBehavior` for unit tests
- **Test Framework**: Uses MSTest for unit testing
- **Code Coverage**: Maintain high code coverage for library APIs

## Code Style

- **XML Docs**: On ALL members (public/internal/private) with spaces after `///` in summaries
- **Namespace**: File-scoped namespaces only
- **Using Statements**: Top of file only (no nested using declarations except for IDisposable)
- **String Formatting**: Use interpolated strings ($"") for clarity

## Project Structure

- **`src/DemaConsulting.SpdxModel/`**: Library source code
- **`test/DemaConsulting.SpdxModel.Tests/`**: Unit tests
- **`DemaConsulting.SpdxModel.sln`**: Visual Studio solution file

## Build and Test

```bash
# Build the project
dotnet build --configuration Release

# Run unit tests
dotnet test --configuration Release
```

## Documentation

- **User Guide**: `docs/guide/`
- **Requirements**: `requirements.yaml` -> auto-generated docs
- **Build Notes**: Auto-generated via BuildMark
- **Trace Matrix**: Auto-generated via ReqStream

## Markdown Link Style

- **AI agent markdown files** (`.github/agents/*.md`): Use inline links `[text](url)` so URLs are
  visible in agent context
- **README.md**: Use absolute URLs (shipped in NuGet package)
- **All other markdown files**: Use reference-style links `[text][ref]` with `[ref]: url` at document end

## CI/CD

- **Quality Checks**: Markdown lint, spell check, YAML lint
- **Build**: Multi-platform (Windows/Linux/macOS) .NET 8/9/10
- **CodeQL**: Security scanning
- **SonarCloud**: Code quality analysis

## Common Tasks

```bash
# Format code
dotnet format

# Run all linters
./lint.sh    # Linux/macOS
lint.bat     # Windows
```

## Agent Report Files

Upon completion, create a report file at `.agent-logs/[agent-name]-[subject]-[unique-id].md` that includes:

- A concise summary of the work performed
- Any important decisions made and their rationale
- Follow-up items, open questions, or TODOs

Store agent logs in the `.agent-logs/` folder so they are ignored via `.gitignore` and excluded from linting and commits.

[template]: https://github.com/demaconsulting/TemplateDotNetLibrary
