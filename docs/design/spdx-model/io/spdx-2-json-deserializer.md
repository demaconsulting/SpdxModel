### Spdx2JsonDeserializer

#### Purpose

`Spdx2JsonDeserializer` reads SPDX 2.x JSON documents and populates the in-memory `SpdxDocument`
object model. It supports both the SPDX 2.2 and SPDX 2.3 JSON schemas, handling version
differences transparently during parsing.

#### Data Model

N/A - `Spdx2JsonDeserializer` is a public static class with no instance state.

#### Key Methods

**Deserialize**: Entry point — parses a raw JSON string into an `SpdxDocument`.

- *Parameters*: `string json` — raw SPDX 2.x JSON text.
- *Returns*: `SpdxDocument` — fully populated in-memory document.
- *Preconditions*: `json` must be valid JSON text.
- *Postconditions*: The returned `SpdxDocument` reflects all elements present in the JSON input;
  unrecognized fields are silently ignored.

**DeserializeDocument**: Converts a parsed `JsonNode` tree into an `SpdxDocument`.

- *Parameters*: `JsonNode node` — root JSON node from `JsonNode.Parse`.
- *Returns*: `SpdxDocument` — populated document.
- *Preconditions*: `node` must be a `JsonObject` representing the SPDX document root.
- *Postconditions*: All child element arrays are deserialized by the corresponding per-element
  helpers.

**Deserialize\* helpers**: Per-element deserialization methods (`DeserializePackage`,
`DeserializeFile`, `DeserializeSnippet`, `DeserializeRelationship`, `DeserializeAnnotation`,
`DeserializeChecksum`, `DeserializeExternalDocumentReference`, `DeserializeExternalReference`,
`DeserializeExtractedLicensingInfo`, `DeserializeCreationInformation`).

- *Parameters*: `JsonNode? node` — the element's JSON node, which may be null.
- *Returns*: The corresponding model type (e.g., `SpdxPackage`), or a default instance when
  `node` is null.
- *Preconditions*: none.
- *Postconditions*: Missing optional fields result in default values; no exception is thrown for
  absent properties.

**DeserializeVerificationCode**: Deserializes an optional `SpdxPackageVerificationCode`.

- *Parameters*: `JsonNode? node` — the package verification code JSON node, which may be null.
- *Returns*: `SpdxPackageVerificationCode?` — a populated instance when `node` is non-null;
  `null` when `node` is null (indicating the package verification code was absent in the JSON).
- *Preconditions*: none.
- *Postconditions*: Returns `null` when `node` is `null`; does not return a default instance.

**Find (private helper)**: Locates a named descendant node within a JSON tree, descending
through arrays automatically.

- *Algorithm*: Accepts a root `JsonNode?`, an index into the `names` path array, and the full
  `names` array. When the current node is a `JsonArray`, each element is searched recursively
  and the first non-null result is returned (enabling path resolution through SPDX `ranges`
  arrays). When the current node is an object, the next name in the path is looked up and
  recursion continues. Used by `DeserializeSnippet` to extract `startPointer` and `endPointer`
  values from the SPDX 2.x `ranges` array structure without knowing the array index in advance.

#### Error Handling

Missing or null JSON properties produce default values rather than exceptions. Input must be
valid JSON; a `JsonException` from `System.Text.Json` will propagate to the caller if the input
is malformed.

#### Dependencies

- **System.Text.Json** — JSON DOM parsing via `JsonNode`, `JsonObject`, and `JsonArray`.
- **SpdxDocument** and all data model units in the root namespace.
- **SpdxConstants** — string constants for JSON property names.

#### Callers

- External consumers of the library who call `Spdx2JsonDeserializer.Deserialize` to load SPDX
  documents.
