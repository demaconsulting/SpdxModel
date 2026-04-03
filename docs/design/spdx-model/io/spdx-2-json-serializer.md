# Spdx2JsonSerializer Unit Design

## Purpose

`Spdx2JsonSerializer` converts an in-memory `SpdxDocument` object model to an SPDX 2.3 JSON
string. It is the counterpart to `Spdx2JsonDeserializer` and completes the round-trip
serialization support for the IO subsystem.

## Design

`Spdx2JsonSerializer` is a public static class with no instance state. All public methods
accept strongly typed model objects and return `JsonObject`/`JsonArray` nodes or a final JSON
string.

Key design decisions:

- Output conforms to SPDX 2.3 JSON schema.
- Optional fields are omitted entirely (not written as `null`) when empty or null to keep
  output concise and compatible with strict schema validators.
- Per-element `Serialize*` methods (`SerializePackage`, `SerializeFile`, etc.) are public to
  support targeted unit testing and partial serialization.

Key methods:

| Method | Description |
| ------ | ----------- |
| `Serialize(SpdxDocument)` | Entry point — returns a complete SPDX JSON string |
| `SerializeDocument(SpdxDocument)` | Converts an `SpdxDocument` to a `JsonObject` |
| `Serialize*(…)` | Per-element helpers for each SPDX element type |

## Dependencies

- `System.Text.Json` (BCL) — JSON node construction via `JsonObject`/`JsonArray`
- `SpdxDocument` and all data model units in the root namespace
- `SpdxConstants` — string constants for JSON property names
