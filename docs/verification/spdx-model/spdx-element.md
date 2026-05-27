## SpdxElement

### Verification Approach

SpdxElement is verified through automated unit tests using the MSTest framework. Tests are
located in `test/DemaConsulting.SpdxModel.Tests/SpdxElementTests.cs`. Each test exercises
the element ID validation logic directly with no mocked dependencies.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxElement_Id_ValidFormat_PassesValidation**: Verifies that an element with a properly
formatted SPDX-ID (matching the SPDXRef- prefix pattern) passes validation without reporting
any issues.

**SpdxElement_Id_InvalidFormat_ReportsValidationIssue**: Verifies that an element with a
malformed SPDX-ID (missing the SPDXRef- prefix or containing invalid characters) is reported
as a validation issue.

### Methods Without Direct Test Scenarios

**EnhanceElement**: `EnhanceElement` is a `protected` method and therefore cannot be invoked
directly from test code. Its behavior is verified indirectly through the `Enhance` method tests
of concrete subclasses (`SpdxAnnotation`, `SpdxChecksum`, `SpdxPackage`, etc.), each of which
exercises the inherited `EnhanceElement` call as part of their own `Enhance` validation. No
separate `SpdxElement`-level scenario is required.
