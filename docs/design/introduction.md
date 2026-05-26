# Introduction

SpdxModel is a .NET library for working with SPDX (Software Package Data Exchange) documents.
It provides a comprehensive in-memory model for reading, manipulating, and writing SPDX Software
Bill of Materials (SBOM) files in JSON format. The library is organized into a root data model
system with IO and Transform subsystems.

## Purpose

This document defines the design for each software item in SpdxModel — full architectural and
detailed design for local items (systems, subsystems, and units). A reviewer should be able to
understand how each item satisfies its requirements without reading source code.

## Scope

Local items:

- **SpdxModel**: system, subsystem, and unit design.

Out of scope:

- Test projects
- Build pipeline
- NuGet package distribution infrastructure
- The internal design of OTS software items

## Software Structure

```text
DemaConsulting.SpdxModel (System)
├── IO (Subsystem)
│   ├── Spdx2JsonDeserializer (Unit)
│   ├── Spdx2JsonSerializer (Unit)
│   └── SpdxConstants (Unit)
├── Transform (Subsystem)
│   └── SpdxRelationships (Unit)
├── SpdxAnnotation (Unit)
├── SpdxChecksum (Unit)
├── SpdxCreationInformation (Unit)
├── SpdxDocument (Unit)
├── SpdxElement (Unit)
├── SpdxExternalDocumentReference (Unit)
├── SpdxExternalReference (Unit)
├── SpdxExtractedLicensingInfo (Unit)
├── SpdxFile (Unit)
├── SpdxHelpers (Unit)
├── SpdxLicenseElement (Unit)
├── SpdxPackage (Unit)
├── SpdxPackageVerificationCode (Unit)
├── SpdxRelationship (Unit)
└── SpdxSnippet (Unit)
```

## Folder Layout

```text
src/DemaConsulting.SpdxModel/
├── IO/
│   ├── Spdx2JsonDeserializer.cs    — SPDX 2.x JSON deserialization
│   ├── Spdx2JsonSerializer.cs      — SPDX 2.x JSON serialization
│   └── SpdxConstants.cs            — SPDX JSON field name constants
├── Transform/
│   └── SpdxRelationships.cs        — Relationship manipulation utilities
├── SpdxAnnotation.cs               — Annotation data model
├── SpdxAnnotationType.cs           — Annotation type enumeration
├── SpdxChecksum.cs                 — Checksum data model
├── SpdxChecksumAlgorithm.cs        — Checksum algorithm enumeration
├── SpdxCreationInformation.cs      — Creation information data model
├── SpdxDocument.cs                 — Root document data model
├── SpdxElement.cs                  — Abstract base element class
├── SpdxExternalDocumentReference.cs — External document reference model
├── SpdxExternalReference.cs        — External reference data model
├── SpdxExtractedLicensingInfo.cs   — Extracted licensing information model
├── SpdxFile.cs                     — File data model
├── SpdxFileType.cs                 — File type enumeration
├── SpdxHelpers.cs                  — Shared utility functions
├── SpdxLicenseElement.cs           — Abstract license element base class
├── SpdxPackage.cs                  — Package data model
├── SpdxPackageVerificationCode.cs  — Package verification code model
├── SpdxReferenceCategory.cs        — Reference category enumeration
├── SpdxRelationship.cs             — Relationship data model
├── SpdxRelationshipType.cs         — Relationship type enumeration
└── SpdxSnippet.cs                  — Snippet data model

test/DemaConsulting.SpdxModel.Tests/
├── IO/
│   ├── Examples/                    — Test example JSON files
│   ├── Spdx2JsonDeserialize*.cs     — Deserializer unit tests
│   ├── Spdx2JsonSerialize*.cs       — Serializer unit tests
│   └── SpdxModelIOTests.cs          — IO subsystem integration tests
├── Transforms/
│   ├── SpdxModelTransformTests.cs  — Transform subsystem integration tests
│   └── SpdxRelationshipsTests.cs   — Relationship utilities tests
├── SpdxModelTests.cs               — System-level integration tests
├── SpdxAnnotationTests.cs          — SpdxAnnotation unit tests
├── SpdxChecksumTests.cs            — SpdxChecksum unit tests
├── SpdxCreationInformationTests.cs — SpdxCreationInformation unit tests
├── SpdxDocumentTests.cs            — SpdxDocument unit tests
├── SpdxExternalDocumentReferenceTests.cs — SpdxExternalDocumentReference unit tests
├── SpdxExternalReferenceTests.cs   — SpdxExternalReference unit tests
├── SpdxExtractedLicensingInfoTests.cs — SpdxExtractedLicensingInfo unit tests
├── SpdxFileTests.cs                — SpdxFile unit tests
├── SpdxHelpersTests.cs             — SpdxHelpers unit tests
├── SpdxPackageTests.cs             — SpdxPackage unit tests
├── SpdxPackageVerificationCodeTests.cs — SpdxPackageVerificationCode unit tests
├── SpdxRelationshipTests.cs        — SpdxRelationship unit tests
└── SpdxSnippetTests.cs             — SpdxSnippet unit tests
```

## Companion Artifact Structure

Each local software item has corresponding artifacts in parallel directory trees:

- Requirements: `docs/reqstream/spdx-model/spdx-model.yaml`,
  `docs/reqstream/spdx-model[/{subsystem-name}...]/{item}.yaml`
- Design: `docs/design/spdx-model.md`,
  `docs/design/spdx-model[/{subsystem-name}...]/{item}.md`
- Verification: `docs/verification/spdx-model.md`,
  `docs/verification/spdx-model[/{subsystem-name}...]/{item}.md`
- Source: `src/DemaConsulting.SpdxModel[/{SubsystemName}...]/{Item}.cs`
- Tests: `test/DemaConsulting.SpdxModel.Tests[/{SubsystemName}...]/{Item}Tests.cs`

Review-sets: defined in `.reviewmark.yaml`

## References

- [REF-1] SpdxModel releases, <https://github.com/demaconsulting/SpdxModel/releases>

## Structural Deviation

The companion artifact layout described in this document places subsystem design and verification
files at the subsystem level (e.g., `docs/design/spdx-model/io/` for the IO subsystem).
In practice, subsystem design files (`transform.md`, `io.md`) and verification files are located
inside their respective subsystem subfolders rather than at the parent `spdx-model/` level.
This layout was chosen for consistency with the IO subsystem file organization conventions
adopted early in the project and is accepted as a project-wide structural deviation.
Existing file references in review-sets and traceability tooling reflect the actual folder
layout and do not require updating.
