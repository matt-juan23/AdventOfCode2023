using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
   public static partial class Day2
   {
        private const int MAX_RED = 12;
        private const int MAX_GREEN = 13;
        private const int MAX_BLUE = 14;
        private readonly static string _filename = @"C:\Dev\AdventOfCode2023\AdventOfCode\AdventOfCode\Day2\Input.txt";

        private static bool IsInvalidNumber(string line, Regex regex, int maxVal)
        {
            var invalid = 
                from match in regex.Matches(line)
                where (Int32.Parse(match.Groups[1].Value) > maxVal)
                select match;
            return invalid.Any();
        }

        private static int GetMinimalColour(string line, Regex regex)
        {
            return regex.Matches(line).Cast<Match>().Select(match => Int32.Parse(match.Groups[1].Value)).Max();
        }

        public static int SolvePart1()
        {
            using var sr = new StreamReader(_filename);
            var game = 1;
            var score = 0;
            var redRegex = RedRegex();
            var greenRegex = GreenRegex();
            var blueRegex = BlueRegex();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                if (!IsInvalidNumber(line, redRegex, MAX_RED)
                    && !IsInvalidNumber(line, greenRegex, MAX_GREEN)
                    && !IsInvalidNumber(line, blueRegex, MAX_BLUE))
                {
                    score += game;
                }
                game++;
            }
            return score;
        }

        public static int SolvePart2()
        {
            using var sr = new StreamReader(_filename);
            var total = 0;
            var redRegex = RedRegex();
            var greenRegex = GreenRegex();
            var blueRegex = BlueRegex();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var minimalPower = GetMinimalColour(line, redRegex) *
                    GetMinimalColour(line, greenRegex) *
                    GetMinimalColour(line, blueRegex);
                total += minimalPower;
            }
            return total;
        }

        [GeneratedRegex(@"(\d+) red")]
        private static partial Regex RedRegex();

        [GeneratedRegex(@"(\d+) blue")]
        private static partial Regex BlueRegex();

        [GeneratedRegex(@"(\d+) green")]
        private static partial Regex GreenRegex();
    }
}
