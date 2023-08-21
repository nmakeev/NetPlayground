namespace NetPlayground.Tests;

public static class Helper
{
    public static List<int> GenerateList(Random random, int count)
    {
        var result = new List<int>(count);
        for (var i = 0; i < count; i++)
        {
            result.Add(random.Next());
        }

        return result;
    }
    
    public static List<T> GenerateList<T>(int count, Func<T> ctr)
    {
        var result = new List<T>(count);
        for (var i = 0; i < count; i++)
        {
            result.Add(ctr.Invoke());
        }

        return result;
    }
    
    public static Dictionary<TKey, TValue> GenerateDict<TKey, TValue>(int count, Func<TValue> ctr) where TValue : IKey<TKey>
    {
        var result = new Dictionary<TKey, TValue>();
        for (var i = 0; i < count; i++)
        {
            var value = ctr.Invoke();
            result[value.Key] = value;
        }

        return result;
    }
    
    public static void FillList(List<int> list, Random random, int count)
    {
        for (var i = 0; i < count; i++)
        {
            list.Add(random.Next());
        }
    }
}