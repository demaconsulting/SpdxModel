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

- OTS item verification: MSTest, ReqStream, BuildMark, VersionMark, SarifMark, SonarMark,
  ReviewMark, FileAssert, Pandoc, WeasyPrint
- Test infrastructure and test helpers

## Companion Artifact Structure

The following companion artifacts are related to this verification document:

- Requirements artifacts are located in `docs/reqstream/spdx-model/`
- Software design documents are located in `docs/design/spdx-model/`
- Production source code is located in `src/DemaConsulting.SpdxModel/`
- Automated test suite is located in `test/DemaConsulting.SpdxModel.Tests/`

## References

- [REF-1] SpdxModel releases, <https://github.com/demaconsulting/SpdxModel/releases>
