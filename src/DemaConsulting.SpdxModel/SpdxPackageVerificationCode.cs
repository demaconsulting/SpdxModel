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
///     SPDX Package Verification Code
/// </summary>
/// <remarks>
///     A manifest based verification code (the algorithm is defined in section 4.7
///     of the full specification) of the SPDX Item. This allows consumers of this
///     data and/or database to determine if an SPDX item they have in hand is
///     identical to the SPDX item from which the data was produced. This
///     algorithm works even if the SPDX document is included in the SPDX item.
/// </remarks>
public sealed class SpdxPackageVerificationCode
{
    /// <summary>
    ///     Equality comparer for the same package verification code
    /// </summary>
    /// <remarks>
    ///     This considers package verification codes as being the same if they have the same
    ///     value.
    /// </remarks>
    public static readonly IEqualityComparer<SpdxPackageVerificationCode> Same = new SpdxPackageVerificationCodeSame();

    /// <summary>
    ///     Excluded Files Field
    /// </summary>
    /// <remarks>
    ///     Files that were excluded when calculating the package verification code.
    ///     This is usually a file containing SPDX data regarding the package.
    ///     If a package contains more than one SPDX file all SPDX files must be
    ///     excluded from the package verification code. If this is not done it
    ///     would be impossible to correctly calculate the verification codes in
    ///     both files.
    /// </remarks>
    public string[] ExcludedFiles { get; set; } = [];

    /// <summary>
    ///     Verification Code Value Field
    /// </summary>
    /// <remarks>
    ///     The actual package verification code as a hex encoded value.
    /// </remarks>
    public string Value { get; set; } = "";

    /// <summary>
    ///     Make a deep-copy of this object
    /// </summary>
    /// <remarks>
    ///     Both <see cref="Value" /> and <see cref="ExcludedFiles" /> are fully copied, so the caller is free to mutate
    ///     the result without affecting the original.
    /// </remarks>
    /// <returns>Deep copy of this object</returns>
    public SpdxPackageVerificationCode DeepCopy()
    {
        return new SpdxPackageVerificationCode
        {
            ExcludedFiles = (string[])ExcludedFiles.Clone(),
            Value = Value
        };
    }

    /// <summary>
    ///     Enhance missing fields in the verification code
    /// </summary>
    /// <remarks>
    ///     <see cref="ExcludedFiles" /> entries from <paramref name="other" /> are merged by deduplication.
    ///     <see cref="Value" /> is updated only if the current value is null or empty.
    /// </remarks>
    /// <param name="other">Other verification code to enhance with</param>
    public void Enhance(SpdxPackageVerificationCode other)
    {
        // Merge the excluded files
        ExcludedFiles = [.. ExcludedFiles.Concat(other.ExcludedFiles).Distinct()];

        // Populate the value field if missing
        Value = SpdxHelpers.EnhanceString(Value, other.Value) ?? "";
    }

    /// <summary>
    ///     Perform validation of information
    /// </summary>
    /// <remarks>
    ///     Validation checks that <see cref="Value" /> is exactly 40 hex characters (a SHA1 hex digest). No format check
    ///     is performed on <see cref="ExcludedFiles" /> entries.
    /// </remarks>
    /// <param name="package">Associated package</param>
    /// <param name="issues">List to populate with issues</param>
    public void Validate(string package, List<string> issues)
    {
        // Validate Package Verification Code Value Field
        if (Value.Length != 40 || !System.Text.RegularExpressions.Regex.IsMatch(Value, "^[0-9a-fA-F]{40}$"))
        {
            issues.Add($"Package '{package}' Invalid Package Verification Code Value '{Value}'");
        }
    }

    /// <summary>Equality comparer that considers two package verification codes the same when their Value fields are identical.</summary>
    /// <remarks>
    ///     A dedicated nested class is used rather than an ad-hoc lambda so the comparer instance can be stored in the
    ///     <see cref="SpdxPackageVerificationCode.Same" /> field and passed to LINQ operations without boxing or allocation.
    /// </remarks>
    private sealed class SpdxPackageVerificationCodeSame : IEqualityComparer<SpdxPackageVerificationCode>
    {
        /// <inheritdoc />
        public bool Equals(SpdxPackageVerificationCode? v1, SpdxPackageVerificationCode? v2)
        {
            if (ReferenceEquals(v1, v2))
            {
                return true;
            }

            if (v1 == null || v2 == null)
            {
                return false;
            }

            return v1.Value == v2.Value;
        }

        /// <inheritdoc />
        public int GetHashCode(SpdxPackageVerificationCode obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
