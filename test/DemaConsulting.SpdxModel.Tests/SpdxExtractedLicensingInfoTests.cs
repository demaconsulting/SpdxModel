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

/// <summary>
/// Tests for the <see cref="SpdxExtractedLicensingInfo"/> class.
/// </summary>
[TestClass]
public class SpdxExtractedLicensingInfoTests
{
    /// <summary>
    /// Tests the <see cref="SpdxExtractedLicensingInfo.Same"/> comparer.
    /// </summary>
    [TestMethod]
    public void ExtractedLicensingInfoSameComparer()
    {
        var l1 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License"
        };

        var l2 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License",
            Comment = "Extracted from files"
        };

        var l3 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-2",
            ExtractedText = "Some Random License"
        };

        // Assert extracted-licensing-infos compare to themselves
        Assert.IsTrue(SpdxExtractedLicensingInfo.Same.Equals(l1, l1));
        Assert.IsTrue(SpdxExtractedLicensingInfo.Same.Equals(l2, l2));
        Assert.IsTrue(SpdxExtractedLicensingInfo.Same.Equals(l3, l3));

        // Assert extracted-licensing-infos compare correctly
        Assert.IsTrue(SpdxExtractedLicensingInfo.Same.Equals(l1, l2));
        Assert.IsTrue(SpdxExtractedLicensingInfo.Same.Equals(l2, l1));
        Assert.IsFalse(SpdxExtractedLicensingInfo.Same.Equals(l1, l3));
        Assert.IsFalse(SpdxExtractedLicensingInfo.Same.Equals(l3, l1));
        Assert.IsFalse(SpdxExtractedLicensingInfo.Same.Equals(l2, l3));
        Assert.IsFalse(SpdxExtractedLicensingInfo.Same.Equals(l3, l2));

        // Assert same extracted-licensing-infos have identical hashes
        Assert.AreEqual(SpdxExtractedLicensingInfo.Same.GetHashCode(l1), SpdxExtractedLicensingInfo.Same.GetHashCode(l2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxExtractedLicensingInfo.DeepCopy"/> method.
    /// </summary>
    [TestMethod]
    public void DeepCopy()
    {
        var l1 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License",
            Comment = "Extracted from files"
        };

        // Make deep copy
        var l2 = l1.DeepCopy();

        // Assert both objects are equal
        Assert.AreEqual(l1, l2, SpdxExtractedLicensingInfo.Same);
        Assert.AreEqual(l1.LicenseId, l2.LicenseId);
        Assert.AreEqual(l1.ExtractedText, l2.ExtractedText);
        Assert.AreEqual(l1.Comment, l2.Comment);

        // Assert separate instances
        Assert.IsFalse(ReferenceEquals(l1, l2));
    }

    /// <summary>
    /// Tests the <see cref="SpdxExtractedLicensingInfo.Enhance(SpdxExtractedLicensingInfo[], SpdxExtractedLicensingInfo[])"/> method.
    /// </summary>
    [TestMethod]
    public void Enhance()
    {
        var infos = new[]
        {
            new SpdxExtractedLicensingInfo
            {
                LicenseId = "LicenseRef-1",
                ExtractedText = "The CyberNeko Software License"
            }
        };

        infos = SpdxExtractedLicensingInfo.Enhance(
            infos,
            [
                new SpdxExtractedLicensingInfo
                {
                    LicenseId = "LicenseRef-1",
                    ExtractedText = "The CyberNeko Software License",
                    Comment = "Extracted from files"
                },
                new SpdxExtractedLicensingInfo
                {
                    LicenseId = "LicenseRef-2",
                    ExtractedText = "Some Random License"
                }
            ]);

        Assert.AreEqual(2, infos.Length);
        Assert.AreEqual("LicenseRef-1", infos[0].LicenseId);
        Assert.AreEqual("The CyberNeko Software License", infos[0].ExtractedText);
        Assert.AreEqual("Extracted from files", infos[0].Comment);
        Assert.AreEqual("LicenseRef-2", infos[1].LicenseId);
        Assert.AreEqual("Some Random License", infos[1].ExtractedText);
    }
}