namespace NetPlayground.Tests.Model;

public class SimplePool<T> where T : class, new()
{
    private List<T> _instances;

    public SimplePool()
    {
        _instances = new List<T>();
    }

    public T Get()
    {
        if (_instances.Count == 0)
        {
            var instance = new T();
            return instance;
        }

        var result = _instances.Last();
        _instances.RemoveAt(_instances.Count - 1);
        return result;
    }

    public void Release(T instance)
    {
        if (_instances.Contains(instance))
        {
            return;
        }

        _instances.Add(instance);
    }
}