# Introduction

This document describes how each software item in the SpdxModel library is verified.

## Purpose

This document provides the verification design for the SpdxModel library. For each local
software item — system, subsystems, and units — the document names the test scenarios that
verify the item's requirements. A reviewer can confirm coverage completeness by reading this
document without consulting test source code.

## Scope

The following items are in scope for this document:

- SpdxModel system verification
- IO subsystem and unit verification
- Transform subsystem and unit verification
- All software unit verifications within the SpdxModel system

The following items are out of scope:

- OTS item verification: xUnit v3, ReqStream, BuildMark, VersionMark, SarifMark, SonarMark,
  ReviewMark, FileAssert, Pandoc, WeasyPrint
- Test infrastructure and test helpers

## Companion Artifact Structure

The following companion artifacts are related to this verification document:

- Requirements artifacts are located in `docs/reqstream/spdx-model/`
- Software design documents are located in `docs/design/spdx-model/`
- Production source code is located in `src/DemaConsulting.SpdxModel/`
- Automated test suite is located in `test/DemaConsulting.SpdxModel.Tests/`

## Structural Deviation

The companion artifact layout described in this document places subsystem verification files at
the subsystem level (e.g., `docs/verification/spdx-model/io/` for the IO subsystem). In practice,
subsystem verification files (`transform.md`, `io.md`) and their children are located inside their
respective subsystem subfolders rather than at the parent `spdx-model/` level. This deviation
mirrors the layout adopted in the design documentation and is accepted as a project-wide structural
deviation. Existing file references in review-sets and traceability tooling reflect the actual folder
layout and do not require updating.

## References

- [REF-1] SpdxModel releases, <https://github.com/demaconsulting/SpdxModel/releases>
