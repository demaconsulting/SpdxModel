# SPDX Model

![GitHub forks](https://img.shields.io/github/forks/demaconsulting/SpdxModel?style=plastic)
![GitHub Repo stars](https://img.shields.io/github/stars/demaconsulting/SpdxModel?style=plastic)
![GitHub contributors](https://img.shields.io/github/contributors/demaconsulting/SpdxModel?style=plastic)
![GitHub](https://img.shields.io/github/license/demaconsulting/SpdxModel?style=plastic)
![Build](https://github.com/demaconsulting/SpdxModel/actions/workflows/build_on_push.yaml/badge.svg)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=demaconsulting_SpdxModel&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=demaconsulting_SpdxModel)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=demaconsulting_SpdxModel&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=demaconsulting_SpdxModel)
[![NuGet](https://img.shields.io/nuget/v/DemaConsulting.SpdxModel.svg)](https://www.nuget.org/packages/DemaConsulting.SpdxModel/)

A modern C# library for working with SPDX (Software Package Data Exchange) documents. This library provides a comprehensive in-memory model for reading, manipulating, and writing SPDX Software Bill of Materials (SBOM) files.

## Features

- 🚀 **Full SPDX 2.2 and 2.3 Support** - Complete implementation of SPDX specifications
- 📦 **In-Memory Model** - Efficient object model for SPDX documents
- 🔄 **JSON Serialization** - Read and write SPDX documents in JSON format
- 🎯 **Type-Safe** - Strongly-typed C# API with nullable reference types
- 🔍 **Transform Support** - Built-in utilities for manipulating SPDX relationships
- ⚡ **Multi-Target** - Supports .NET 8, 9, and 10
- 🧪 **Well-Tested** - Comprehensive test suite with high code coverage
- 📚 **Well-Documented** - XML documentation for all public APIs

## Installation

Install the package via NuGet:

```bash
dotnet add package DemaConsulting.SpdxModel
```

Or via the Package Manager Console:

```powershell
Install-Package DemaConsulting.SpdxModel
```

## Quick Start

### Reading an SPDX Document

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;

// Read SPDX document from JSON
var json = File.ReadAllText("sbom.spdx.json");
var document = Spdx2JsonDeserializer.Deserialize(json);

// Access document properties
Console.WriteLine($"Document: {document.Name}");
Console.WriteLine($"Version: {document.SpdxVersion}");
Console.WriteLine($"Packages: {document.Packages.Length}");
```

### Creating an SPDX Document

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;

// Create a new SPDX document
var document = new SpdxDocument
{
    Id = "SPDXRef-DOCUMENT",
    Name = "My Software",
    SpdxVersion = "SPDX-2.3",
    DocumentNamespace = "https://example.com/my-software",
    CreationInformation = new SpdxCreationInformation
    {
        Created = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
        Creators = ["Tool: MyTool-1.0"]
    },
    Packages =
    [
        new SpdxPackage
        {
            Id = "SPDXRef-Package",
            Name = "MyPackage",
            Version = "1.0.0",
            DownloadLocation = "https://example.com/package",
            FilesAnalyzed = false,
            LicenseConcluded = "MIT",
            LicenseDeclared = "MIT",
            CopyrightText = "Copyright (c) 2024 Example Corp"
        }
    ]
};

// Serialize to JSON
var json = Spdx2JsonSerializer.Serialize(document);
File.WriteAllText("output.spdx.json", json);
```

### Working with Relationships

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.Transform;

// Add relationships to a document
var relationship = new SpdxRelationship
{
    Id = "SPDXRef-DOCUMENT",
    RelationshipType = SpdxRelationshipType.Describes,
    RelatedSpdxElement = "SPDXRef-Package"
};

SpdxRelationships.Add(document, relationship);

// Get root packages from a document
var rootPackages = document.GetRootPackages();
```

## API Overview

### Core Classes

- **`SpdxDocument`** - Represents an SPDX document
- **`SpdxPackage`** - Represents a software package
- **`SpdxFile`** - Represents a file
- **`SpdxSnippet`** - Represents a code snippet
- **`SpdxRelationship`** - Represents relationships between elements
- **`SpdxCreationInformation`** - Document creation metadata

### Serialization

- **`Spdx2JsonSerializer`** - Serialize SPDX documents to JSON
- **`Spdx2JsonDeserializer`** - Deserialize SPDX documents from JSON

### Transforms

- **`SpdxRelationships`** - Utilities for managing relationships

## Documentation

- [API Documentation](https://github.com/demaconsulting/SpdxModel/wiki) - Detailed API reference
- [Contributing Guide](CONTRIBUTING.md) - How to contribute to the project
- [Code of Conduct](CODE_OF_CONDUCT.md) - Community guidelines
- [Security Policy](SECURITY.md) - Security vulnerability reporting
- [AGENTS.md](AGENTS.md) - Instructions for AI coding agents

## Requirements

- .NET 8.0, 9.0, or 10.0
- C# 12 or later

## Development

### Building from Source

```bash
# Clone the repository
git clone https://github.com/demaconsulting/SpdxModel.git
cd SpdxModel

# Restore tools
dotnet tool restore

# Build
dotnet build

# Run tests
dotnet test
```

### Running Tests with Coverage

```bash
dotnet test --collect:"XPlat Code Coverage;Format=opencover"
```

## Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details on:

- Setting up your development environment
- Coding standards and guidelines
- Submitting pull requests
- Reporting bugs and requesting features

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- 📫 **Issues**: [GitHub Issues](https://github.com/demaconsulting/SpdxModel/issues)
- 💬 **Discussions**: [GitHub Discussions](https://github.com/demaconsulting/SpdxModel/discussions)

## Related Projects

- [SPDX Specification](https://spdx.dev/) - Official SPDX specification
- [spdx-tool](https://github.com/demaconsulting/spdx-tool) - Command-line tool for working with SPDX documents

## Acknowledgments

This project follows the [SPDX specification](https://spdx.dev/) maintained by the Linux Foundation.

---

Made with ❤️ by [DEMA Consulting](https://github.com/demaconsulting)
