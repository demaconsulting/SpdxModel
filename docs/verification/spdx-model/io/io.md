### IO

#### Verification Approach

The IO subsystem is verified through automated integration tests using the MSTest framework.
Tests are located in `test/DemaConsulting.SpdxModel.Tests/IO/SpdxModelIOTests.cs`. Integration
tests verify the IO subsystem end-to-end using real JSON input and output.
System.Text.Json is not mocked as JSON parsing is part of the verification scope.
Round-trip tests serialize and then deserialize documents to confirm fidelity.
Element-level field preservation is verified by the unit-level IO tests (Spdx2JsonDeserializer
and Spdx2JsonSerializer test classes). The subsystem-level tests verify end-to-end document
fidelity only.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All integration tests pass with zero failures.

#### Test Scenarios

**SpdxModelIO_ReadWriteSpdxJson_Spdx22Document_RoundTripProducesValidDocument**: Verifies
that an SPDX 2.2 document read from JSON and then written back to JSON and re-read produces
a document that passes all validation checks.

**SpdxModelIO_ReadWriteSpdxJson_Spdx23Document_RoundTripProducesValidDocument**: Verifies
that an SPDX 2.3 document read from JSON and then written back to JSON and re-read produces
a document that passes all validation checks.

**SpdxModelIO_ReadSpdxJson_InvalidJson_ThrowsJsonException**: Verifies that passing malformed
JSON to `Spdx2JsonDeserializer.Deserialize` throws a `JsonException`, confirming that the
subsystem correctly propagates fatal parsing errors to callers.
