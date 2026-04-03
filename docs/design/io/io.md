# IO Subsystem Design

## Purpose

The IO subsystem provides JSON serialization and deserialization for SPDX 2.x documents,
converting between the in-memory `SpdxDocument` object model and SPDX JSON files conforming
to the SPDX 2.2 and 2.3 specifications.

## Units

| Unit | File | Responsibility |
| ---- | ---- | -------------- |
| `Spdx2JsonDeserializer` | `IO/Spdx2JsonDeserializer.cs` | Reads SPDX 2.x JSON into the object model |
| `Spdx2JsonSerializer` | `IO/Spdx2JsonSerializer.cs` | Writes the object model to SPDX 2.x JSON |
| `SpdxConstants` | `IO/SpdxConstants.cs` | String constants for SPDX JSON field names |

## Design

### Spdx2JsonDeserializer

`Spdx2JsonDeserializer` reads a JSON stream or string and populates a `SpdxDocument`. It uses
`System.Text.Json.Nodes`, parsing input with `JsonNode.Parse` and traversing `JsonObject` and
`JsonArray` nodes to reconstruct each element. Both SPDX 2.2 and 2.3 JSON schemas are
supported; version differences are handled transparently during parsing.

Key design decisions:

- DOM-based parsing via `JsonNode` (rather than streaming) to allow forward references between
  document elements
- Graceful handling of optional SPDX fields (missing fields result in default values)

### Spdx2JsonSerializer

`Spdx2JsonSerializer` takes an `SpdxDocument`, builds a JSON DOM using `JsonObject` and
`JsonArray`, and emits the final JSON with `ToJsonString(...)`. It iterates over each element
collection in document order, creating the appropriate JSON structure for each SPDX element
type.

Key design decisions:

- Output follows SPDX 2.3 JSON schema by default
- Optional fields are omitted when empty or null to keep output clean

### SpdxConstants

`SpdxConstants` is a static class holding string constants for every JSON property name used in
the SPDX 2.x JSON format. Using named constants prevents typos and centralizes the mapping
between the object model and the serialized form.

## Dependencies

The IO subsystem depends on:

- `System.Text.Json` (BCL / NuGet)
- All data model units in the root namespace (`SpdxDocument`, `SpdxPackage`, etc.)
