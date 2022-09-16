using BenchmarkDotNet.Running;
using DependencyManagement.Benchmarks.Versus.Startups;

BenchmarkRunner.Run<InitializeStartup>();
BenchmarkRunner.Run<TransientStartup>();
BenchmarkRunner.Run<SingletonStartup>();
BenchmarkRunner.Run<DisposeStartup>();