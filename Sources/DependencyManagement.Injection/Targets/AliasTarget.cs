namespace DependencyManagement.Targets;

using System.Runtime.CompilerServices;
using Containers;

public sealed class AliasTarget<TA, T> : Target<TA> where T : class, TA where TA : notnull
{
    private ITarget<T> _target;

    public AliasTarget(ITarget<T> target)
    {
        _target = target;
    }

    public override TA GetInstance(IReadOnlyContainer container)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        return _target.GetInstance(container);
    }

    public override bool ContainsInstance(TA instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        return _target.ContainsInstance(Unsafe.As<T>(instance));
    }

    public override bool RemoveInstance(TA instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        return _target.RemoveInstance(Unsafe.As<T>(instance));
    }

    protected override void DisposeCore(bool disposing)
    {
        if (disposing) _target?.Dispose();
        
        _target = null!;
    }

    protected override ValueTask DisposeCoreAsync()
    {
        return _target is not null
            ? _target.DisposeAsync()
            : default;
    }
}