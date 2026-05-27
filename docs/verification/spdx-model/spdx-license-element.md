## SpdxLicenseElement

### Verification Approach

`SpdxLicenseElement` is an abstract base class; it cannot be instantiated directly. Its
logic is exercised through `SpdxPackage`, the simplest concrete subclass. All unit tests
live in `SpdxLicenseElementTests` within the `DemaConsulting.SpdxModel.Tests` project.
Subclass-level deep-copy tests (for `SpdxPackage`, `SpdxFile`, and `SpdxSnippet`) provide
additional evidence that the inherited fields are stored and copied correctly.

### Test Environment

Standard test environment — no external services, files, or special configuration required.
Tests run with `dotnet test` under the standard CI pipeline.

### Acceptance Criteria

All automated tests in `SpdxLicenseElementTests` pass with zero failures, and all
subclass tests that exercise the inherited license-element fields (deep-copy and enhance)
also pass with zero failures.

### Test Scenarios

The following scenarios are covered by `SpdxLicenseElementTests`:

- **Empty and null fields replaced by concrete values**: verifies that `ConcludedLicense`,
  `CopyrightText`, and `LicenseComments` set to empty string or null (fitness 0/1) are
  replaced when the source carries concrete (rank-3) values.

- **NOASSERTION fields replaced by concrete values**: verifies that `ConcludedLicense`,
  `CopyrightText`, and `LicenseComments` set to `NOASSERTION` (fitness 2) are replaced
  when the source carries concrete (rank-3) values.

- **Concrete fields not replaced by secondary values**: verifies that `ConcludedLicense`,
  `CopyrightText`, and `LicenseComments` already holding concrete values are not overwritten
  by any secondary value regardless of its fitness level.

- **Attribution text merged by deduplication**: verifies that unique entries from the
  secondary `AttributionText` array are appended while duplicate entries are discarded, so
  each attribution notice appears exactly once in the merged result.

- **Annotations merged by identity-match and append**: verifies that annotations matching an
  existing entry (same annotator, date, type, and comment) are recognized as duplicates and
  that annotations with no matching entry are appended as independent deep copies.
