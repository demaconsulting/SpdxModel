# SpdxElement Unit Design

## Purpose

`SpdxElement` is the abstract base class for all identifiable SPDX elements. It defines the
common `Id` property (`SPDXRef-…`) and the shared `EnhanceElement` helper, ensuring consistent
identity handling across all element types.

## Design

`SpdxElement` is a public abstract class. Concrete element types (`SpdxDocument`, `SpdxPackage`,
`SpdxFile`, `SpdxSnippet`, `SpdxRelationship`, `SpdxAnnotation`) inherit from it.

Data members:

| Member | Type | Description |
| ------ | ---- | ----------- |
| `Id` | `string` | SPDX element identifier in `SPDXRef-<name>` format |
| `NoAssertion` | `const string` | The sentinel value `"NOASSERTION"` used by optional fields |
| `SpdxRefRegex` | `protected static Regex` | Validates `SPDXRef-…` format |

Key methods:

- `EnhanceElement(SpdxElement)` — protected helper that populates `Id` from another element if currently empty

## Dependencies

- `System.Text.RegularExpressions` — `SpdxRefRegex` for ID validation
- `SpdxHelpers` — `EnhanceString` utility used in `EnhanceElement`
