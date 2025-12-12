# Contributing to SpdxModel

Thank you for your interest in contributing to SpdxModel! This document provides guidelines and instructions for contributing to this project.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [How to Contribute](#how-to-contribute)
- [Coding Guidelines](#coding-guidelines)
- [Testing Requirements](#testing-requirements)
- [Pull Request Process](#pull-request-process)
- [Reporting Bugs](#reporting-bugs)
- [Suggesting Features](#suggesting-features)

## Code of Conduct

This project adheres to a Code of Conduct (see CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code. Please report unacceptable behavior to the project maintainers.

## Getting Started

1. Fork the repository on GitHub
2. Clone your fork locally
3. Create a branch for your changes
4. Make your changes
5. Push your changes to your fork
6. Submit a pull request

## Development Setup

### Prerequisites

- .NET 8, 9, or 10 SDK
- A code editor (Visual Studio, Visual Studio Code, or JetBrains Rider recommended)
- Git

### Building the Project

```bash
# Restore .NET tools
dotnet tool restore

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with code coverage
dotnet test --collect:"XPlat Code Coverage;Format=opencover"
```

## How to Contribute

### Types of Contributions

We welcome various types of contributions:

- **Bug fixes**: Help us fix issues in the codebase
- **New features**: Add new functionality to the library
- **Documentation**: Improve or add documentation
- **Tests**: Add or improve test coverage
- **Code quality**: Refactor code, improve performance, or enhance maintainability

### Before You Start

1. Check the [issue tracker](https://github.com/demaconsulting/SpdxModel/issues) to see if your issue or feature has already been reported
2. For major changes, please open an issue first to discuss what you would like to change
3. Ensure your development environment is properly set up

## Coding Guidelines

### Code Style

- Follow standard C# naming conventions (PascalCase for classes/methods/properties, camelCase for local variables)
- Use meaningful variable and method names
- Keep methods focused and concise
- Add XML documentation comments for public APIs
- The project uses `.editorconfig` for consistent code style - ensure your editor respects these settings

### C# Conventions

- Use nullable reference types appropriately
- Prefer `var` when the type is obvious from the right-hand side
- Use `file-scoped` namespaces
- Avoid public fields; use properties instead
- Follow the existing code style in the project

### Documentation

- All public APIs must have XML documentation comments
- Include `<summary>`, `<param>`, `<returns>`, and `<exception>` tags as appropriate
- Provide clear and concise descriptions
- Include code examples for complex APIs when helpful

### Analyzers and Code Quality

The project uses several code analyzers:

- **Microsoft.CodeAnalysis.NetAnalyzers**: For .NET-specific code analysis
- **StyleCop.Analyzers**: For consistent code style enforcement
- **Nullable Reference Types**: Enabled project-wide

Ensure your code passes all analyzer checks before submitting a pull request.

## Testing Requirements

### Test Coverage

- All new features must include comprehensive unit tests
- Bug fixes should include tests that verify the fix
- Aim for high test coverage (80%+ for new code)
- Use the AAA (Arrange, Act, Assert) pattern for tests

### Test Structure

```csharp
[TestMethod]
public void MethodName_Scenario_ExpectedBehavior()
{
    // Arrange: Set up test data and dependencies
    var sut = new SystemUnderTest();
    
    // Act: Execute the method being tested
    var result = sut.MethodName();
    
    // Assert: Verify the expected behavior
    Assert.AreEqual(expected, result);
}
```

### Test Naming

- Test files should be named `[Component]Tests.cs`
- Test methods should follow the pattern: `MethodName_Scenario_ExpectedBehavior`
- Use descriptive test names that clearly indicate what is being tested

## Pull Request Process

1. **Create a Branch**: Create a feature branch from `main`
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make Changes**: Implement your changes following the coding guidelines

3. **Write Tests**: Add or update tests to cover your changes

4. **Run Tests**: Ensure all tests pass
   ```bash
   dotnet test
   ```

5. **Build Verification**: Ensure the project builds without errors or warnings
   ```bash
   dotnet build
   ```

6. **Commit Changes**: Use clear and descriptive commit messages
   ```bash
   git commit -m "Add feature: brief description"
   ```

7. **Push Changes**: Push your branch to your fork
   ```bash
   git push origin feature/your-feature-name
   ```

8. **Open Pull Request**: Submit a pull request to the main repository
   - Provide a clear description of the changes
   - Reference any related issues
   - Ensure all CI checks pass

### Pull Request Requirements

- All tests must pass
- Code coverage should not decrease
- No build warnings or errors
- Code must pass all analyzer checks
- Documentation must be updated if APIs change
- Commit messages should be clear and descriptive

### Code Review Process

- Maintainers will review your pull request
- Address any feedback or requested changes
- Once approved, a maintainer will merge your pull request

## Reporting Bugs

### Before Submitting a Bug Report

- Check the [issue tracker](https://github.com/demaconsulting/SpdxModel/issues) to see if the bug has already been reported
- Ensure you're using the latest version of the library
- Verify that the issue is reproducible

### How to Submit a Bug Report

1. Use the GitHub issue tracker
2. Provide a clear and descriptive title
3. Describe the expected behavior
4. Describe the actual behavior
5. Provide steps to reproduce the issue
6. Include relevant code samples or test cases
7. Specify your environment (OS, .NET version, library version)

## Suggesting Features

We welcome feature suggestions! To suggest a new feature:

1. Check the [issue tracker](https://github.com/demaconsulting/SpdxModel/issues) to see if the feature has already been suggested
2. Open a new issue with the "enhancement" label
3. Provide a clear description of the feature
4. Explain the use case and benefits
5. Consider providing a proposed implementation approach

## License

By contributing to SpdxModel, you agree that your contributions will be licensed under the MIT License.

## Questions?

If you have questions about contributing, feel free to:

- Open an issue with your question
- Contact the project maintainers

Thank you for contributing to SpdxModel!
