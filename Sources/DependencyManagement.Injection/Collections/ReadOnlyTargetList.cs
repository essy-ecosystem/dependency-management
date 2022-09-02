namespace DependencyManagement.Injection.Collections;

using System.Collections;
using Composition.Composites;
using Core.Disposables;
using Targets;

public sealed class ReadOnlyTargetList<T> : Disposable, IReadOnlyList<T> where T : class
{
    private readonly Dictionary<ITarget<T>, T> _cache = new();
    private readonly IReadOnlyComposite _composite;

    private readonly IReadOnlyList<ITarget<T>> _targets;

    public ReadOnlyTargetList(IReadOnlyComposite composite, IReadOnlyList<ITarget<T>> targets)
    {
        _composite = composite;
        _targets = targets;
    }

    public int Count => _targets.Count;

    public T this[int index] => GetInstance(_targets[index]);

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator<T>(_composite, _targets, _cache);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private T GetInstance(ITarget<T> target)
    {
        if (!_cache.TryGetValue(target, out var instance))
        {
            instance = target.GetInstance(_composite);
            _cache.TryAdd(target, instance);
        }

        return instance;
    }

    protected override void DisposeCore(bool disposing)
    {
        _cache.Clear();

        base.DisposeCore(disposing);
    }

    public sealed class Enumerator<T> : IEnumerator<T> where T : class
    {
        private readonly Dictionary<ITarget<T>, T> _cache;
        private readonly IReadOnlyComposite _composite;

        private readonly IReadOnlyList<ITarget<T>> _targets;

        private int _index;

        public Enumerator(IReadOnlyComposite composite, IReadOnlyList<ITarget<T>> targets,
            Dictionary<ITarget<T>, T> cache)
        {
            _composite = composite;
            _targets = targets;
            _cache = cache;
            Current = default;
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_index >= _targets.Count) return false;

            Current = GetInstance(_targets[_index]);
            _index++;

            return true;
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Dispose() { }

        private T GetInstance(ITarget<T> target)
        {
            if (!_cache.TryGetValue(target, out var instance))
            {
                instance = target.GetInstance(_composite);
                _cache.TryAdd(target, instance);
            }

            return instance;
        }
    }
}