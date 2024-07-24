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

using System.Text.RegularExpressions;

namespace DemaConsulting.SpdxModel;

internal static class SpdxHelpers
{
    /// <summary>
    /// Regular expression for checking date/time formats
    /// </summary>
    private static readonly Regex DateTimeRegex = new(
        @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z",
        RegexOptions.None,
        TimeSpan.FromMilliseconds(100));

    /// <summary>
    /// Test if a string is a valid SPDX date/time field (which include null/empty)
    /// </summary>
    /// <param name="value">String value</param>
    /// <returns>True if valid</returns>
    internal static bool IsValidSpdxDateTime(string? value)
    {
        return string.IsNullOrEmpty(value) || DateTimeRegex.IsMatch(value);
    }
}