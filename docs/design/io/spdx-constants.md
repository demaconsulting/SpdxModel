# SpdxConstants Unit Design

## Purpose

`SpdxConstants` is a static class that centralizes all JSON property-name strings used when
serializing and deserializing SPDX 2.x JSON documents. It eliminates hard-coded string literals
scattered throughout the IO subsystem and provides a single place to update field names if the
specification changes.

## Design

`SpdxConstants` is a non-instantiable static class containing only `public const string` fields.
Each constant corresponds to one JSON property name in the SPDX 2.x JSON schema (e.g.,
`FieldSpdxId`, `FieldName`, `FieldVersionInfo`).

Key design decisions:

- All constants are `const string` to allow use as switch-case labels and compile-time
  embedding.
- No logic or state — purely a name registry.

## Dependencies

- None (no external dependencies; consumed by `Spdx2JsonDeserializer` and `Spdx2JsonSerializer`)
