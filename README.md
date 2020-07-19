# Name Analyzer

[![Version](https://img.shields.io/nuget/v/NameAnalyzer)](https://www.nuget.org/packages/NameAnalyzer)
[![License](https://img.shields.io/github/license/jaqra/NameAnalyzer)](https://raw.githubusercontent.com/jaqra/NameAnalyzer/master/LICENSE)

Namespace based type name matching to given pattern analyzer

![Sample](https://raw.githubusercontent.com/jaqra/NameAnalyzer/master/assets/sample.jpg)

## Getting Started

For example project click [here](https://github.com/jaqra/NameAnalyzer/tree/master/samples/NugetReference)

- Add package: `dotnet add package NameAnalyzer`
- Create a `.nameanalyzer` file near to `.csproj` file
- Add _semicolon separated_ rules to `.nameanalyzer` file
  - First path: Namespace regex
  - Second path: Type name regex
- Add following to `.csproj` file inside `<Project></Project>` node

```xml
  <ItemGroup>
    <AdditionalFiles Include="$(MSBuildProjectDirectory)\.nameanalyzer" />
  </ItemGroup>
```

__Note:__ `.nameanalyzer` file name is __required__ but location is __not required__
