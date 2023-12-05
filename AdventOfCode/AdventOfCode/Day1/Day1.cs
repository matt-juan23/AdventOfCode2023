using System.Text.RegularExpressions;

namespace AdventOfCode.Day1
{
    public static partial class Day1
    {
        private readonly static string _filename = @"C:\Dev\AdventOfCode2023\AdventOfCode\AdventOfCode\Day1\TestInput.txt";

        private static Dictionary<string, int> Mapping = new()
        {
            { "one"  , 1 },
            { "two"  , 2 },
            { "three", 3 },
            { "four" , 4 },
            { "five" , 5 },
            { "six"  , 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine" , 9 }
        };

        private static int MapValue(string value, int prevN = 0)
        {
            if (String.IsNullOrEmpty(value))
            {
                return prevN;
            }

            if (int.TryParse(value, out int n)) return n;
            return Mapping[value];
        }

        public static int SolvePart1()
        {
            using var sr = new StreamReader(_filename);
            int total = 0;
            var rex = Part1Regex();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var matches = rex.Matches(line!);
                if (matches.Count != 1)
                {
                    throw new Exception($"Incorrect number of matches {matches.Count}");
                }

                var groups = matches[0].Groups;
                var combined = $"{groups[1].Value}{groups[2].Value}";
                if (!int.TryParse(combined, out var n))
                {
                    throw new Exception($"Invalid regex number {combined}");
                }
                var value = n < 10 ? n * 11 : n;
                total += value;
            }

            return total;
        }

        public static int SolvePart2()
        {
            using var sr = new StreamReader(_filename);
            int total = 0;
            var rex1 = Part2RegexOneNumber();
            var rex2 = Part2RegexTwoNumbers();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var matches2 = rex2.Matches(line!);
                var value = 0;
                if (matches2.Count == 1)
                {
                    var groups2 = matches2[0].Groups;
                    var match1 = MapValue(groups2[1].Value);
                    var match2 = MapValue(groups2[2].Value, match1);
                    value = match1 * 10 + match2;
                } else
                {
                    var matches1 = rex1.Matches(line!);
                    var groups1 = matches1[0].Groups;
                    var n = MapValue(groups1[1].Value);
                    value = n * 11;
                }

                total += value;
            }

            return total;
        }

        [GeneratedRegex(@"([0-9])?.*([0-9])")]
        private static partial Regex Part1Regex();

        [GeneratedRegex(@"([0-9]|one|two|three|four|five|six|seven|eight|nine)")]
        private static partial Regex Part2RegexOneNumber();

        [GeneratedRegex(@"([0-9]|one|two|three|four|five|six|seven|eight|nine).*([0-9]|one|two|three|four|five|six|seven|eight|nine)")]
        private static partial Regex Part2RegexTwoNumbers();
    }
}
