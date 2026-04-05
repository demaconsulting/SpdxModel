# DemaConsulting.SpdxModel System Design

## System Architecture

DemaConsulting.SpdxModel is a .NET library providing a complete implementation of the SPDX
(Software Package Data Exchange) data model. The library exposes an in-memory object model
representing all SPDX document elements, plus serialization and transformation capabilities.

### Subsystems

| Subsystem | Folder | Responsibility |
| --------- | ------ | -------------- |
| IO | `IO/` | JSON serialization and deserialization for SPDX 2.2 and 2.3 formats |
| Transform | `Transform/` | Utilities for manipulating SPDX documents in memory |

### Data Model Units

| Unit | File | Responsibility |
| ---- | ---- | -------------- |
| `SpdxElement` | `SpdxElement.cs` | Abstract base for all identifiable SPDX elements |
| `SpdxLicenseElement` | `SpdxLicenseElement.cs` | Abstract base for elements carrying license and copyright fields |
| `SpdxDocument` | `SpdxDocument.cs` | Root container of a complete SPDX document |
| `SpdxPackage` | `SpdxPackage.cs` | Represents a software package in the SBOM |
| `SpdxFile` | `SpdxFile.cs` | Represents an individual file in the SBOM |
| `SpdxSnippet` | `SpdxSnippet.cs` | Represents a code snippet within a file |
| `SpdxRelationship` | `SpdxRelationship.cs` | Represents a directional relationship between elements |
| `SpdxAnnotation` | `SpdxAnnotation.cs` | Represents a review or assessment annotation |
| `SpdxChecksum` | `SpdxChecksum.cs` | Represents a cryptographic checksum |
| `SpdxCreationInformation` | `SpdxCreationInformation.cs` | Metadata about document authorship and creation time |
| `SpdxExternalDocumentReference` | `SpdxExternalDocumentReference.cs` | Reference to an external SPDX document |
| `SpdxExternalReference` | `SpdxExternalReference.cs` | Reference to an external resource (registry, VDB, etc.) |
| `SpdxExtractedLicensingInfo` | `SpdxExtractedLicensingInfo.cs` | Non-standard license text extracted from the software |
| `SpdxPackageVerificationCode` | `SpdxPackageVerificationCode.cs` | Cryptographic integrity code for a package's contents |
| `SpdxHelpers` | `SpdxHelpers.cs` | Shared utility functions (date-time validation, string fitness) |

## External Interfaces and Dependencies

### External Dependencies

- **System.Text.Json** — used by the IO subsystem for JSON reading and writing; available in-box on
  modern .NET targets and via NuGet for .NET Standard 2.0
- **.NET Standard 2.0 / .NET 8 / .NET 9 / .NET 10** — target frameworks

### Public API Surface

The library exposes:

- `SpdxDocument` — root object representing a complete SPDX document
- Data model classes for all SPDX elements
- `Spdx2JsonDeserializer` — reads SPDX JSON into the object model
- `Spdx2JsonSerializer` — writes the object model to SPDX JSON
- `SpdxRelationships` — static utilities for relationship manipulation

## Data Flow

```text
JSON File
   │
   ▼
Spdx2JsonDeserializer ──► SpdxDocument (in-memory model)
                                │
                          (manipulate via
                          Transform utilities)
                                │
                                ▼
                      Spdx2JsonSerializer ──► JSON File
```

## System-Wide Design Constraints and Decisions

- **Immutability by convention**: data model classes use public mutable properties to allow
  flexible construction while deep-copy methods provide safe cloning
- **Nullable reference types enabled**: all public API members declare nullability explicitly
- **Minimal runtime dependencies**: keeps the library lightweight and avoids dependency conflicts
  for consumers by relying only on BCL/framework-provided APIs where available, with
  compatibility NuGet packages used on older targets such as `netstandard2.0`
- **Target multi-framework**: the library targets `netstandard2.0`, `net8.0`, `net9.0`,
  and `net10.0` simultaneously

## Integration Patterns

Consumers typically:

1. Deserialize an SPDX document from a JSON file using `Spdx2JsonDeserializer`
2. Inspect or modify the `SpdxDocument` object model in memory
3. Serialize back to JSON using `Spdx2JsonSerializer`

For programmatic SBOM construction, consumers create `SpdxDocument` instances directly and
populate the data model before serializing.
