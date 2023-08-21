using BenchmarkDotNet.Attributes;

namespace NetPlayground.Tests;

[MemoryDiagnoser]
public class ListVsDictionaryTest
{
    public IEnumerable<object> Lists()
    {
        yield return GenerateList(10);
        yield return GenerateList(25);
        yield return GenerateList(50);
        yield return GenerateList(100);
    }

    public IEnumerable<object> Dictionaries()
    {
        yield return GenerateDictionary(10);
        yield return GenerateDictionary(25);
        yield return GenerateDictionary(50);
        yield return GenerateDictionary(100);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public Item SelectFromList(List<Item> list)
    {
        var count = list.Count;
        Item item = null;
        for (var i = 0; i < count; i++)
        {
            item = FindItem(list, i);
        }

        return item;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Dictionaries))]
    public Item SelectFromDictionary(Dictionary<int, Item> dict)
    {
        var count = dict.Count;
        Item item = null;
        for (var i = 0; i < count; i++)
        {
            if (dict.TryGetValue(i, out var res))
            {
                item = res;
            }
        }

        return item;
    }

    public Item FindItem(List<Item> list, int id)
    {
        foreach (var item in list)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    private List<Item> GenerateList(int max)
    {
        return Enumerable.Range(0, max)
            .Select(i => new Item
            {
                Id = i
            }).ToList();
    }

    private Dictionary<int, Item> GenerateDictionary(int max)
    {
        return Enumerable.Range(0, max)
            .Select(i => new Item
            {
                Id = i
            }).ToDictionary(i => i.Id);
    }
}