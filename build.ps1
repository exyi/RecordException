#!/usr/bin/pwsh

cd RecordHacker.Fody/
rm -rf bin obj
dotnet build
cd ../RecordException
rm -rf bin obj
dotnet build
cd ../tests
rm -rf bin obj
dotnet test

cd ../RecordException
dotnet pack -c Release
