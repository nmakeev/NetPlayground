using BenchmarkDotNet.Attributes;

namespace Playground;

public class EnumerationTests
{
    private const int EntriesCount = 100000;
    private List<ClassEntry> _classEntries;
    private List<StructEntry> _structEntries;
    
    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();
        GenerateStructEntries(random);
        GenerateClassEntries(random);
    }

    [Benchmark]
    public float SumWithForClass()
    {
        var result = 0f;
        for (var i = 0; i < _classEntries.Count; i++)
        {
            result += _classEntries[i].Weight;
        }

        return result;
    }

    [Benchmark]
    public float SumWithForStruct()
    {
        var result = 0f;
        for (var i = 0; i < _structEntries.Count; i++)
        {
            result += _structEntries[i].Weight;
        }

        return result;
    }

    [Benchmark]
    public float SumWithForeachClass()
    {
        var result = 0f;
        
        foreach (var classEntry in _classEntries)
        {
            result += classEntry.Weight;
        }

        return result;
    }

    [Benchmark]
    public float SumWithForeachStruct()
    {
        var result = 0f;
        
        foreach (var structEntry in _structEntries)
        {
            result += structEntry.Weight;
        }

        return result;
    }

    [Benchmark]
    public float SumWithLinqClass()
    {
        return _classEntries.Sum(item => item.Weight);
    }

    [Benchmark]
    public float SumWithLinqStruct()
    {
        return _structEntries.Sum(item => item.Weight);
    }

    private void GenerateClassEntries(Random random)
    {
        _classEntries = new List<ClassEntry>(EntriesCount);
        for (var i = 0; i < EntriesCount; i++)
        {
            var entry = new ClassEntry(id: i, weight: random.NextSingle());
            _classEntries.Add(entry);
        }
    }

    private void GenerateStructEntries(Random random)
    {
        _structEntries = new List<StructEntry>(EntriesCount);
        for (var i = 0; i < EntriesCount; i++)
        {
            var entry = new StructEntry(id: i, weight: random.NextSingle());
            _structEntries.Add(entry);
        }
    }
}