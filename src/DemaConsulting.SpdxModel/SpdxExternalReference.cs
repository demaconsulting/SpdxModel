﻿// Copyright(c) 2024 DEMA Consulting
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace DemaConsulting.SpdxModel;

/// <summary>
///     SPDX External Reference Field
/// </summary>
/// <remarks>
///     An External Reference allows a Package to reference an external source of
///     additional information, metadata, enumerations, asset identifiers, or
///     downloadable content believed to be relevant to the Package.
/// </remarks>
public sealed class SpdxExternalReference
{
    /// <summary>
    ///     Equality comparer for the same external reference
    /// </summary>
    /// <remarks>
    ///     This considers external references to be the same if they have the same
    ///     category, type, and locator.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxExternalReference> Same = new SpdxExternalReferenceSame();

    /// <summary>
    ///     External Reference Category Field
    /// </summary>
    /// <remarks>
    ///     Category for the external reference
    /// </remarks>
    public SpdxReferenceCategory Category { get; set; } = SpdxReferenceCategory.Missing;

    /// <summary>
    ///     External Reference Type Field
    /// </summary>
    /// <remarks>
    ///     Type of the external reference. These are defined in an appendix in
    ///     the SPDX specification.
    /// </remarks>
    public string Type { get; set; } = "";

    /// <summary>
    ///     External Reference Locator Field
    /// </summary>
    /// <remarks>
    ///     The unique string with no spaces necessary to access the
    ///     package-specific information, metadata, or content within the target
    ///     location. The format of the locator is subject to constraints defined
    ///     by the type.
    /// </remarks>
    public string Locator { get; set; } = "";

    /// <summary>
    ///     External Reference Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxExternalReference DeepCopy()
    {
        return new SpdxExternalReference
        {
            Category = Category,
            Type = Type,
            Locator = Locator,
            Comment = Comment
        };
    }

    /// <summary>
    ///     Enhance missing fields in the external reference
    /// </summary>
    /// <param name="other">Other external reference to enhance with</param>
    public void Enhance(SpdxExternalReference other)
    {
        // Populate the category if missing
        if (Category == SpdxReferenceCategory.Missing)
            Category = other.Category;

        // Populate the type field if missing
        Type = SpdxHelpers.EnhanceString(Type, other.Type) ?? "";

        // Populate the locator field if missing
        Locator = SpdxHelpers.EnhanceString(Locator, other.Locator) ?? "";

        // Populate the comment if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment);
    }

    /// <summary>
    ///     Enhance missing external references in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxExternalReference[] Enhance(SpdxExternalReference[] array, SpdxExternalReference[] others)
    {
        // Convert to list
        var list = array.ToList();

        // Iterate over other array
        foreach (var other in others)
        {
            // Check if other item is the same as one we have
            var existing = list.Find(a => Same.Equals(a, other));
            if (existing != null)
                // Enhance our item with the other information
                existing.Enhance(other);
            else
                // Add the new item to our list
                list.Add(other.DeepCopy());
        }

        // Return as array
        return [..list];
    }

    /// <summary>
    ///     Perform validation of information
    /// </summary>
    /// <param name="package">Package name</param>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(string package, List<string> issues)
    {
        // Validate External Reference Category Field
        if (Category == SpdxReferenceCategory.Missing)
            issues.Add($"Package {package} Invalid External Reference Category Field");

        // Validate External Reference Type Field
        if (Type.Length == 0)
            issues.Add($"Package {package} Invalid External Reference Type Field");

        // Validate External Reference Locator Field
        if (Locator.Length == 0)
            issues.Add($"Package {package} Invalid External Reference Locator Field");
    }

    /// <summary>
    ///     Equality Comparer to test for the same external reference
    /// </summary>
    private sealed class SpdxExternalReferenceSame : IEqualityComparer<SpdxExternalReference>
    {
        /// <inheritdoc />
        public bool Equals(SpdxExternalReference? r1, SpdxExternalReference? r2)
        {
            if (ReferenceEquals(r1, r2)) return true;
            if (r1 == null || r2 == null) return false;

            return r1.Category == r2.Category &&
                   r1.Type == r2.Type &&
                   r1.Locator == r2.Locator;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxExternalReference obj)
        {
            return HashCode.Combine(
                obj.Category,
                obj.Type,
                obj.Locator);
        }
    }
}