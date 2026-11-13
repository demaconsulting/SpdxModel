# Installation

## Prerequisites

Using SpdxModel requires:

- .NET Standard 2.0, .NET 10.0, or .NET 11.0 (older runtimes, such as .NET Framework and
  .NET 8/9, use the .NET Standard 2.0 build)
- C# 12 or later

## Installing via NuGet

Install the SpdxModel package via the .NET CLI:

```bash
dotnet add package DemaConsulting.SpdxModel
```

Or via the Package Manager Console in Visual Studio:

```powershell
Install-Package DemaConsulting.SpdxModel
```

## Verifying Installation

After installation, verify that you can reference the library namespaces:

```csharp
using DemaConsulting.SpdxModel;
using DemaConsulting.SpdxModel.IO;
```
