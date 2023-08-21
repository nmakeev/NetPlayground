using BenchmarkDotNet.Attributes;
using System.Text;
using System.Text.RegularExpressions;

[MemoryDiagnoser]
public class LocalizationManagerTest
{
    private static StringBuilder _stringBuilderForNaive;
    private static StringBuilder _stringBuilderForSmart;

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

 

    [Benchmark]
    [ArgumentsSource(nameof(InputsWithPreparedPairs))]
    public string ApplyFormat_StringBuilder(string value, LocPair pair1, LocPair pair2, LocPair pair3)
    {
        _stringBuilderForNaive ??= new StringBuilder();
        _stringBuilderForNaive.Clear();
        _stringBuilderForNaive.Append(value);

        _stringBuilderForNaive.Replace(pair1.Key, pair1.Substitution);
        _stringBuilderForNaive.Replace(pair2.Key, pair1.Substitution);
        _stringBuilderForNaive.Replace(pair3.Key, pair1.Substitution);
        return _stringBuilderForNaive.ToString();
    }

    [Benchmark]
    [ArgumentsSource(nameof(InputsWithPairs))]
    public string ApplyFormat_Smart(string value, LocPair pair1, LocPair pair2, LocPair pair3)
    {
        _stringBuilderForSmart ??= new StringBuilder();
        _stringBuilderForSmart.Clear();

        for (var i = 0; i < value.Length - 1; i++)
        {
            if (value[i] == '$' && value[i + 1] == '{')
            {
                var end = value.IndexOf('}', i);
                if (end == -1)
                {
                    //TODO: error
                    return value;
                }

                var key = value.Substring(i + 2, end - i - 2);
				Console.WriteLine(key);
                var (result, pair) = Find(key, pair1, pair2, pair3);
                if (result)
                {
                    _stringBuilderForSmart.Append(pair.Substitution);
                }
                else
                {
                    _stringBuilderForSmart.Append('$').Append('{').Append(key).Append('}');
                }
				i = end;
            }
            else
            {
                _stringBuilderForSmart.Append(value[i]);
            }
        }
        
        (bool, LocPair) Find(string key, LocPair pair1, LocPair pair2, LocPair pair3)
        {
            if (pair1.Key == key)
            {
                return (true, pair1);
            }

            if (pair2.Key == key)
            {
                return (true, pair2);
            }

            if (pair3.Key == key)
            {
                return (true, pair3);
            }

            return (false, pair1);
        }

        return _stringBuilderForSmart.ToString();
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