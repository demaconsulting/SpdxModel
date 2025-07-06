// Copyright(c) 2024 DEMA Consulting
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
///     SPDX File Information Element
/// </summary>
public sealed class SpdxFile : SpdxLicenseElement
{
    /// <summary>
    ///     Equality comparer for the same file
    /// </summary>
    /// <remarks>
    ///     This considers files as being the same if they have the same file name
    ///     and there is no differing SHA1 digest.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxFile> Same = new SpdxFileSame();

    /// <summary>
    ///     File Name Field
    /// </summary>
    /// <remarks>
    ///     The name of the file relative to the root of the package.
    /// </remarks>
    public string FileName { get; set; } = "";

    /// <summary>
    ///     File Types Field
    /// </summary>
    /// <remarks>
    ///     The type of the file.
    /// </remarks>
    public SpdxFileType[] FileTypes { get; set; } = [];

    /// <summary>
    ///     File Checksums
    /// </summary>
    /// <remarks>
    ///     The checksum property provides a mechanism that can be used to verify
    ///     that the contents of a file have not changed.
    /// </remarks>
    public SpdxChecksum[] Checksums { get; set; } = [];

    /// <summary>
    ///     License Information In File Field
    /// </summary>
    /// <remarks>
    ///     License expressions. See SPDX Annex D for the license expression syntax.
    ///     Licensing information that was discovered directly in the subject file.
    ///     This is also considered a declared license for the file.
    ///     If not present for a file, it implies an equivalent meaning to NOASSERTION.
    /// </remarks>
    public string[] LicenseInfoInFiles { get; set; } = [];

    /// <summary>
    ///     File Comment Field (optional)
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     File Notice Field (optional)
    /// </summary>
    /// <remarks>
    ///     This field provides a place for the SPDX file creator to record
    ///     potential legal notices found in the file. This may or may not
    ///     include copyright statements.
    /// </remarks>
    public string? Notice { get; set; }

    /// <summary>
    ///     File Contributors Field
    /// </summary>
    /// <remarks>
    ///     This field provides a place for the SPDX file creator to record file
    ///     contributors. Contributors could include names of copyright holders
    ///     and/or authors who may not be copyright holders yet contributed to the
    ///     file content.
    /// </remarks>
    public string[] Contributors { get; set; } = [];

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <returns>Deep copy of this object</returns>
    public SpdxFile DeepCopy()
    {
        return new SpdxFile
        {
            Id = Id,
            FileName = FileName,
            FileTypes = [..FileTypes],
            Checksums = [..Checksums.Select(c => c.DeepCopy())],
            ConcludedLicense = ConcludedLicense,
            LicenseInfoInFiles = (string[])LicenseInfoInFiles.Clone(),
            LicenseComments = LicenseComments,
            CopyrightText = CopyrightText,
            Comment = Comment,
            Notice = Notice,
            Contributors = (string[])Contributors.Clone(),
            AttributionText = (string[])AttributionText.Clone(),
            Annotations = [..Annotations.Select(a => a.DeepCopy())]
        };
    }

    /// <summary>
    ///     Enhance missing fields in the file
    /// </summary>
    /// <param name="other">Other file to enhance with</param>
    public void Enhance(SpdxFile other)
    {
        // Enhance the license information
        EnhanceLicenseElement(other);

        // Populate the file name if missing
        FileName = SpdxHelpers.EnhanceString(FileName, other.FileName) ?? "";

        // Merge the file types
        FileTypes = [..FileTypes.Concat(other.FileTypes).Distinct()];

        // Enhance the checksums
        Checksums = SpdxChecksum.Enhance(Checksums, other.Checksums);

        // Merge the license info in files
        LicenseInfoInFiles = [..LicenseInfoInFiles.Concat(other.LicenseInfoInFiles).Distinct()];

        // Populate the comment if missing
        Comment = SpdxHelpers.EnhanceString(Comment, other.Comment);

        // Populate the notice if missing
        Notice = SpdxHelpers.EnhanceString(Notice, other.Notice);

        // Merge the contributors
        Contributors = [..Contributors.Concat(other.Contributors).Distinct()];
    }

    /// <summary>
    ///     Enhance missing files in array
    /// </summary>
    /// <param name="array">Array to enhance</param>
    /// <param name="others">Other array to enhance with</param>
    /// <returns>Updated array</returns>
    public static SpdxFile[] Enhance(SpdxFile[] array, SpdxFile[] others)
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
    /// <param name="issues">List to populate with issues</param>
    public void Validate(List<string> issues)
    {
        // Validate File Name Field
        if (!FileName.StartsWith("./"))
            issues.Add($"File {FileName} Invalid File Name Field");

        // Validate File SPDX Identifier Field
        if (!SpdxRefRegex.IsMatch(Id))
            issues.Add($"File {FileName} Invalid SPDX Identifier Field");

        // Validate Checksums
        if (!Array.Exists(Checksums, c => c.Algorithm == SpdxChecksumAlgorithm.Sha1))
            issues.Add($"File {FileName} Invalid Checksum Field (missing SHA1)");
        foreach (var checksum in Checksums)
            checksum.Validate(FileName, issues);

        // Validate Annotations
        foreach (var annotation in Annotations)
            annotation.Validate($"File {FileName}", issues);
    }

    /// <summary>
    ///     Equality Comparer to test for the same file
    /// </summary>
    private sealed class SpdxFileSame : IEqualityComparer<SpdxFile>
    {
        /// <inheritdoc />
        public bool Equals(SpdxFile? f1, SpdxFile? f2)
        {
            if (ReferenceEquals(f1, f2)) return true;
            if (f1 == null || f2 == null) return false;

            // Reject equality if they have differing sha1 checksums
            var c1 = Array.Find(f1.Checksums, c => c.Algorithm == SpdxChecksumAlgorithm.Sha1);
            var c2 = Array.Find(f2.Checksums, c => c.Algorithm == SpdxChecksumAlgorithm.Sha1);
            if (c1 != null && c2 != null && c1.Value != c2.Value)
                return false;

            return f1.FileName == f2.FileName;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxFile obj)
        {
            return obj.FileName.GetHashCode();
        }
    }
}