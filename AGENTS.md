# AI Instructions for SpdxModel

This file provides specific context and instructions for AI coding agents to
interact effectively with this C# project.

## Project Overview

SpdxModel is a C# library for serializing and deserializing SPDX SBOMs into an
in-memory representation suitable for manipulation and analysis.

## Technologies and Dependencies

* **Language**: C# 12
* **.NET Frameworks**: .NET 8, 9, and 10
* **Primary Dependencies**: [System.Text.Json]

## Project Structure

The repository is organized as follows:

* `/.config/`: Contains the .NET Tool configuration.
* `/.github/workflows/`: Contains the CI/CD pipeline configurations.
* `/src/DemaConsulting.SpdxModel/`: Contains the library source code.
* `/test/DemaConsulting.SpdxModel.Tests/`: Contains the library unit tests.
* `/DemaConsulting.SpdxModel.sln`: The main Visual Studio solution file.

## Development Commands

Use these commands to perform common development tasks:

* **Restore DotNet Tools**:

  ```bash
  dotnet tool restore
  ```

* **Build the Project**:

  ```bash
  dotnet build
  ```

* **Run All Tests**:

  ```bash
  dotnet test
  ```

## Testing Guidelines

* Tests are located under the `/test/DemaConsulting.SpdxModel.Tests/` folder and use the MSTest framework.
* Test files should end with `.cs` and adhere to the naming convention `[Component]Tests.cs`.
* All new features should be tested with comprehensive unit tests.
* The build must pass all tests and static analysis warnings before merging.
* Tests should be written using the AAA (Arrange, Act, Assert) pattern.

## Code Style and Conventions

* Follow standard C# naming conventions (PascalCase for classes/methods/properties, camelCase for
  local variables).
* Nullable reference types are enabled at the project level (`<Nullable>enable</Nullable>` in .csproj files).
  Do not use file-level `#nullable enable` directives.
* Warnings are treated as errors (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`).
* Avoid public fields; prefer properties.

## Code Quality Tools

The project uses several code quality and analysis tools:

* **Code Analyzers**:
  * `Microsoft.CodeAnalysis.NetAnalyzers`: Provides .NET-specific code analysis including security,
    performance, and design rules
  * All warnings are treated as errors (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`)
  * Certain analyzer rules are configured to 'suggestion' or 'none' in `.editorconfig` to align with existing
    codebase patterns

* **Code Coverage**:
  * `coverlet.collector` is used for collecting code coverage
  * Coverage reports are generated in OpenCover format
  * Aim for high test coverage (80%+ for new code)

* **Static Analysis**:
  * SonarCloud integration for continuous code quality monitoring
  * CodeQL for security analysis
  * All code changes must pass static analysis checks

* **Linting and Code Quality**:
  * `cspell` for spell checking (configured in `.cspell.json`)
  * `markdownlint-cli2` for Markdown file linting (configured in `.markdownlint.json`)
  * `yamllint` for YAML file linting (configured in `.yamllint.yaml`)
  * Code quality checks run automatically in CI pipeline
  * Run locally: `npx cspell lint "**/*.md"`, `npx markdownlint-cli2 "**/*.md"`, `yamllint .`

* **Editor Configuration**:
  * `.editorconfig` defines consistent code style rules
  * Ensure your editor respects EditorConfig settings

## Code Coverage Requirements

* All new features must include comprehensive unit tests
* Bug fixes should include tests that verify the fix
* Aim for 80%+ code coverage for new code
* Run tests with coverage: `dotnet test --collect:"XPlat Code Coverage;Format=opencover"`

## Documentation Standards

* All public APIs must have XML documentation comments
* Include `<summary>`, `<param>`, `<returns>`, and `<exception>` tags as appropriate
* XML documentation is generated during build (`<GenerateDocumentationFile>True</GenerateDocumentationFile>`)
* Keep documentation clear, concise, and up-to-date with code changes

## Performance Testing

* While there are currently no formal performance benchmarks, consider performance implications for:
  * Large SPDX document parsing and serialization
  * Memory usage when working with many packages or files
  * Relationship graph traversal operations
* If adding features that may impact performance, consider adding benchmark tests using BenchmarkDotNet

## Security Guidelines

* Never introduce security vulnerabilities
* Be cautious with untrusted input (especially when deserializing SPDX documents)
* Regular expressions must have timeout limits to prevent ReDoS attacks
* Follow secure coding practices for C# and .NET
* Review the [SECURITY.md](SECURITY.md) file for security policies

## Continuous Integration

* All changes are validated by GitHub Actions CI
* CI pipeline includes:
  * Code quality checks (spell checking, Markdown linting, YAML linting)
  * Building on multiple .NET versions (8, 9, 10)
  * Running all unit tests
  * Code coverage collection
  * SonarCloud analysis
  * SBOM generation
* All CI checks must pass before merging

## Boundaries and Guardrails

* **NEVER** modify files within the `/obj/` or `/bin/` directories.
* **NEVER** commit secrets, API keys, or sensitive configuration data.
* **ASK FIRST** before making significant architectural changes to the core library logic.
