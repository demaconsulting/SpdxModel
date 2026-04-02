# DemaConsulting.SpdxModel Design Documentation

## Purpose

This document provides the design overview for the DemaConsulting.SpdxModel library, a .NET library
for reading, writing, and manipulating SPDX (Software Package Data Exchange) documents. It serves as
the entry point for the design documentation, providing architectural context for formal code review,
compliance auditing, and maintenance support.

## Scope

This design documentation covers the DemaConsulting.SpdxModel library, including:

- The SPDX data model (documents, packages, files, snippets, relationships, annotations, checksums, etc.)
- JSON serialization and deserialization (SPDX 2.2 and SPDX 2.3)
- Relationship manipulation utilities

Excluded from scope:

- Consumer application code using this library
- CI/CD pipeline configuration
- NuGet package distribution infrastructure

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

OTS Software Items:

- MSTest — unit test framework
- ReqStream — requirements traceability enforcement
- BuildMark — build notes documentation generation
- VersionMark — tool version documentation
- SarifMark — CodeQL SARIF report generation
- SonarMark — SonarCloud quality report generation

## Folder Layout

```text
src/DemaConsulting.SpdxModel/
├── IO/
│   ├── Spdx2JsonDeserializer.cs    — SPDX 2.x JSON deserialization
│   ├── Spdx2JsonSerializer.cs      — SPDX 2.x JSON serialization
│   └── SpdxConstants.cs            — SPDX constants
├── Transform/
│   └── SpdxRelationships.cs        — Relationship manipulation utilities
├── SpdxAnnotation.cs               — Annotation data model
├── SpdxAnnotationType.cs           — Annotation type enum
├── SpdxChecksum.cs                 — Checksum data model
├── SpdxChecksumAlgorithm.cs        — Checksum algorithm enum
├── SpdxCreationInformation.cs      — Creation information data model
├── SpdxDocument.cs                 — Document data model
├── SpdxElement.cs                  — Base element class
├── SpdxExternalDocumentReference.cs — External document reference model
├── SpdxExternalReference.cs        — External reference data model
├── SpdxExtractedLicensingInfo.cs   — Extracted licensing info model
├── SpdxFile.cs                     — File data model
├── SpdxFileType.cs                 — File type enum
├── SpdxHelpers.cs                  — Helper utilities
├── SpdxLicenseElement.cs           — License element base class
├── SpdxPackage.cs                  — Package data model
├── SpdxPackageVerificationCode.cs  — Package verification code model
├── SpdxReferenceCategory.cs        — Reference category enum
├── SpdxRelationship.cs             — Relationship data model
├── SpdxRelationshipType.cs         — Relationship type enum
└── SpdxSnippet.cs                  — Snippet data model

test/DemaConsulting.SpdxModel.Tests/
├── IO/
│   ├── Examples/                    — Test example JSON files
│   └── (Spdx2JsonDeserialize*.cs and Spdx2JsonSerialize*.cs test files)
├── Transforms/
│   └── SpdxRelationshipsTests.cs   — Relationship utility tests
├── SpdxAnnotationTests.cs
├── SpdxChecksumTests.cs
├── SpdxCreationInformationTests.cs
├── SpdxDocumentTests.cs
├── SpdxExternalDocumentReferenceTests.cs
├── SpdxExternalReferenceTests.cs
├── SpdxExtractedLicensingInfoTests.cs
├── SpdxFileTests.cs
├── SpdxPackageTests.cs
├── SpdxPackageVerificationCodeTests.cs
├── SpdxRelationshipTests.cs
└── SpdxSnippetTests.cs
```
