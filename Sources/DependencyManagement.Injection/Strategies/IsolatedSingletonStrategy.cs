namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using System.Timers;
using Core.Utils;
using Models;

public sealed class IsolatedSingletonStrategy : Strategy
{
    private readonly Timer _threadsCleaner;
    
    private readonly ConcurrentDictionary<Thread, SingletonStrategy> _threadsSingletonStrategies;

    /// <summary/>
    public IsolatedSingletonStrategy()
    {
        _threadsSingletonStrategies = new();
        _threadsCleaner = CreateTimer();
    }

    /// <inheritdoc />
    public override T GetInstance<T>(StrategyContext<T> context)
    {
        Thrower.ThrowIfObjectDisposed(this);
        
        var thread = Thread.CurrentThread;
        
        if (_threadsSingletonStrategies.TryGetValue(thread, out var currentSingletonStrategy))
        {
            return currentSingletonStrategy.GetInstance(context);
        }

        var singletonStrategy = new SingletonStrategy();

        return !_threadsSingletonStrategies.TryAdd(thread, singletonStrategy) 
            ? GetInstance(context) 
            : singletonStrategy.GetInstance(context);
    }

    /// <inheritdoc />
    public override bool ContainsInstance<T>(T instance)
    {
        return _threadsSingletonStrategies.Values.Any(singletonStrategy =>
            singletonStrategy.ContainsInstance(instance));
    }

    /// <inheritdoc />
    public override bool RemoveInstance<T>(T instance)
    {
        return _threadsSingletonStrategies.Values.Any(singletonStrategy =>
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
        var threads = _threadsSingletonStrategies.Keys;

        foreach (var thread in threads)
        {
            if (thread.IsAlive) continue;

            if (_threadsSingletonStrategies.TryRemove(thread, out var singletonStrategy))
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
            foreach (var singletonStrategy in _threadsSingletonStrategies.Values)
            {
                singletonStrategy.Dispose();
            }
            
            _threadsCleaner.Dispose();
        }
        
        _threadsSingletonStrategies.Clear();
        
        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var singletonStrategy in _threadsSingletonStrategies.Values)
        {
            await singletonStrategy.DisposeAsync();
        }
        
        _threadsCleaner.Dispose();

        await base.DisposeCoreAsync();
    }
}