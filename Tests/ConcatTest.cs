using System.Text;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class ConcatTest
{
    private StringBuilder _builder;

    [GlobalSetup]
    public void Setup()
    {
        _builder = new StringBuilder();
    }

    [Benchmark]
    [ArgumentsSource("Keys")]   
    public string Concat(string key)
    {
        return string.Concat("${", key, '}');
    }

    [Benchmark]    
    [ArgumentsSource("Keys")]
    public string BuilderChars(string key)
    {
        _builder.Clear();
        return _builder.Append('$').Append('{').Append(key).Append('}').ToString();
    }

    [Benchmark]    
    [ArgumentsSource("Keys")]
    public string BuilderStringAndChars(string key)
    {
        _builder.Clear();
        return _builder.Append("${").Append(key).Append('}').ToString();
    }

    public IEnumerable<object> Keys()
    {
        yield return "id";
        yield return "0123456790";
        yield return "AReallyLongStringLongLong";
    }
}