### IO

The IO subsystem provides JSON serialization and deserialization for SPDX 2.x documents,
converting between the in-memory `SpdxDocument` object model and SPDX JSON files conforming
to the SPDX 2.2 and 2.3 specifications.

#### Overview

The IO subsystem is responsible for all JSON I/O for SPDX 2.x documents. It contains three
units: `Spdx2JsonDeserializer`, which reads JSON text into a `SpdxDocument`; `Spdx2JsonSerializer`,
which writes a `SpdxDocument` back to JSON text; and `SpdxConstants`, which holds the JSON field
name strings used by both the deserializer and serializer.

#### Interfaces

**Spdx2JsonDeserializer.Deserialize**: Reads an SPDX 2.x JSON string into an `SpdxDocument`.

- *Type*: In-process .NET public API
- *Role*: Provider
- *Contract*: Accepts a raw JSON string; returns a fully populated `SpdxDocument`.
- *Constraints*: Input must be valid JSON; unsupported or unknown fields are silently ignored.

**Spdx2JsonSerializer.Serialize**: Writes an `SpdxDocument` to an SPDX 2.3 JSON string.

- *Type*: In-process .NET public API
- *Role*: Provider
- *Contract*: Accepts an `SpdxDocument`; returns a complete SPDX 2.3 JSON string.
- *Constraints*: Optional fields are omitted when null or empty.

**Error Handling**:

- `Spdx2JsonDeserializer.Deserialize` throws `System.Text.Json.JsonException` when the input is
  fatally malformed JSON that cannot be parsed by `JsonNode.Parse`. Missing or unknown SPDX fields
  do not throw; the corresponding properties in the returned `SpdxDocument` receive their default
  values (empty strings for required fields, null for optional fields).
- `Spdx2JsonSerializer.Serialize` does not throw under normal operation. It silently omits null
  or empty optional fields from the output JSON.

#### Design

Deserialization uses `System.Text.Json.Nodes` DOM-based parsing so that all elements are
available before cross-references are resolved:

1. Caller invokes `Spdx2JsonDeserializer.Deserialize(jsonString)`.
2. `JsonNode.Parse` produces a `JsonObject` root.
3. `DeserializeDocument` traverses the root object, calling per-element helpers
   (`DeserializePackage`, `DeserializeFile`, etc.) for each child array.
4. `SpdxConstants` supplies the JSON property name strings to avoid hard-coded literals.
5. A populated `SpdxDocument` is returned to the caller.

Serialization follows the reverse path:

1. Caller invokes `Spdx2JsonSerializer.Serialize(document)`.
2. `SerializeDocument` builds a `JsonObject` root.
3. Per-element helpers (`SerializePackage`, `SerializeFile`, etc.) append child objects.
4. `SpdxConstants` supplies property names.
5. `ToJsonString(...)` produces the final JSON string.

Element-level field preservation (for example, verifying that all package fields survive
a round trip) is the responsibility of the unit-level IO tests, not the subsystem-level
integration tests. The subsystem-level tests verify that a complete document survives a
round trip and passes validation.
