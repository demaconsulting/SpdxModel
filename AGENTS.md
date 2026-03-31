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

## Available Specialized Agents

- **implementation** - Orchestrator agent that manages quality implementations
  through a formal RESEARCH → DEVELOPMENT → QUALITY state machine workflow
- **developer** - General-purpose software development agent that applies
  appropriate standards based on the work being performed
- **quality** - Quality assurance agent that grades developer work against DEMA
  Consulting standards and Continuous Compliance practices
- **code-review** - Agent for performing formal reviews using standardized
  review processes
- **repo-consistency** - Ensures SpdxModel remains consistent with the
  [TemplateDotNetLibrary](https://github.com/demaconsulting/TemplateDotNetLibrary) template patterns
- **requirements** - Develops requirements and ensures test coverage linkage
- **technical-writer** - Creates accurate documentation following regulatory best practices
- **test-developer** - Creates unit tests following AAA pattern

## Agent Selection Guide

- Fix a bug → **developer**
- Add a new feature (complex) → **implementation** (orchestrates research→development→quality)
- Add a new feature (simple) → **developer** → **test-developer**
- Write a test → **test-developer**
- Fix linting or static analysis issues → **quality**
- Update documentation → **technical-writer**
- Add or update requirements → **requirements**
- Ensure test coverage linkage in `requirements.yaml` → **requirements**
- Run security scanning or address CodeQL alerts → **quality**
- Propagate template changes → **repo-consistency**
- Formal code review → **code-review**

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

## Agent Log Files

Upon completion, agents should create a log file at `.agent-logs/[agent-name]-[subject]-[unique-id].md` that includes:

- A concise summary of the work performed
- Any important decisions made and their rationale
- Follow-up items, open questions, or TODOs

Agent logs are stored in `.agent-logs/` which is excluded from git (via `.gitignore`) and excluded from linting.

## Agent Report Files

When agents need to write report files to communicate with each other or the user, follow these guidelines:

- **Naming Convention**: Use the pattern `AGENT_REPORT_xxxx.md` (e.g., `AGENT_REPORT_analysis.md`,
  `AGENT_REPORT_results.md`)
- **Purpose**: These files are for temporary inter-agent communication and should not be committed
- **Exclusions**: Files matching `AGENT_REPORT_*.md` are automatically:
  - Excluded from git (via .gitignore)
  - Excluded from markdown linting
  - Excluded from spell checking
