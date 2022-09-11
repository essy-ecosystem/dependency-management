namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using System.Timers;
using Core.Utils;
using Models;

public sealed class IsolatedScopeStrategy : Strategy
{
    private readonly Timer _threadsCleaner;

    private readonly ConcurrentDictionary<Thread, SingletonStrategy> _threadsScopeStrategies;

    /// <summary />
    public IsolatedScopeStrategy()
    {
        _threadsScopeStrategies = new ConcurrentDictionary<Thread, SingletonStrategy>();
        _threadsCleaner = CreateTimer();
    }

    /// <inheritdoc />
    public override T GetInstance<T>(StrategyContext<T> context)
    {
        Thrower.ThrowIfObjectDisposed(this);

        var thread = Thread.CurrentThread;

        if (_threadsScopeStrategies.TryGetValue(thread, out var currentSingletonStrategy))
        {
            return currentSingletonStrategy.GetInstance(context);
        }

        var singletonStrategy = new SingletonStrategy();

        return !_threadsScopeStrategies.TryAdd(thread, singletonStrategy)
            ? GetInstance(context)
            : singletonStrategy.GetInstance(context);
    }

    /// <inheritdoc />
    public override bool ContainsInstance<T>(T instance)
    {
        return _threadsScopeStrategies.Values.Any(singletonStrategy =>
            singletonStrategy.ContainsInstance(instance));
    }

    /// <inheritdoc />
    public override bool RemoveInstance<T>(T instance)
    {
        return _threadsScopeStrategies.Values.Any(singletonStrategy =>
            singletonStrategy.RemoveInstance(instance));
    }

    private Timer CreateTimer()
    {
        var timer = new Timer();
        timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
        timer.Elapsed += (_, _) => OnRequireClearStoppedThreads();
        timer.AutoReset = true;
        timer.Enabled = true;
        return _threadsCleaner;
    }

    private void OnRequireClearStoppedThreads()
    {
        var threads = _threadsScopeStrategies.Keys;

        foreach (var thread in threads)
        {
            if (thread.IsAlive) continue;

            if (_threadsScopeStrategies.TryRemove(thread, out var singletonStrategy))
            {
                singletonStrategy.Dispose();
            }
        }
    }

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        _threadsCleaner.Stop();

        if (disposing)
        {
            foreach (var singletonStrategy in _threadsScopeStrategies.Values)
            {
                singletonStrategy.Dispose();
            }

            _threadsCleaner.Dispose();
        }

        _threadsScopeStrategies.Clear();

        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var singletonStrategy in _threadsScopeStrategies.Values)
        {
            await singletonStrategy.DisposeAsync();
        }

        _threadsCleaner.Dispose();

        await base.DisposeCoreAsync();
    }
}