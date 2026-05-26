## SpdxLicenseElement

### Purpose

`SpdxLicenseElement` is an abstract intermediate base class that adds license-related fields to
`SpdxElement`. It is the common ancestor of `SpdxPackage`, `SpdxFile`, and `SpdxSnippet`,
centralizing the concluded-license, copyright, and attribution fields to avoid duplication across
all three element types.

### Data Model

**ConcludedLicense**: `string` — License expression concluded by the SPDX document preparer
for this element.

**LicenseComments**: `string?` — Optional explanation of the concluded license choice.

**CopyrightText**: `string` — Copyright declarations text for this element.

**AttributionText**: `string[]` — Attribution notices required when redistributing or using
this element.

**Annotations**: `SpdxAnnotation[]` — Element-level annotations (comments, reviews, or other
notes attached to this element).

*Inherited from `SpdxElement`*: `Id`.

### Key Methods

**EnhanceLicenseElement**: Protected helper that populates license-related fields from another
instance when the existing value has lower fitness than the source value.

- *Parameters*: `SpdxLicenseElement other` — source element.
- *Returns*: `void`
- *Preconditions*: none.
- *Postconditions*:
  a) **String fields** (`ConcludedLicense`, `LicenseComments`, `CopyrightText`): fitness-based
     selection — concrete value > NOASSERTION > empty string > null.
  b) **AttributionText**: merged by concatenation and deduplication (union of both arrays).
  c) **Annotations**: merged by identity-match (enhance existing entries by comparer) and
     append (add new entries not already present).
  d) **Base-class delegation**: also calls `EnhanceElement(other)`, which populates the
     inherited `Id` field if absent.

#### Algorithm

The fitness ranking used for string fields is: null=0, empty string=1, NOASSERTION=2, concrete
value=3. The field with the higher fitness rank is retained. When both fields have equal fitness,
the current value is kept.

For `AttributionText` and `Annotations`, both arrays are merged: existing entries are enhanced
in-place where a match is found (by annotation identity comparer), and unmatched entries from
`other` are appended as deep copies.

### Error Handling

N/A - `SpdxLicenseElement` is abstract. Subclasses implement their own `Validate` methods.

### Dependencies

- **SpdxElement** — abstract base class providing the `Id` property.
- **SpdxAnnotation** — element-level annotations.
- **SpdxHelpers** — shared utility functions for fitness-ranked string selection.

### Callers

- **SpdxPackage** — extends `SpdxLicenseElement`.
- **SpdxFile** — extends `SpdxLicenseElement`.
- **SpdxSnippet** — extends `SpdxLicenseElement`.
