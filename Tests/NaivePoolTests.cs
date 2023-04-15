using BenchmarkDotNet.Attributes;
using NetPlayground.Tests.Model;

namespace NetPlayground.Tests;

public class NaivePoolTests
{
    private const int EntriesCount = 10000;
    private Random _random;
    private SimplePool<List<int>> _pool;
    
    [GlobalSetup]
    public void Setup()
    {
        _random = new Random();
        _pool = new SimplePool<List<int>>();
    }

    [Benchmark]
    public long SumWithNewList()
    {
        var list = Helper.GenerateList(_random, EntriesCount);
        return Sum(list);
    }

    [Benchmark]
    public long SumWithListFromPool()
    {
        var list = _pool.Get();
        Helper.FillList(list, _random, EntriesCount);
        var result = Sum(list);
        _pool.Release(list);
        return result;
    }

    [Benchmark]
    public long SumWithListFromPoolTryFinally()
    {
        var list = _pool.Get();
        try
        {
            Helper.FillList(list, _random, EntriesCount);
            return Sum(list);
        }
        finally
        {
            _pool.Release(list);
        }
    }

    private long Sum(List<int> list)
    {
        var result = 0l;
        
        foreach (var i in list)
        {
            result += i;
        }

        return result;
    }
}