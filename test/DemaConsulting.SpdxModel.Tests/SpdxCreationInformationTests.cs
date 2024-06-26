﻿namespace DemaConsulting.SpdxModel.Tests;

[TestClass]
public class SpdxCreationInformationTests
{
    [TestMethod]
    public void DeepCopy()
    {
        var c1 = new SpdxCreationInformation
        {
            Creators = new [] { "Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()", "Person: Jane Doe ()" },
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form.",
            LicenseListVersion = "3.9"
        };

        var c2 = c1.DeepCopy();
        c2.Creators[2] = "Person: Malcolm Nixon";

        Assert.IsFalse(ReferenceEquals(c1, c2));
        Assert.AreEqual("Person: Jane Doe ()", c1.Creators[2]);
        Assert.AreEqual("Person: Malcolm Nixon", c2.Creators[2]);
    }

    [TestMethod]
    public void Enhance()
    {
        var info = new SpdxCreationInformation
        {
            Creators = new[] { "Tool: LicenseFind-1.0", "Organization: ExampleCodeInspect ()" },
            Created = "2010-01-29T18:30:22Z",
            Comment = "This package has been shipped in source and binary form."
        };

        info.Enhance(
            new SpdxCreationInformation
            {
                Creators = new[] { "Person: Jane Doe ()" },
                LicenseListVersion = "3.9"
            });

        Assert.AreEqual(3, info.Creators.Length);
        Assert.AreEqual("Tool: LicenseFind-1.0", info.Creators[0]);
        Assert.AreEqual("Organization: ExampleCodeInspect ()", info.Creators[1]);
        Assert.AreEqual("Person: Jane Doe ()", info.Creators[2]);
        Assert.AreEqual("2010-01-29T18:30:22Z", info.Created);
        Assert.AreEqual("This package has been shipped in source and binary form.", info.Comment);
        Assert.AreEqual("3.9", info.LicenseListVersion);
    }
}