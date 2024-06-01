namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxExtractedLicensingInfoTests
{
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
        Assert.IsTrue(SpdxExtractedLicensingInfo.Same.GetHashCode(l1) == SpdxExtractedLicensingInfo.Same.GetHashCode(l2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var l1 = new SpdxExtractedLicensingInfo
        {
            LicenseId = "LicenseRef-1",
            ExtractedText = "The CyberNeko Software License",
            Comment = "Extracted from files"
        };

        var l2 = l1.DeepCopy();
        l2.ExtractedText = "Different License";

        Assert.IsFalse(ReferenceEquals(l1, l2));
        Assert.AreEqual("The CyberNeko Software License", l1.ExtractedText);
        Assert.AreEqual("Different License", l2.ExtractedText);
    }

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
            new[]
            {
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
            });

        Assert.AreEqual(2, infos.Length);
        Assert.AreEqual("LicenseRef-1", infos[0].LicenseId);
        Assert.AreEqual("The CyberNeko Software License", infos[0].ExtractedText);
        Assert.AreEqual("Extracted from files", infos[0].Comment);
        Assert.AreEqual("LicenseRef-2", infos[1].LicenseId);
        Assert.AreEqual("Some Random License", infos[1].ExtractedText);
    }
}