#!/usr/bin/env bash
# Build and test SpdxModel

set -e  # Exit on error

echo "🔧 Building SpdxModel..."
dotnet build --configuration Release

echo "🧪 Running unit tests..."
dotnet test --configuration Release

echo "✨ Build and tests completed successfully!"
