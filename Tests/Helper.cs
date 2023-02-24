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
}