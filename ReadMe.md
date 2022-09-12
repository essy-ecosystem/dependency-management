[![Version 1.3.0](https://img.shields.io/static/v1?label=version&message=1.3.0&color=21C96E&style=for-the-badge)](https://www.nuget.org/profiles/essy-ecosystem)
[![C# 9+](https://img.shields.io/static/v1?label=Csharp&message=9%2B&color=21C96E&style=for-the-badge)](https://dotnet.microsoft.com)
[![Dot NET 6, 7](https://img.shields.io/static/v1?label=DOTNET&message=6,7&color=21C96E&style=for-the-badge)](https://dotnet.microsoft.com)
[![Nullable](https://img.shields.io/static/v1?label=&message=Nullable&color=21C96E&style=for-the-badge)](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
[![Generator](https://img.shields.io/static/v1?label=&message=Generator&color=21C96E&style=for-the-badge)](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)

<a href="https://www.nuget.org/profiles/essy-ecosystem">
    <img src="Assets/icon-128-preview.png" alt="NuGet" width="64" />
</a>

# Dependency Management

The Dependency Management is a very fast dependency injection and components container, with many interesting features,
and without reflection.

Its allows you to make your project more extensible and modular.

## Features

> The project contains many extensions (eg - modularity) and nuances that are not described here, read
> the [documentation]().

- Without any [reflection](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection). Only (
  optional) [code generation](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview).
- Faster than other popular same projects, such as [Autofac](https://github.com/autofac/Autofac)
  , [Niject](https://github.com/ninject/Ninject), other.
- Supports [DotNet 6](https://dotnet.microsoft.com), [7](https://dotnet.microsoft.com),
  also [Native AOT](https://docs.microsoft.com/en-us/dotnet/core/deploying/native-aot)
  , [Nullable](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references).
- The project is extremely modular and can easily be expanded from the outside, as it uses a components system.

## Example

> This example is very primitive and does not allow you to take into all the features of the project.

*Dependency.cs*

```C#
public sealed class Dependency { }
```

*Service.cs*

```C#
public sealed class Service
{
    private readonly Dependency _dependency;

    public Service(Dependency dependency)
    {
        _dependency = dependency;
    }
}
```

### With Code Generator

*Program.cs*

```C#
await using var container = new Container()
    .WithStrategies()
    .WithProviders();

container.SetTarget<Dependency>().ToSingleton();

container.SetTarget<Service>().ToTransient();

var readOnlyContainer = container.ToReadOnly();

var service = readOnlyContainer.LastInstance<Service>();
```

### Without Code Generator

*Program.cs*

```C#
await using var container = new Container().WithStrategies();

container.SetTarget<Dependency>()
    .With(_ => new())
    .ToSingleton();

container.SetTarget<Service>()
    .With(c => new(c.LastInstance<Dependency>()))
    .ToTransient();

var readOnlyContainer = container.ToReadOnly();

var service = readOnlyContainer.LastInstance<Service>();
```

## Installation

```xml
<PropertyGroup>
    <Nullable>enable</Nullable>
  
    <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
    <CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)Generated</CompilerGeneratedFilesOutputPath>
</PropertyGroup>

<ItemGroup>
    <ProjectReference Include="Essy.DependencyManagement.Injection" Version="1.3.0"/>
    <ProjectReference Include="Essy.DependencyManagement.Injection.Generator" Version="1.3.0" 
                      OutputItemType="Analyzer" 
                      ReferenceOutputAssembly="false"/>
    
    <ProjectReference Include="Essy.DependencyManagement.Modularity" Version="1.3.0"/>
    <ProjectReference Include="Essy.DependencyManagement.Modularity.Generator" Version="1.3.0" 
                      OutputItemType="Analyzer" 
                      ReferenceOutputAssembly="false"/>
</ItemGroup>
```

## Benchmarks

The Dependency Management contains benchmarks to conduct measured performance comparison between this and the following dependency injection frameworks:

- Microsof Dependency Injection;
- Autofac;
- Ninject.

```
BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.4 (21F79) [Darwin 21.5.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.100-preview.6.22352.1
  [Host]        : .NET 7.0.0 (7.0.22.32404), X64 RyuJIT AVX2
  NativeAOT 7.0 : .NET 7.0.0 (7.0.22.32404), X64 RyuJIT AVX2
  .NET 7.0      : .NET 7.0.0 (7.0.22.32404), X64 RyuJIT AVX2


|                Method |           Job |       Runtime |      Mean |     Error |    StdDev |    Median |    Gen0 |   Gen1 |   Gen2 | Allocated |
|---------------------- |-------------- |-------------- |----------:|----------:|----------:|----------:|--------:|-------:|-------:|----------:|
|  DependencyManagement | NativeAOT 7.0 | NativeAOT 7.0 |  10.11 us |  0.237 us |  0.676 us |  10.05 us |  2.9297 | 2.7161 | 0.1068 |   5.88 KB |
|  DependencyManagement |      .NET 7.0 |      .NET 7.0 |  19.63 us |  0.363 us |  0.532 us |  19.63 us |  3.0212 | 2.5330 | 0.1831 |   5.88 KB |
|               Autofac | NativeAOT 7.0 | NativeAOT 7.0 |  20.96 us |  0.459 us |  1.303 us |  20.56 us |  9.4910 | 9.4604 |      - |  19.38 KB |
|               Autofac |      .NET 7.0 |      .NET 7.0 |  21.75 us |  0.339 us |  0.300 us |  21.74 us |  9.3689 | 9.3384 |      - |  19.38 KB |
| MSDependencyInjection |      .NET 7.0 |      .NET 7.0 |  34.42 us |  3.169 us |  9.193 us |  36.33 us | 34.8969 | 1.3275 |      - |   5.77 KB |
| MSDependencyInjection | NativeAOT 7.0 | NativeAOT 7.0 |  52.93 us |  8.672 us | 25.298 us |  47.85 us | 34.9579 | 1.3504 |      - |   5.77 KB |
|               Ninject |      .NET 7.0 |      .NET 7.0 | 586.78 us | 28.346 us | 83.578 us | 577.85 us | 15.6250 | 1.9531 |      - |  29.58 KB |
|               Ninject | NativeAOT 7.0 | NativeAOT 7.0 | 835.09 us | 32.496 us | 88.406 us | 827.90 us | 13.6719 | 0.9766 |      - |  29.25 KB |
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

Dependency Management is licensed under [MIT license](License.txt).
