[![Version 1.0.3-alpha](https://img.shields.io/static/v1?label=version&message=1.0.3-aplha&color=21C96E&style=for-the-badge)](https://www.nuget.org/profiles/essy-ecosystem)
[![C# 9+](https://img.shields.io/static/v1?label=Csharp&message=9%2B&color=21C96E&style=for-the-badge)](https://dotnet.microsoft.com)
[![Dot NET 6, 7](https://img.shields.io/static/v1?label=DOTNET&message=6,7&color=21C96E&style=for-the-badge)](https://dotnet.microsoft.com)
[![Nullable](https://img.shields.io/static/v1?label=&message=Nullable&color=21C96E&style=for-the-badge)](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
[![Generator](https://img.shields.io/static/v1?label=&message=Generator&color=21C96E&style=for-the-badge)](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)

<a href="https://www.nuget.org/profiles/essy-ecosystem">
    <img src="Assets/icon-128-preview.png" alt="NuGet" width="64" />
</a>

# Dependency Management

The Dependency Management is a very fast dependency injection and components container, with many interesting features,
and without reflexion.

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
await using var composite = new Composite()
    .WithStrategies()
    .WithProviders();

composite.SetTarget<Dependency>().ToSingleton();

composite.SetTarget<Service>().ToTransient();

var readOnlyComposite = composite.ToReadOnly();

var service = composite.LastInstance<Service>();
```

### Without Code Generator

*Program.cs*

```C#
await using var composite = new Composite().WithStrategies();

composite.SetTarget<Dependency>()
    .With(_ => new())
    .ToSingleton();

composite.SetTarget<Service>()
    .With(c => new(c.LastInstance<Dependency>()))
    .ToTransient();

var readOnlyComposite = composite.ToReadOnly();

var service = composite.LastInstance<Service>();
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

Dependency Management is licensed under [MIT license](License.txt).