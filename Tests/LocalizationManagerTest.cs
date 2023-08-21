using BenchmarkDotNet.Attributes;
using System.Text;
using System.Text.RegularExpressions;

[MemoryDiagnoser]
public partial class LocalizationManagerTest
{
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(InputsWithDictionary))]
    public string ApplyFormat(string value, Dictionary<string, string> formatValues = null)
    {
        if(formatValues != null)
        {
            value = Regex.Replace(value, @"\$\{\w+\}", ReplaceKey);

            string ReplaceKey(Match m)
            {
                var formatKey = m.Value.Substring(2, m.Value.Length - 3);
                formatValues.TryGetValue(formatKey, out var formatValue);
                if(formatValue is null)
                {
                    return string.Empty;
                }

                return formatValue;
            }
        }

        return value;
    }

    public readonly struct LocPair
    {
        public readonly string Key;
        public readonly string Substitution;

        public LocPair(string key, string substitution)
        {
            Key = key;
            Substitution = substitution;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(InputsWithPairs))]
    public string ApplyFormat_WithReplace(string value, LocPair pair1, LocPair pair2, LocPair pair3)
    {
        value = ReplaceNaive(value, pair1);
        value = ReplaceNaive(value, pair2);
        value = ReplaceNaive(value, pair3);
        return value;
    }

    [Benchmark]
    [ArgumentsSource(nameof(InputsWithPreparedPairs))]
    public string ApplyFormat_PreparedPairs(string value, LocPair pair1, LocPair pair2, LocPair pair3)
    {
        value = value.Replace(pair1.Key, pair1.Substitution);
        value = value.Replace(pair2.Key, pair1.Substitution);
        value = value.Replace(pair3.Key, pair1.Substitution);
        return value;
    }

    private static StringBuilder _stringBuilder;

    [Benchmark]
    [ArgumentsSource(nameof(InputsWithPreparedPairs))]
    public string ApplyFormat_StringBuilder(string value, LocPair pair1, LocPair pair2, LocPair pair3)
    {
        _stringBuilder ??= new StringBuilder();
        _stringBuilder.Clear();
        _stringBuilder.Append(value);

        _stringBuilder.Replace(pair1.Key, pair1.Substitution);
        _stringBuilder.Replace(pair2.Key, pair1.Substitution);
        _stringBuilder.Replace(pair3.Key, pair1.Substitution);
        return _stringBuilder.ToString();
    }

    private string ReplaceNaive(string value, LocPair pair)
    {
        return value.Replace($"${{{pair.Key}}}", pair.Substitution);
    }

    public IEnumerable<object[]> InputsWithDictionary()
    {
        yield return new object[] { "Your game progress will be transferred from ${loadFromNetwork} to your ${transferToNetwork} account, discarding any progress currently saved on ${thirdNetwork}.", 
            new Dictionary<string, string> { ["loadFromNetwork"] = "Facebook", ["transferToNetwork"] = "Google Play", ["thirdNetwork"] = "Apple" }
        };
    }

    public IEnumerable<object[]> InputsWithPairs()
    {
        yield return new object[] { "Your game progress will be transferred from ${loadFromNetwork} to your ${transferToNetwork} account, discarding any progress currently saved on ${thirdNetwork}.", 
            new LocPair("loadFromNetwork", "Facebook"),
            new LocPair("transferToNetwork", "Google Play"),
            new LocPair("thirdNetwork", "Apple")
        };
    }

    public IEnumerable<object[]> InputsWithPreparedPairs()
    {
        yield return new object[] { "Your game progress will be transferred from ${loadFromNetwork} to your ${transferToNetwork} account, discarding any progress currently saved on ${thirdNetwork}.", 
            new LocPair("${loadFromNetwork}", "Facebook"),
            new LocPair("${transferToNetwork}", "Google Play"),
            new LocPair("${thirdNetwork}", "Apple")
        };
    }
}