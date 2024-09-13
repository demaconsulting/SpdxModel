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

namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxChecksumTests
{
    [TestMethod]
    public void ChecksumSameComparer()
    {
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c2 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c3 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Md5,
            Value = "624c1abb3664f4b35547e7c73864ad24"
        };

        // Assert checksums compare to themselves
        Assert.IsTrue(SpdxChecksum.Same.Equals(c1, c1));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c2, c2));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c3, c3));

        // Assert checksums compare correctly
        Assert.IsTrue(SpdxChecksum.Same.Equals(c1, c2));
        Assert.IsTrue(SpdxChecksum.Same.Equals(c2, c1));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c1, c3));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c3, c1));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c2, c3));
        Assert.IsFalse(SpdxChecksum.Same.Equals(c3, c2));

        // Assert same checksums have identical hashes
        Assert.IsTrue(SpdxChecksum.Same.GetHashCode(c1) == SpdxChecksum.Same.GetHashCode(c2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var c1 = new SpdxChecksum
        {
            Algorithm = SpdxChecksumAlgorithm.Sha1,
            Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
        };

        var c2 = c1.DeepCopy();
        c2.Value = "d6a770ba38583ed4bb4525bd96e50461655d2759";

        Assert.IsFalse(ReferenceEquals(c1, c2));
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", c1.Value);
        Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2759", c2.Value);
    }

    [TestMethod]
    public void Enhance()
    {
        var checksums = new[]
        {
            new SpdxChecksum
            {
                Algorithm = SpdxChecksumAlgorithm.Sha1,
                Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
            }
        };

        checksums = SpdxChecksum.Enhance(
            checksums,
            [
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Sha1,
                    Value = "c2b4e1c67a2d28fced849ee1bb76e7391b93f125"
                },
                new SpdxChecksum
                {
                    Algorithm = SpdxChecksumAlgorithm.Md5,
                    Value = "624c1abb3664f4b35547e7c73864ad24"
                }
            ]);

        Assert.AreEqual(2, checksums.Length);
        Assert.AreEqual(SpdxChecksumAlgorithm.Sha1, checksums[0].Algorithm);
        Assert.AreEqual("c2b4e1c67a2d28fced849ee1bb76e7391b93f125", checksums[0].Value);
        Assert.AreEqual(SpdxChecksumAlgorithm.Md5, checksums[1].Algorithm);
        Assert.AreEqual("624c1abb3664f4b35547e7c73864ad24", checksums[1].Value);
    }
}