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

using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

internal static partial class SpdxHelpers
{
#if NET7_0_OR_GREATER
    /// <summary>
    ///     Regular expression for checking date/time formats (source-generated for .NET 7+)
    /// </summary>
    /// <remarks>
    ///     The source-generated variant is used on .NET 7+ because it is AOT-safe: the compiler
    ///     emits a fully static, allocation-free regex with no runtime compilation step, which is
    ///     required for Native AOT deployments. Stateless and thread-safe.
    /// </remarks>
    [GeneratedRegex(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$", RegexOptions.None, 100)]
    private static partial Regex DateTimeRegex();
#else
    /// <summary>
    ///     Cached regular expression instance for checking date/time formats (pre-.NET 7 fallback)
    /// </summary>
    /// <remarks>
    ///     The instance is initialized once at type load and shared across all calls. Thread-safe
    ///     because <see cref="Regex"/> instances are immutable after construction.
    /// </remarks>
    private static readonly Regex DateTimeRegexInstance =
        new Regex(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$", RegexOptions.None, TimeSpan.FromMilliseconds(100));

    /// <summary>
    ///     Regular expression for checking date/time formats
    /// </summary>
    /// <remarks>
    ///     Pre-.NET 7 wrapper that returns the cached <see cref="DateTimeRegexInstance"/> to
    ///     provide the same call site as the source-generated variant above.
    /// </remarks>
    /// <returns>Compiled <see cref="Regex"/> instance</returns>
    private static Regex DateTimeRegex() => DateTimeRegexInstance;
#endif

    /// <summary>
    ///     Test if a string is a valid SPDX date/time field (which include null/empty)
    /// </summary>
    /// <remarks>
    ///     Null and empty strings are treated as valid because SPDX date/time fields are optional;
    ///     a null or empty value means "not set", which is permitted by the specification. Non-empty
    ///     values are validated against the ISO 8601 UTC format (<c>yyyy-MM-ddTHH:mm:ssZ</c>) using
    ///     a regular expression. Stateless and thread-safe.
    /// </remarks>
    /// <param name="value">The timestamp string to validate. Null and empty strings are treated as valid (not-set).</param>
    /// <returns>
    ///     Returns <c>true</c> if <paramref name="value"/> matches the ISO 8601 UTC format, or if
    ///     <paramref name="value"/> is null or empty (both treated as not-set and therefore valid);
    ///     <c>false</c> otherwise.
    /// </returns>
    internal static bool IsValidSpdxDateTime(string? value)
    {
        return string.IsNullOrEmpty(value) || DateTimeRegex().IsMatch(value);
    }

    /// <summary>
    ///     This method picks the best string.
    /// </summary>
    /// <param name="values">String values to pick from</param>
    /// <returns>Best string</returns>
    /// <remarks>
    ///     Fitness ranking: null=0, empty string=1, NOASSERTION=2, any other concrete value=3.
    ///     Returns the candidate with the highest fitness. When all candidates are null (or the
    ///     array is empty), returns null.
    /// </remarks>
    internal static string? EnhanceString(params string?[] values)
    {
        // Return the value with the highest fitness
        return values
            .Select(value => (
                Value: value,
                Fitness: value switch
                {
                    null => 0,
                    "" => 1,
                    SpdxElement.NoAssertion => 2,
                    _ => 3
                }))
            .OrderByDescending(x => x.Fitness)
            .FirstOrDefault().Value;
    }
}
