namespace DependencyManagement.Injection.Collections;

using System.Collections;
using Composition.Containers;
using Core.Disposables;
using Targets;

public sealed class ReadOnlyTargetList<T> : Disposable, IReadOnlyList<T> where T : class
{
    private readonly Dictionary<ITarget<T>, T> _cache = new();
    private readonly IReadOnlyContainer _container;

    private readonly IReadOnlyList<ITarget<T>> _targets;

    public ReadOnlyTargetList(IReadOnlyContainer container, IReadOnlyList<ITarget<T>> targets)
    {
        _container = container;
        _targets = targets;
    }

    public int Count => _targets.Count;

    public T this[int index] => GetInstance(_targets[index]);

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator<T>(_container, _targets, _cache);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private T GetInstance(ITarget<T> target)
    {
        if (!_cache.TryGetValue(target, out var instance))
        {
            instance = target.GetInstance(_container);
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
        private readonly IReadOnlyContainer _container;

        private readonly IReadOnlyList<ITarget<T>> _targets;

        private int _index;

        public Enumerator(IReadOnlyContainer container, IReadOnlyList<ITarget<T>> targets,
            Dictionary<ITarget<T>, T> cache)
        {
            _container = container;
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
                instance = target.GetInstance(_container);
                _cache.TryAdd(target, instance);
            }

            return instance;
        }
    }
}