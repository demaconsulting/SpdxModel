# Troubleshooting

## Common Issues

### Invalid SPDX Identifier Format

SPDX identifiers must start with `SPDXRef-`. Using any other prefix causes validation errors:

```csharp
// Correct
Id = "SPDXRef-MyPackage"

// Incorrect
Id = "MyPackage"
```

### Missing Required Fields

Ensure all required fields are populated before serializing a document:

```csharp
var document = new SpdxDocument
{
    Id = "SPDXRef-DOCUMENT",       // Required
    Name = "MyDoc",                // Required
    Version = "SPDX-2.3",          // Required
    DataLicense = "CC0-1.0",       // Required
    DocumentNamespace = "...",     // Required
    CreationInformation = new ...  // Required
};
```

### Error Handling During Deserialization

Always handle potential errors when reading SPDX documents from untrusted sources:

```csharp
using System.IO;
using System.Text.Json;
using DemaConsulting.SpdxModel.IO;

try
{
    var json = File.ReadAllText("sbom.spdx.json");
    var document = Spdx2JsonDeserializer.Deserialize(json);
}
catch (JsonException ex)
{
    Console.WriteLine($"Failed to parse SPDX document: {ex.Message}");
}
catch (IOException ex)
{
    Console.WriteLine($"Failed to read file: {ex.Message}");
}
```

## Additional Resources

- [SPDX Specification](https://spdx.dev/) — Official SPDX specification
- [SpdxModel NuGet Package](https://www.nuget.org/packages/DemaConsulting.SpdxModel/) — Package downloads
- [spdx-tool](https://github.com/demaconsulting/spdx-tool) — Command-line tool for SPDX documents
- [GitHub Issues](https://github.com/demaconsulting/SpdxModel/issues) — Bug reports and feature requests
- [GitHub Discussions](https://github.com/demaconsulting/SpdxModel/discussions) — Community questions and support
