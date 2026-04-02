# Transform Subsystem Design

## Purpose

The Transform subsystem provides utilities for manipulating SPDX documents in memory, enabling
consumers to programmatically build and modify SPDX relationship graphs.

## Units

| Unit | File | Responsibility |
| ---- | ---- | -------------- |
| `SpdxRelationships` | `Transform/SpdxRelationships.cs` | Utilities for adding and managing SPDX relationships |

## Design

### SpdxRelationships

`SpdxRelationships` is a static utility class that provides helper methods for adding relationships
to an `SpdxDocument`. It ensures relationships are added without duplication and in a consistent
manner, reducing boilerplate for consumers constructing SPDX documents programmatically.

Key methods:

- `AddRelationship` — adds a single relationship to the document if it does not already exist
- `AddRelationships` — adds multiple relationships, deduplicating against existing entries

Key design decisions:

- Static class with no instance state to simplify usage
- Deduplication logic prevents malformed documents with duplicate relationship entries

## Dependencies

The Transform subsystem depends on:

- `SpdxDocument` and `SpdxRelationship` data model units
