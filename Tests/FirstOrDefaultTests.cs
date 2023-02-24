using BenchmarkDotNet.Attributes;

namespace NetPlayground.Tests;

public class FirstOrDefaultTests
{
    private const int count = 100000;

    private List<int> _list;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();
        _list = Helper.GenerateList(random, count);
    }

    private T GetFirstOrDefault<T>(List<T> list)
    {
        return list.Count > 0 ? list[0] : default;
    }

    private T GetFirstOrDefaultForEnumerable<T>(IEnumerable<T> enumerable)
    {
        using var enumerator = enumerable.GetEnumerator();
        return enumerator.MoveNext() ? enumerator.Current : default;
    }

    [Benchmark]
    public int GetFirstByIndex()
    {
        return GetFirstOrDefault(_list);
    }
    
    [Benchmark]
    public int GetFirstByEnumerator()
    {
        return GetFirstOrDefaultForEnumerable(_list);
    }
    
    [Benchmark]
    public int GetFirstByLinq()
    {
        return _list.FirstOrDefault();
    }
}