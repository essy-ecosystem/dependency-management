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
```

|                Method |           Job |       Runtime |       Mean |      Error |     StdDev |     Gen0 |    Gen1 | Allocated |
|---------------------- |-------------- |-------------- |-----------:|-----------:|-----------:|---------:|--------:|----------:|
| MSDependencyInjection | NativeAOT 7.0 | NativeAOT 7.0 |   4.877 us |  0.0932 us |  0.0998 us |  36.1786 |       - |   5.77 KB |
| MSDependencyInjection |      .NET 7.0 |      .NET 7.0 |   5.055 us |  0.0621 us |  0.0550 us |  36.1786 |       - |   5.77 KB |
|  DependencyManagement | NativeAOT 7.0 | NativeAOT 7.0 |   7.750 us |  0.0834 us |  0.0697 us |  37.6434 |       - |   6.01 KB |
|  DependencyManagement |      .NET 7.0 |      .NET 7.0 |   8.153 us |  0.1041 us |  0.0973 us |  37.6434 |       - |   6.01 KB |
|               Autofac | NativeAOT 7.0 | NativeAOT 7.0 |  24.491 us |  0.3315 us |  0.2939 us |  68.1152 |  3.7231 |  19.93 KB |
|               Autofac |      .NET 7.0 |      .NET 7.0 |  25.515 us |  0.4707 us |  0.4173 us |  55.2063 |  4.0283 |  19.92 KB |
|               Ninject |      .NET 7.0 |      .NET 7.0 | 423.103 us |  8.4147 us | 13.8256 us | 184.0820 | 10.2539 |  30.24 KB |
|               Ninject | NativeAOT 7.0 | NativeAOT 7.0 | 688.578 us | 13.1108 us | 12.8765 us | 186.5234 | 10.7422 |  30.35 KB |


## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

Dependency Management is licensed under [MIT license](License.txt).
