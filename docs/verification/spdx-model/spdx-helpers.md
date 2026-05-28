## SpdxHelpers

### Verification Approach

SpdxHelpers is verified through automated unit tests using the xUnit v3 framework.
Tests are located in
`test/DemaConsulting.SpdxModel.Tests/SpdxHelpersTests.cs`. Each test exercises the helper
method directly with no mocked dependencies. Additional coverage is provided implicitly
through the unit tests of dependent classes.

### Test Environment

N/A - standard test environment.

### Acceptance Criteria

All automated tests pass with zero failures.

### Test Scenarios

**SpdxHelpers_IsValidSpdxDateTime_NullInput_ReturnsTrue**: Verifies that `IsValidSpdxDateTime`
returns `true` when passed a null value, since null represents a not-set field which is valid.

**SpdxHelpers_IsValidSpdxDateTime_EmptyInput_ReturnsTrue**: Verifies that `IsValidSpdxDateTime`
returns `true` when passed an empty string, since an empty string represents a not-set field
which is valid.

**SpdxHelpers_IsValidSpdxDateTime_ValidFormat_ReturnsTrue**: Verifies that `IsValidSpdxDateTime`
returns `true` when passed a correctly formatted ISO 8601 UTC timestamp such as
`"2024-01-01T00:00:00Z"`.

**SpdxHelpers_IsValidSpdxDateTime_InvalidFormat_ReturnsFalse**: Verifies that
`IsValidSpdxDateTime` returns `false` when passed a string that does not match the ISO 8601
UTC format.

**SpdxHelpers_EnhanceString_ConcretePreferredOverNoAssertion_ReturnsConcreteValue**: Verifies
that `EnhanceString` returns the concrete value when given a mix of a concrete value and
`NOASSERTION`.

**SpdxHelpers_EnhanceString_NullInputs_ReturnsNull**: Verifies that `EnhanceString` returns
`null` when all inputs are null.
