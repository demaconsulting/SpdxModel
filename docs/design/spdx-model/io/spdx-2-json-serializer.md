### Spdx2JsonSerializer

#### Purpose

`Spdx2JsonSerializer` converts an in-memory `SpdxDocument` object model to an SPDX 2.3 JSON
string. It is the counterpart to `Spdx2JsonDeserializer` and completes the round-trip
serialization support for the IO subsystem.

#### Data Model

N/A - `Spdx2JsonSerializer` is a public static class with no instance state.

#### Key Methods

**Serialize**: Entry point — returns a complete SPDX 2.3 JSON string.

- *Parameters*: `SpdxDocument document` — the in-memory document to serialize.
- *Returns*: `string` — SPDX 2.3 JSON text.
- *Preconditions*: none.
- *Postconditions*: The returned string is valid JSON conforming to the SPDX 2.3 schema; optional
  fields absent from the model are omitted from the output.

**SerializeDocument**: Converts an `SpdxDocument` to a `JsonObject`.

- *Parameters*: `SpdxDocument document` — document to serialize.
- *Returns*: `JsonObject` — root JSON object with all element arrays populated.
- *Preconditions*: none.
- *Postconditions*: All element arrays are serialized by the corresponding per-element helpers.

**Serialize\* helpers**: Per-element serialization methods (`SerializePackage`, `SerializeFile`,
`SerializeSnippet`, `SerializeRelationship`, `SerializeAnnotation`, `SerializeChecksum`,
`SerializeExternalDocumentReference`, `SerializeExternalReference`,
`SerializeExtractedLicensingInfo`, `SerializeCreationInformation`,
`SerializeVerificationCode`).

- *Parameters*: The corresponding model object.
- *Returns*: A `JsonObject` or `JsonArray` representing the element.
- *Preconditions*: none.
- *Postconditions*: Optional fields that are null or empty are omitted from the output object.
  Exception: the top-level `files`, `packages`, `snippets`, and `relationships` arrays are
  required by the SPDX 2.x schema and are always emitted even when empty — they are not
  optional at the document level.

**Serialize\*s array helpers**: Per-element-array serialization methods (`SerializePackages`,
`SerializeFiles`, `SerializeSnippets`, `SerializeRelationships`, `SerializeAnnotations`,
`SerializeChecksums`, `SerializeExternalDocumentReferences`, `SerializeExternalReferences`,
`SerializeExtractedLicensingInfos`).

- *Parameters*: A typed array of the corresponding model objects (e.g., `SpdxPackage[]`).
- *Returns*: A `JsonArray` containing one serialized `JsonObject` per element.
- *Pattern*: Each method creates an empty `JsonArray`, iterates the input array calling
  the corresponding singular helper, and returns the populated array.

#### Error Handling

No exceptions are thrown for valid model objects. Null or empty optional fields are silently
omitted rather than written as null JSON values.

Notable conditional serialization behaviors:

- `SerializeAnnotation`: The `SPDXID` field is conditionally omitted when the annotation's
  `Id` is null or empty (annotations on sub-elements often do not carry their own SPDX ID).
- `SerializeSnippet`: A line-range entry is only added to the `ranges` array when both
  `SnippetLineStart` and `SnippetLineEnd` are non-zero; otherwise only the byte-range entry
  is written.

#### Dependencies

- **System.Text.Json** — JSON node construction via `JsonObject` and `JsonArray`.
- **SpdxDocument** and all data model units in the root namespace.
- **SpdxConstants** — string constants for JSON property names.

#### Callers

- External consumers of the library who call `Spdx2JsonSerializer.Serialize` to produce SPDX
  JSON output.
