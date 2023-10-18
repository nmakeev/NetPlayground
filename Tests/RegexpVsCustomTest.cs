using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace NetPlayground.Tests;

[MemoryDiagnoser]
public partial class RegexpVsCustomTest
{
    [Params("(scream,choose_right_emoji),(smirk,choose_wrong_emoji)")]
    public string Argument { get; set; }

    [Benchmark]
    public (bool, string, string, string, string) TryParseEmoji()
    {
        var argmuent = Argument;
        const string playerEmojiValidationPattern = @"\(.*,.*\),\(.*,.*\)";
        var isArgumentsValid = Regex.IsMatch(argmuent, playerEmojiValidationPattern);
        if (!isArgumentsValid)
        {   
            return (false, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        argmuent = Regex.Replace(argmuent, @"\(|\)", "");
        var split = Regex.Split(argmuent, ",");
        return (true, split[0], split[1], split[2], split[3]);
    }

    [GeneratedRegex("\\(.*,.*\\),\\(.*,.*\\)")]
    private static partial Regex ValidationPattern();
    [GeneratedRegex("\\(|\\)")]
    private static partial Regex ReplacePattern();
    [GeneratedRegex(",")]
    private static partial Regex SplitPattern();

    [Benchmark]
    public (bool, string, string, string, string) TryParseEmojiGenerated()
    {
        var argmuent = Argument;

        var isArgumentsValid = ValidationPattern().IsMatch(argmuent);
        if (!isArgumentsValid)
        {
            return (false, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        argmuent = ReplacePattern().Replace(argmuent, "");
        var split = SplitPattern().Split(argmuent);
        return (true, split[0], split[1], split[2], split[3]);
    }

    [Benchmark]
    public (bool, string, string, string, string) TryParseEmojiCustom()
    {
        var argument = Argument;

        var index = 0;
        if (!TryFindPart(argument, ref index, '(', ',', out var emoji1SpriteTag) ||
            !TryFindPart(argument, ref index, ',', ')', out var fillBar1Value) ||
            !TryFindPart(argument, ref index, '(', ',', out var emoji2SpriteTag) ||
            !TryFindPart(argument, ref index, ',', ')', out var fillBar2Value))
        {
            return (false, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        return (true, emoji1SpriteTag, fillBar1Value, emoji2SpriteTag, fillBar2Value);
    }

    private static bool TryFindPart(string argument, ref int index, char startChar, char endChar, out string result)
    {
        var startCharIndex = -1;
        result = default;
        for (var i = index; i < argument.Length; i++)
        {
            var c = argument[i];
            if (c == startChar)
            {
                if (startCharIndex != -1)
                {
                    return false;
                }

                startCharIndex = i;
                continue;
            }

            if (c == endChar && startCharIndex != -1)
            {
                var length = i - startCharIndex - 1;
                if (length <= 0)
                {
                    return false;
                }

                result = argument.Substring(startCharIndex + 1, length);
                index = i;
                return true;
            }
        }

        return false;
    }
}