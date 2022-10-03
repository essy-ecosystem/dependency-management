[![Version 1.4.2](https://img.shields.io/static/v1?label=version&message=1.4.2&color=21C96E&style=for-the-badge)](https://www.nuget.org/profiles/essy-ecosystem)
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
    <PackageReference Include="Essy.DependencyManagement.Injection" Version="1.4.2"/>
    <PackageReference Include="Essy.DependencyManagement.Injection.Generator" Version="1.4.2" 
                      OutputItemType="Analyzer" 
                      ReferenceOutputAssembly="false"/>
    
    <PackageReference Include="Essy.DependencyManagement.Modularity" Version="1.4.2"/>
    <PackageReference Include="Essy.DependencyManagement.Modularity.Generator" Version="1.4.2" 
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
BenchmarkDotNet=v0.13.2, OS=ubuntu 20.04
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=7.0.100-rc.1.22431.12
```

### Initialize

|                                        Method |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------------------------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|----------:|------------:|
|          'Dependency Management - Initialize' |      .NET 7.0 |  37.63 us |  18.63 us |  54.92 us |  30.90 us |  1.00 |    0.00 |   5.76 KB |        1.00 |
| 'Microsoft Dependency Injection - Initialize' |      .NET 7.0 |  27.91 us |  18.62 us |  54.89 us |  22.10 us |  0.71 |    0.07 |    3.9 KB |        0.68 |
|                        'Autofac - Initialize' |      .NET 7.0 | 126.47 us |  46.97 us | 138.50 us | 107.60 us |  3.53 |    0.35 |  19.07 KB |        3.31 |
|                                               |               |           |           |           |           |       |         |           |             |
|          'Dependency Management - Initialize' | NativeAOT 7.0 |  90.14 us | 172.88 us | 509.74 us |  32.60 us |  1.00 |    0.00 |   5.76 KB |        1.00 |
| 'Microsoft Dependency Injection - Initialize' | NativeAOT 7.0 |  29.11 us |  19.79 us |  58.35 us |  22.50 us |  0.69 |    0.11 |   4.27 KB |        0.74 |
|                        'Autofac - Initialize' | NativeAOT 7.0 | 128.21 us |  45.56 us | 134.35 us | 107.80 us |  3.37 |    0.44 |  19.07 KB |        3.31 |

### Transient

|                                       Method |       Runtime |     Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------------------------------------------- |-------------- |---------:|----------:|----------:|----------:|------:|--------:|
|          'Dependency Management - Transient' |      .NET 7.0 | 1.099 us | 0.4995 us | 0.3304 us | 0.9550 us |  1.00 |    0.00 |
| 'Microsoft Dependency Injection - Transient' |      .NET 7.0 | 2.143 us | 6.8938 us | 4.5598 us | 0.7050 us |  1.48 |    2.45 |
|                        'Autofac - Transient' |      .NET 7.0 | 2.630 us | 0.9855 us | 0.6518 us | 2.4101 us |  2.46 |    0.36 |
|                                              |               |          |           |           |           |       |         |
|          'Dependency Management - Transient' | NativeAOT 7.0 | 1.168 us | 0.5282 us | 0.3494 us | 1.0050 us |  1.00 |    0.00 |
| 'Microsoft Dependency Injection - Transient' | NativeAOT 7.0 | 2.165 us | 7.1158 us | 4.7067 us | 0.6850 us |  1.39 |    2.37 |
|                        'Autofac - Transient' | NativeAOT 7.0 | 2.698 us | 1.0792 us | 0.7138 us | 2.4651 us |  2.36 |    0.35 |

### Singleton

|                                       Method |       Runtime |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------------------------------- |-------------- |---------:|----------:|----------:|------:|--------:|
|          'Dependency Management - Singleton' |      .NET 7.0 | 1.863 us | 0.0601 us | 0.0953 us |  1.00 |    0.00 |
| 'Microsoft Dependency Injection - Singleton' |      .NET 7.0 | 1.270 us | 0.0516 us | 0.0818 us |  0.68 |    0.03 |
|                        'Autofac - Singleton' |      .NET 7.0 | 4.921 us | 0.3616 us | 0.5735 us |  2.64 |    0.20 |
|                                              |               |          |           |           |       |         |
|          'Dependency Management - Singleton' | NativeAOT 7.0 | 1.974 us | 0.0782 us | 0.1241 us |  1.00 |    0.00 |
| 'Microsoft Dependency Injection - Singleton' | NativeAOT 7.0 | 1.384 us | 0.0539 us | 0.0854 us |  0.70 |    0.05 |
|                        'Autofac - Singleton' | NativeAOT 7.0 | 4.963 us | 0.3303 us | 0.5238 us |  2.51 |    0.16 |

### Dispose

|                                     Method |       Runtime |     Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------------------- |-------------- |---------:|----------:|----------:|----------:|------:|--------:|----------:|------------:|
|          'Dependency Management - Dispose' |      .NET 7.0 | 15.30 us |  48.65 us | 143.45 us | 0.7000 us |  1.00 |    0.00 |     816 B |        1.00 |
| 'Microsoft Dependency Injection - Dispose' |      .NET 7.0 | 14.60 us |  44.34 us | 130.74 us | 1.5000 us |  2.22 |    0.40 |     432 B |        0.53 |
|                        'Autofac - Dispose' |      .NET 7.0 | 35.35 us | 116.97 us | 344.88 us | 0.7000 us |  1.01 |    0.19 |     816 B |        1.00 |
|                                            |               |          |           |           |           |       |         |           |             |
|          'Dependency Management - Dispose' | NativeAOT 7.0 | 16.16 us |  51.71 us | 152.48 us | 0.6000 us |  1.00 |    0.00 |     816 B |        1.00 |
| 'Microsoft Dependency Injection - Dispose' | NativeAOT 7.0 | 13.41 us |  40.17 us | 118.44 us | 1.6000 us |  2.42 |    0.41 |     816 B |        1.00 |
|                        'Autofac - Dispose' | NativeAOT 7.0 | 36.74 us | 121.84 us | 359.24 us | 0.6000 us |  1.01 |    0.20 |     816 B |        1.00 |

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

Dependency Management is licensed under [MIT license](License.txt).
