using BenchmarkDotNet.Attributes;

namespace NetPlayground.Tests;

[MemoryDiagnoser]
public class EnumStringVsBoxTests
{
    private Random _random;
    private TestEnum[] _values;

    [GlobalSetup]
    public void Setup()
    {
        _random = new Random();
        _values = Enum.GetValues<TestEnum>();
    }

    [Benchmark(Baseline = true)]
    public TestEnum GenerateRandom()
    {
        return _values[_random.Next(_values.Length)];
    }

    [Benchmark]
    public string TestToString() 
    {
        return GenerateRandom().ToString();
    }
    
    [Benchmark]
    public string TestConvert()
    {
        return Convert(GenerateRandom());
    }

    public static string Convert(TestEnum input)
    {
        return input switch
        {
            TestEnum.TestSolution => "TestSolution",
            TestEnum.StartSession => "StartSession",
            TestEnum.FinishSession => "FinishSession",
            TestEnum.BeginTracking => "BeginTracking",
            TestEnum.EndTracking => "EndTracking",
            TestEnum.SendAnalytics => "SendAnalytics",
            TestEnum.ExecuteCommands => "ExecuteCommands",
            TestEnum.ThrowException => "ThrowException",
            TestEnum.CalmDown => "CalmDown",
            TestEnum.DrinkTea => "DrinkTea",
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
    }
}

public enum TestEnum
{
    TestSolution,
    StartSession,
    FinishSession,
    BeginTracking,
    EndTracking,
    SendAnalytics,
    ExecuteCommands,
    ThrowException,
    CalmDown,
    DrinkTea
}