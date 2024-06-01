namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxPackageTests
{
    [TestMethod]
    public void PackageSameComparer()
    {
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };

        var p2 = new SpdxPackage
        {
            Id = "SPDXRef-Package-SpdxModel",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };

        var p3 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "SomePackage",
            Version = "1.2.3"
        };

        // Assert packages compare to themselves
        Assert.IsTrue(SpdxPackage.Same.Equals(p1, p1));
        Assert.IsTrue(SpdxPackage.Same.Equals(p2, p2));
        Assert.IsTrue(SpdxPackage.Same.Equals(p3, p3));

        // Assert packages compare correctly
        Assert.IsTrue(SpdxPackage.Same.Equals(p1, p2));
        Assert.IsTrue(SpdxPackage.Same.Equals(p2, p1));
        Assert.IsFalse(SpdxPackage.Same.Equals(p1, p3));
        Assert.IsFalse(SpdxPackage.Same.Equals(p3, p1));
        Assert.IsFalse(SpdxPackage.Same.Equals(p2, p3));
        Assert.IsFalse(SpdxPackage.Same.Equals(p3, p2));

        // Assert same packages have identical hashes
        Assert.IsTrue(SpdxPackage.Same.GetHashCode(p1) == SpdxPackage.Same.GetHashCode(p2));
    }

    [TestMethod]
    public void DeepCopy()
    {
        var p1 = new SpdxPackage
        {
            Id = "SPDXRef-Package1",
            Name = "DemaConsulting.SpdxModel",
            Version = "0.0.0"
        };

        var p2 = p1.DeepCopy();
        p2.Id = "SPDXRef-Package2";
        
        Assert.IsFalse(ReferenceEquals(p1, p2));
        Assert.AreEqual("SPDXRef-Package1", p1.Id);
        Assert.AreEqual("SPDXRef-Package2", p2.Id);
    }

    [TestMethod]
    public void Enhance()
    {
        var packages = new[]
        {
            new SpdxPackage
            {
                Id = "SPDXRef-Package1",
                Name = "DemaConsulting.SpdxModel",
                Version = "0.0.0"
            }
        };

        packages = SpdxPackage.Enhance(
            packages,
            new[]
            {
                new SpdxPackage
                {
                    Id = "SPDXRef-Package-SpdxModel",
                    Name = "DemaConsulting.SpdxModel",
                    Version = "0.0.0"
                },
                new SpdxPackage
                {
                    Id = "SPDXRef-Package1",
                    Name = "SomePackage",
                    Version = "1.2.3"
                }
            });

        Assert.AreEqual(2, packages.Length);
        Assert.AreEqual("SPDXRef-Package1", packages[0].Id);
        Assert.AreEqual("DemaConsulting.SpdxModel", packages[0].Name);
        Assert.AreEqual("0.0.0", packages[0].Version);
        Assert.AreEqual("SPDXRef-Package1", packages[1].Id);
        Assert.AreEqual("SomePackage", packages[1].Name);
        Assert.AreEqual("1.2.3", packages[1].Version);
    }
}