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

#if NETSTANDARD2_0

// Provide a minimal HashCode.Combine polyfill for netstandard2.0, which does not
// have System.HashCode. The implementation uses prime-based accumulation to
// produce reasonably well-distributed hash codes from multiple values, matching
// the public API surface used in this library.
namespace System;

/// <summary>
///     Polyfill for <c>System.HashCode</c> on netstandard2.0 targets.
/// </summary>
/// <remarks>
///     System.HashCode was introduced in .NET Core 2.1 / netstandard2.1 and is not
///     available in netstandard2.0. This internal struct provides the Combine overloads
///     used by this library so that no conditional compilation is needed in calling code.
/// </remarks>
internal struct HashCode
{
    /// <summary>
    ///     Combines two values into a hash code using prime-based accumulation.
    /// </summary>
    /// <typeparam name="T1">Type of the first value</typeparam>
    /// <typeparam name="T2">Type of the second value</typeparam>
    /// <param name="value1">First value</param>
    /// <param name="value2">Second value</param>
    /// <returns>Combined hash code</returns>
    public static int Combine<T1, T2>(T1 value1, T2 value2)
    {
        // Accumulate hash codes using a prime multiplier to reduce collisions
        var hash = 17;
        hash = hash * 31 + (value1?.GetHashCode() ?? 0);
        hash = hash * 31 + (value2?.GetHashCode() ?? 0);
        return hash;
    }

    /// <summary>
    ///     Combines three values into a hash code using prime-based accumulation.
    /// </summary>
    /// <typeparam name="T1">Type of the first value</typeparam>
    /// <typeparam name="T2">Type of the second value</typeparam>
    /// <typeparam name="T3">Type of the third value</typeparam>
    /// <param name="value1">First value</param>
    /// <param name="value2">Second value</param>
    /// <param name="value3">Third value</param>
    /// <returns>Combined hash code</returns>
    public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
    {
        // Accumulate hash codes using a prime multiplier to reduce collisions
        var hash = 17;
        hash = hash * 31 + (value1?.GetHashCode() ?? 0);
        hash = hash * 31 + (value2?.GetHashCode() ?? 0);
        hash = hash * 31 + (value3?.GetHashCode() ?? 0);
        return hash;
    }

    /// <summary>
    ///     Combines four values into a hash code using prime-based accumulation.
    /// </summary>
    /// <typeparam name="T1">Type of the first value</typeparam>
    /// <typeparam name="T2">Type of the second value</typeparam>
    /// <typeparam name="T3">Type of the third value</typeparam>
    /// <typeparam name="T4">Type of the fourth value</typeparam>
    /// <param name="value1">First value</param>
    /// <param name="value2">Second value</param>
    /// <param name="value3">Third value</param>
    /// <param name="value4">Fourth value</param>
    /// <returns>Combined hash code</returns>
    public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
    {
        // Accumulate hash codes using a prime multiplier to reduce collisions
        var hash = 17;
        hash = hash * 31 + (value1?.GetHashCode() ?? 0);
        hash = hash * 31 + (value2?.GetHashCode() ?? 0);
        hash = hash * 31 + (value3?.GetHashCode() ?? 0);
        hash = hash * 31 + (value4?.GetHashCode() ?? 0);
        return hash;
    }
}

#endif
