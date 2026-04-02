# DemaConsulting.SpdxModel System Design

## System Architecture

DemaConsulting.SpdxModel is a .NET library providing a complete implementation of the SPDX
(Software Package Data Exchange) data model. The library exposes an in-memory object model
representing all SPDX document elements, plus serialization and transformation capabilities.

### Major Components

- **Data Model** — C# classes representing every SPDX 2.x element (documents, packages, files,
  snippets, relationships, annotations, checksums, external references, etc.)
- **IO Subsystem** — JSON serialization and deserialization for SPDX 2.2 and 2.3 formats using
  `System.Text.Json`
- **Transform Subsystem** — Utilities for manipulating SPDX documents, starting with relationship
  management

## External Interfaces and Dependencies

### External Dependencies

- **System.Text.Json** — used by the IO subsystem for JSON reading and writing; available as part of
  the .NET BCL from .NET 6 onwards and via NuGet for .NET Standard 2.0
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
- **No external runtime dependencies beyond BCL**: keeps the library lightweight and avoids
  dependency conflicts for consumers
- **Target multi-framework**: the library targets `netstandard2.0`, `net8.0`, `net9.0`,
  and `net10.0` simultaneously

## Integration Patterns

Consumers typically:

1. Deserialize an SPDX document from a JSON file using `Spdx2JsonDeserializer`
2. Inspect or modify the `SpdxDocument` object model in memory
3. Serialize back to JSON using `Spdx2JsonSerializer`

For programmatic SBOM construction, consumers create `SpdxDocument` instances directly and
populate the data model before serializing.
