# Introduction

This guide describes how to install, configure, and use SpdxModel.

## Purpose

SpdxModel is a modern C# library for working with SPDX (Software Package Data Exchange) documents.
SPDX is an open standard for communicating software bill of materials (SBOM) information, including
components, licenses, copyrights, and security references.

The library provides a comprehensive in-memory model for reading, manipulating, and writing SPDX SBOM
files in JSON format, with full support for SPDX 2.2 and 2.3 specifications. It targets .NET Standard
2.0, .NET 8, 9, and 10, and builds and runs on Windows, Linux, and macOS. It is suitable for use in
.NET applications, tools, and CI/CD pipelines.

## Scope

This guide covers installation of the SpdxModel NuGet package, basic and advanced usage of the library
API, and troubleshooting common issues. It includes usage examples for reading and writing SPDX documents,
working with packages, files, snippets, relationships, and custom license references.

Prerequisites for using SpdxModel:

- .NET Standard 2.0, .NET 8.0, 9.0, or 10.0
- C# 12 or later

## References

- [REF-1] SpdxModel releases, <https://github.com/demaconsulting/SpdxModel/releases>
- [REF-2] SPDX Specification, <https://spdx.dev/>
