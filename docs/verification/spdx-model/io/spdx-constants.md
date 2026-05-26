### SpdxConstants

#### Verification Approach

SpdxConstants defines string constants used by the Spdx2JsonDeserializer and
Spdx2JsonSerializer units. No dedicated unit test file exists for SpdxConstants. The
correctness of these constants is verified implicitly through the IO round-trip and unit
tests for the deserializer and serializer.

#### Test Environment

N/A - standard test environment.

#### Acceptance Criteria

All automated tests for Spdx2JsonDeserializer and Spdx2JsonSerializer pass with zero
failures.

#### Test Scenarios

N/A — SpdxConstants defines string constants verified implicitly through
`Spdx2JsonDeserializer` and `Spdx2JsonSerializer` unit tests.
