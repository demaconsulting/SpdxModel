### SpdxConstants

#### Purpose

`SpdxConstants` is a static class that centralizes all JSON property-name strings used when
serializing and deserializing SPDX 2.x JSON documents. It eliminates hard-coded string literals
throughout the IO subsystem and provides a single place to update field names if the SPDX
specification changes.

#### Data Model

N/A - `SpdxConstants` contains only `internal const string` fields and no instance state.
Representative constants include `FieldSpdxId` (`"SPDXID"`), `FieldName` (`"name"`),
`FieldVersionInfo` (`"versionInfo"`), `FieldPackages` (`"packages"`),
`FieldRelationships` (`"relationships"`), `FieldAnnotationType` (`"annotationType"`) and over
sixty other property-name constants covering all SPDX 2.x JSON fields.

#### Key Methods

N/A - `SpdxConstants` contains no methods; it is a pure name registry of `const string` values.

#### Error Handling

N/A - no logic is executed; all values are compile-time constants.

#### Dependencies

N/A - no external dependencies; consumed by `Spdx2JsonDeserializer` and `Spdx2JsonSerializer`.

#### Callers

- **Spdx2JsonDeserializer** — uses constants as JSON property name keys when reading elements.
- **Spdx2JsonSerializer** — uses constants as JSON property name keys when writing elements.
