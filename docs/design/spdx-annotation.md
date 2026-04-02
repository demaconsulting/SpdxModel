# SpdxAnnotation Unit Design

## Purpose

`SpdxAnnotation` represents an SPDX annotation — a comment or review note attached to any SPDX
element by a person, organization, or tool. Annotations support compliance workflows where
reviewers document findings about software components.

## Design

`SpdxAnnotation` is a sealed class that extends `SpdxElement` (inheriting the `Id` field).

Data members:

| Property | Type | Description |
| -------- | ---- | ----------- |
| `Annotator` | `string` | Person, organization, or tool that made the annotation |
| `Date` | `string` | ISO 8601 UTC timestamp of the annotation |
| `Type` | `SpdxAnnotationType` | Enumerated annotation type (Review, Other) |
| `Comment` | `string` | Free-text annotation content |

Key methods:

- `DeepCopy()` — returns a new `SpdxAnnotation` with all fields copied
- `Enhance(SpdxAnnotation)` — fills in missing fields from another instance
- `Validate(List<string>)` — appends validation issues to the supplied list
- `Same` — static `IEqualityComparer<SpdxAnnotation>` comparing annotator, date, type, and comment

## Dependencies

- `SpdxElement` (base class)
- `SpdxAnnotationType` (enum)
