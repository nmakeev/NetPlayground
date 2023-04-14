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
    
    public static void FillList(List<int> list, Random random, int count)
    {
        for (var i = 0; i < count; i++)
        {
            list.Add(random.Next());
        }
    }
}