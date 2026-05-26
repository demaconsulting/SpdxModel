# Usage

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
Console.WriteLine($"Version: {document.Version}");
Console.WriteLine($"Namespace: {document.DocumentNamespace}");
Console.WriteLine($"Packages: {document.Packages.Length}");
Console.WriteLine($"Files: {document.Files.Length}");
```

### Creating a Simple SPDX Document

Here is how to create a minimal SPDX document:

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;

// Create a new SPDX document
var document = new SpdxDocument
{
    Id = "SPDXRef-DOCUMENT",
    Name = "My Software",
    Version = "SPDX-2.3",
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

Every SPDX element must have a unique identifier within the document. Identifiers follow the
format `SPDXRef-{name}`:

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
    Version = "SPDX-2.3",
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

Every SPDX document should have at least one DESCRIBES relationship indicating what the document
describes:

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

When a package includes file information, set `FilesAnalyzed` to `true` and provide a verification
code:

```csharp
var package = new SpdxPackage
{
    Id = "SPDXRef-Package",
    Name = "MyPackage",
    DownloadLocation = "https://github.com/example/mypackage",
    FilesAnalyzed = true,
    VerificationCode = new SpdxPackageVerificationCode
    {
        Value = "d6a770ba38583ed4bb4525bd96e50461655d2758",
        ExcludedFiles = ["./package.spdx"]
    }
};

// Add files and a relationship linking the package to those files
document.Files =
[
    new SpdxFile
    {
        Id = "SPDXRef-File1",
        FileName = "./src/main.cs"
    }
];

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

Packages can include external references for security and package-manager metadata:

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
            Locator = "cpe:2.3:a:example:my-package:1.0.0:*:*:*:*:*:*:*"
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
            Value = "abc123def456..."
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

Include multiple checksum algorithms for stronger integrity verification:

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
```

## Advanced Usage

### Custom License References

For licenses not in the SPDX license list, use a `LicenseRef-` identifier:

```csharp
var document = new SpdxDocument
{
    // ... other fields ...
    ExtractedLicensingInfo =
    [
        new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-CustomLicense",
            ExtractedText = "Full license text here...",
            Name = "Custom License",
            CrossReferences = ["https://example.com/license"]
        }
    ]
};

// Reference the custom license in a package
var package = new SpdxPackage
{
    Id = "SPDXRef-Package",
    Name = "MyPackage",
    LicenseConcluded = "LicenseRef-CustomLicense"
};
```

### Document Annotations

Add annotations to provide review or informational comments:

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

Snippets represent portions of files and carry their own license information:

```csharp
var snippet = new SpdxSnippet
{
    Id = "SPDXRef-Snippet1",
    SnippetFromFile = "SPDXRef-File1",
    Name = "Authentication Function",
    SnippetLineStart = 100,
    SnippetLineEnd = 150,
    LicenseConcluded = "MIT"
};

document.Snippets = [snippet];
```

### Deep Copying Elements

The library supports deep copying of any SPDX element or an entire document. This creates
a completely independent copy with no shared references to the original:

```csharp
// Deep copy an entire document
var original = Spdx2JsonDeserializer.Deserialize(json);
var copy = original.DeepCopy();

// Deep copy individual elements
var packageCopy = package.DeepCopy();
var fileCopy = file.DeepCopy();
```

Deep copying is useful when you need to modify an element without affecting the original,
such as when merging SPDX documents or creating variants.

### Comparing SPDX Elements

The library provides equality comparers for all SPDX element types. Each element class
exposes a `Same` static comparer that checks all significant fields:

```csharp
// Compare two documents for equality
bool areEqual = SpdxDocument.Same.Equals(doc1, doc2);

// Compare packages
bool samePackage = SpdxPackage.Same.Equals(pkg1, pkg2);

// Compare relationships (ignoring comment field)
bool sameRelationship = SpdxRelationship.Same.Equals(rel1, rel2);

// Compare relationships by elements only (ignoring relationship type)
bool sameElements = SpdxRelationship.SameElements.Equals(rel1, rel2);
```

These comparers are used internally by the deep-copy verification logic and by the
`SpdxRelationships.Add` deduplication logic.

### Validating SPDX Documents

Call `Validate()` on any SPDX document to get a list of validation issues. An empty list
means the document is valid:

```csharp
var issues = new List<string>();
document.Validate(issues);

if (issues.Count == 0)
{
    Console.WriteLine("Document is valid.");
}
else
{
    Console.WriteLine("Validation issues found:");
    foreach (var issue in issues)
    {
        Console.WriteLine($"  - {issue}");
    }
}
```

The recommended workflow is to validate after deserialization to catch malformed or
incomplete SPDX documents early, before processing them further.

## Best Practices

### Use Meaningful Identifiers

Choose descriptive SPDX identifiers so documents are human-readable:

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
    LicenseConcluded = "MIT",  // What you determined after analysis
    LicenseDeclared = "MIT"    // What the package itself declares
};
```

### Use Checksums for Files

Always include checksums for files when possible to support integrity verification:

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

Each version of your software should have a unique namespace to avoid conflicts:

```csharp
// Good - includes version
DocumentNamespace = "https://example.com/myapp/1.0.0"

// Not as good - no version
DocumentNamespace = "https://example.com/myapp"
```

### Include Creator Information

Provide complete creator information to support audit and traceability:

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
