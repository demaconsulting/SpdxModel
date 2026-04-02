# Spdx2JsonDeserializer Unit Design

## Purpose

`Spdx2JsonDeserializer` reads SPDX 2.x JSON documents and populates the in-memory `SpdxDocument`
object model. It supports both the SPDX 2.2 and SPDX 2.3 JSON schemas, handling version
differences transparently during parsing.

## Design

`Spdx2JsonDeserializer` is a public static class with no instance state. All public entry points
accept either a JSON string or a `JsonNode` and return strongly typed model objects.

Key design decisions:

- DOM-based parsing via `System.Text.Json.Nodes` (`JsonNode`/`JsonArray`) to allow forward
  references between document elements before the full document is assembled.
- Graceful handling of optional SPDX fields: missing properties result in default values rather
  than exceptions.
- Per-element `Deserialize*` methods (`DeserializePackage`, `DeserializeFile`, etc.) are public
  to support targeted unit testing and partial deserialization.

Key methods:

| Method | Description |
| ------ | ----------- |
| `Deserialize(string)` | Entry point — parses a raw JSON string into an `SpdxDocument` |
| `DeserializeDocument(JsonNode)` | Converts a parsed `JsonNode` tree into an `SpdxDocument` |
| `Deserialize*(JsonNode?)` | Per-element helpers for each SPDX element type |

## Dependencies

- `System.Text.Json` (BCL) — JSON DOM parsing via `JsonNode`
- `SpdxDocument` and all data model units in the root namespace
- `SpdxConstants` — string constants for JSON property names
