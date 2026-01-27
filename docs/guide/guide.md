# SpdxModel Library - User Guide

A comprehensive guide to using the SpdxModel library for working with SPDX documents in C#.

## Quick Navigation

This section provides quick links to major sections when viewing this guide on GitHub. A complete table of contents
is available in the generated HTML and PDF versions.

- [Introduction](#introduction)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Core Concepts](#core-concepts)
- [Working with Documents](#working-with-documents)
- [Working with Packages](#working-with-packages)
- [Working with Files](#working-with-files)
- [Working with Relationships](#working-with-relationships)
- [Advanced Usage](#advanced-usage)
- [Best Practices](#best-practices)

## Introduction

### What is SpdxModel?

The SpdxModel library is a modern C# library designed for working with SPDX (Software Package Data Exchange) documents.
SPDX is an open standard for communicating software bill of materials (SBOM) information, including components,
licenses, copyrights, and security references.

This library provides a comprehensive in-memory model for reading, manipulating, and writing SPDX SBOM files in JSON
format.

### When to Use SpdxModel

This library is ideal for:

- **SBOM Generation**: Creating software bill of materials for your applications
- **SBOM Analysis**: Parsing and analyzing existing SPDX documents
- **License Compliance**: Tracking and managing software licenses
- **Supply Chain Security**: Managing software component relationships and dependencies
- **CI/CD Integration**: Automating SBOM creation and validation in build pipelines
- **Tool Integration**: Building tools that work with SPDX documents

### Supported SPDX Versions

- **SPDX 2.2**: Full support for SPDX 2.2 specification
- **SPDX 2.3**: Full support for SPDX 2.3 specification

### Key Features

- 🚀 **Full SPDX Support**: Complete implementation of SPDX 2.2 and 2.3 specifications
- 📦 **In-Memory Model**: Efficient object model for SPDX documents
- 🔄 **JSON Serialization**: Read and write SPDX documents in JSON format
- 🎯 **Type-Safe**: Strongly-typed C# API with nullable reference types
- 🔍 **Transform Support**: Built-in utilities for manipulating SPDX relationships
- ⚡ **Multi-Target**: Supports .NET 8, 9, and 10
- 🧪 **Well-Tested**: Comprehensive test suite with high code coverage
- 📚 **Well-Documented**: XML documentation for all public APIs

## Installation

### Prerequisites

- .NET 8.0, 9.0, or 10.0
- C# 12 or later

### Installing via NuGet

You can install the SpdxModel package via NuGet Package Manager:

```bash
dotnet add package DemaConsulting.SpdxModel
```

Or via the Package Manager Console in Visual Studio:

```powershell
Install-Package DemaConsulting.SpdxModel
```

### Verifying Installation

After installation, verify that you can import the library:

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;
```

## Quick Start

### Reading an SPDX Document

The simplest way to get started is to read an existing SPDX document:

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;

// Read SPDX document from JSON file
var json = File.ReadAllText("sbom.spdx.json");
var document = Spdx2JsonDeserializer.Deserialize(json);

// Access document properties
Console.WriteLine($"Document: {document.Name}");
Console.WriteLine($"Version: {document.SpdxVersion}");
Console.WriteLine($"Namespace: {document.DocumentNamespace}");
Console.WriteLine($"Packages: {document.Packages.Length}");
Console.WriteLine($"Files: {document.Files.Length}");
```

### Creating a Simple SPDX Document

Here's how to create a minimal SPDX document:

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;

// Create a new SPDX document
var document = new SpdxDocument
{
    Id = "SPDXRef-DOCUMENT",
    Name = "My Software",
    SpdxVersion = "SPDX-2.3",
    DocumentNamespace = "https://example.com/my-software/1.0.0",
    CreationInformation = new SpdxCreationInformation
    {
        Created = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
        Creators = ["Tool: MyTool-1.0", "Organization: Example Corp"]
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

## Core Concepts

### SPDX Elements

SPDX documents consist of several key element types:

- **Document**: The root element containing metadata and all other elements
- **Package**: Represents a software package or component
- **File**: Represents individual files within packages
- **Snippet**: Represents code snippets within files
- **Relationship**: Describes relationships between elements

### SPDX Identifiers

Every SPDX element must have a unique identifier within the document. Identifiers follow the format `SPDXRef-{name}`:

```csharp
var package = new SpdxPackage
{
    Id = "SPDXRef-MyPackage",
    Name = "MyPackage"
};
```

### Document Namespace

Every SPDX document must have a unique namespace URI that identifies the document:

```csharp
var document = new SpdxDocument
{
    DocumentNamespace = "https://example.com/my-software/1.0.0"
};
```

The namespace should be unique for each version of your software.

## Working with Documents

### Document Properties

A complete SPDX document includes:

```csharp
var document = new SpdxDocument
{
    // Required fields
    Id = "SPDXRef-DOCUMENT",
    Name = "My Software SBOM",
    SpdxVersion = "SPDX-2.3",
    DataLicense = "CC0-1.0",
    DocumentNamespace = "https://example.com/my-software/1.0.0",
    
    // Creation information
    CreationInformation = new SpdxCreationInformation
    {
        Created = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
        Creators = 
        [
            "Tool: MyTool-1.0",
            "Organization: Example Corp",
            "Person: John Doe (john@example.com)"
        ],
        LicenseListVersion = "3.21"
    },
    
    // Optional fields
    Comment = "This SBOM describes the software components",
    
    // Collections
    Packages = [],
    Files = [],
    Snippets = [],
    Relationships = []
};
```

### Document Describes Relationship

Every SPDX document should have at least one "DESCRIBES" relationship indicating what the document describes:

```csharp
document.Relationships = 
[
    new SpdxRelationship
    {
        Id = "SPDXRef-DOCUMENT",
        RelationshipType = SpdxRelationshipType.Describes,
        RelatedSpdxElement = "SPDXRef-RootPackage"
    }
];
```

## Working with Packages

### Creating Packages

A package represents a software component or library:

```csharp
var package = new SpdxPackage
{
    // Required fields
    Id = "SPDXRef-Package",
    Name = "MyPackage",
    DownloadLocation = "https://github.com/example/mypackage",
    FilesAnalyzed = false,
    
    // Version information
    Version = "1.0.0",
    
    // License information
    LicenseConcluded = "MIT",
    LicenseDeclared = "MIT",
    LicenseComments = "This package is licensed under MIT",
    
    // Copyright information
    CopyrightText = "Copyright (c) 2024 Example Corp",
    
    // Optional metadata
    Summary = "A useful package for doing things",
    Description = "This package provides functionality for...",
    Homepage = "https://example.com/mypackage",
    
    // Source information
    SourceInfo = "Built from commit abc123",
    
    // Package supplier and originator
    Supplier = "Organization: Example Corp",
    Originator = "Organization: Original Corp"
};
```

### Package with Files

When a package includes file information:

```csharp
var package = new SpdxPackage
{
    Id = "SPDXRef-Package",
    Name = "MyPackage",
    DownloadLocation = "https://github.com/example/mypackage",
    FilesAnalyzed = true,  // Set to true when analyzing files
    VerificationCode = new SpdxPackageVerificationCode
    {
        Value = "d6a770ba38583ed4bb4525bd96e50461655d2758",
        ExcludedFiles = ["./package.spdx"]
    }
};

// Add files (see next section)
document.Files = 
[
    new SpdxFile
    {
        Id = "SPDXRef-File1",
        FileName = "./src/main.cs"
    }
];

// Add relationship linking package to files
document.Relationships =
[
    new SpdxRelationship
    {
        Id = "SPDXRef-Package",
        RelationshipType = SpdxRelationshipType.Contains,
        RelatedSpdxElement = "SPDXRef-File1"
    }
];
```

### External References

Packages can include external references for security and other metadata:

```csharp
var package = new SpdxPackage
{
    Id = "SPDXRef-Package",
    Name = "MyPackage",
    ExternalReferences =
    [
        new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.Security,
            Type = "cpe23Type",
            Locator = "cpe:2.3:a:example:mypackage:1.0.0:*:*:*:*:*:*:*"
        },
        new SpdxExternalReference
        {
            Category = SpdxReferenceCategory.PackageManager,
            Type = "purl",
            Locator = "pkg:nuget/MyPackage@1.0.0"
        }
    ]
};
```

## Working with Files

### Creating Files

Files represent individual files within packages:

```csharp
var file = new SpdxFile
{
    // Required fields
    Id = "SPDXRef-File1",
    FileName = "./src/main.cs",
    
    // Checksums
    Checksums =
    [
        new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.SHA256,
            Value = "aabbccdd..."
        }
    ],
    
    // License information
    LicenseConcluded = "MIT",
    LicenseInfoInFiles = ["MIT"],
    CopyrightText = "Copyright (c) 2024 Example Corp",
    
    // File type
    FileTypes = [SpdxFileType.Source],
    
    // Optional fields
    Comment = "Main application file"
};
```

### File Checksums

Files should include checksums for integrity verification:

```csharp
var file = new SpdxFile
{
    Id = "SPDXRef-File1",
    FileName = "./lib/library.dll",
    Checksums =
    [
        new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.SHA256,
            Value = "abc123..."
        },
        new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.SHA1,
            Value = "def456..."
        }
    ]
};
```

## Working with Relationships

### Relationship Types

SPDX defines many relationship types. Common ones include:

- **DESCRIBES**: Document describes an element
- **CONTAINS**: Package contains files or other packages
- **DEPENDS_ON**: Package depends on another package
- **DEPENDENCY_OF**: Inverse of DEPENDS_ON
- **BUILD_DEPENDENCY_OF**: Build-time dependency
- **DEV_DEPENDENCY_OF**: Development dependency
- **GENERATED_FROM**: File generated from another file

### Adding Relationships

```csharp
using DemaConsulting.SpdxModel.Transform;

// Add a relationship
var relationship = new SpdxRelationship
{
    Id = "SPDXRef-Package1",
    RelationshipType = SpdxRelationshipType.DependsOn,
    RelatedSpdxElement = "SPDXRef-Package2"
};

SpdxRelationships.Add(document, relationship);
```

### Finding Related Elements

```csharp
// Get packages described by the document
var rootPackages = document.GetRootPackages();

// Get dependencies of a package
var dependencies = document.GetPackageDependencies("SPDXRef-Package1");

// Get all files in a package
var files = document.GetPackageFiles("SPDXRef-Package1");
```

## Advanced Usage

### Custom License References

For licenses not in the SPDX license list:

```csharp
var document = new SpdxDocument
{
    // ... other fields ...
    
    OtherLicensingInformation =
    [
        new SpdxLicense
        {
            Id = "LicenseRef-CustomLicense",
            ExtractedText = "Full license text here...",
            Name = "Custom License",
            SeeAlsos = ["https://example.com/license"]
        }
    ]
};

// Reference the custom license
var package = new SpdxPackage
{
    Id = "SPDXRef-Package",
    Name = "MyPackage",
    LicenseConcluded = "LicenseRef-CustomLicense"
};
```

### Document Annotations

Add annotations to provide additional information:

```csharp
var annotation = new SpdxAnnotation
{
    Annotator = "Person: John Doe",
    Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
    Type = SpdxAnnotationType.Review,
    Comment = "This package has been reviewed for security concerns"
};

document.Annotations = [annotation];
```

### Working with Snippets

Snippets represent portions of files:

```csharp
var snippet = new SpdxSnippet
{
    Id = "SPDXRef-Snippet1",
    SnippetFromFile = "SPDXRef-File1",
    Name = "Authentication Function",
    Ranges =
    [
        new SpdxRange
        {
            StartPointer = new SpdxPointer { LineNumber = 100 },
            EndPointer = new SpdxPointer { LineNumber = 150 }
        }
    ],
    LicenseConcluded = "MIT"
};

document.Snippets = [snippet];
```

## Best Practices

### Use Meaningful Identifiers

Choose descriptive SPDX identifiers:

```csharp
// Good
Id = "SPDXRef-Package-MyLibrary-1.0.0"

// Not as good
Id = "SPDXRef-Package1"
```

### Include Complete License Information

Always specify both concluded and declared licenses:

```csharp
var package = new SpdxPackage
{
    LicenseConcluded = "MIT",      // What you determined
    LicenseDeclared = "MIT"        // What the package declares
};
```

### Use Checksums for Files

Always include checksums for files when possible:

```csharp
var file = new SpdxFile
{
    FileName = "./app.exe",
    Checksums =
    [
        new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.SHA256,
            Value = "..."
        }
    ]
};
```

### Maintain Unique Document Namespaces

Each version of your software should have a unique namespace:

```csharp
// Good - includes version
DocumentNamespace = "https://example.com/myapp/1.0.0"

// Not as good - no version
DocumentNamespace = "https://example.com/myapp"
```

### Document Relationships

Explicitly document relationships between components:

```csharp
// Document what it describes
document.Relationships =
[
    new SpdxRelationship
    {
        Id = "SPDXRef-DOCUMENT",
        RelationshipType = SpdxRelationshipType.Describes,
        RelatedSpdxElement = "SPDXRef-RootPackage"
    }
];
```

### Include Creator Information

Provide complete creator information:

```csharp
CreationInformation = new SpdxCreationInformation
{
    Created = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
    Creators = 
    [
        "Tool: MyTool-1.0",
        "Organization: Example Corp",
        "Person: Build System"
    ]
};
```

### Error Handling

Always handle potential errors when deserializing:

```csharp
try
{
    var json = File.ReadAllText("sbom.spdx.json");
    var document = Spdx2JsonDeserializer.Deserialize(json);
}
catch (JsonException ex)
{
    Console.WriteLine($"Failed to parse SPDX document: {ex.Message}");
}
catch (IOException ex)
{
    Console.WriteLine($"Failed to read file: {ex.Message}");
}
```

## Troubleshooting

### Common Issues

#### Invalid SPDX Identifier Format

SPDX identifiers must start with "SPDXRef-":

```csharp
// Correct
Id = "SPDXRef-MyPackage"

// Incorrect
Id = "MyPackage"
```

#### Missing Required Fields

Ensure all required fields are populated:

```csharp
var document = new SpdxDocument
{
    Id = "SPDXRef-DOCUMENT",        // Required
    Name = "MyDoc",                 // Required
    SpdxVersion = "SPDX-2.3",       // Required
    DataLicense = "CC0-1.0",        // Required
    DocumentNamespace = "...",       // Required
    CreationInformation = new ...   // Required
};
```

## Additional Resources

- [SPDX Specification](https://spdx.dev/) - Official SPDX specification
- [API Documentation](https://github.com/demaconsulting/SpdxModel/wiki) - Detailed API reference
- [GitHub Repository](https://github.com/demaconsulting/SpdxModel) - Source code and issues
- [NuGet Package](https://www.nuget.org/packages/DemaConsulting.SpdxModel/) - Package downloads
- [spdx-tool](https://github.com/demaconsulting/spdx-tool) - Command-line tool for SPDX documents

## Support

For help and support:

- 📫 **Issues**: [GitHub Issues](https://github.com/demaconsulting/SpdxModel/issues)
- 💬 **Discussions**: [GitHub Discussions](https://github.com/demaconsulting/SpdxModel/discussions)
- 📧 **Email**: Contact DEMA Consulting for enterprise support

## License

This library is licensed under the MIT License. See the LICENSE file for details.

---

Made with ❤️ by [DEMA Consulting](https://github.com/demaconsulting)
