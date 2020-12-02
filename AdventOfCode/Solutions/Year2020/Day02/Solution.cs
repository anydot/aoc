using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AdventOfCode.Solutions;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day02 : ASolution
    {
        public Day02(Config config) : base(config, 02, 2020, "")
        {
        }

        static public bool EvaluateRule(string rule)
        {
            var parts = rule.Split(" ");
            var (minlen, maxlen, _) = parts[0].Split("-", 2).Select(s => int.Parse(s)).ToArray();
            var character = parts[1][0];

            var counts = parts[2].ToCharArray().Count(c => c == character);

            return (minlen <= counts) && (counts <= maxlen);
        }

        static public bool EvaluateRule2(string rule)
        {
            var parts = rule.Split(" ");
            var (pos1, pos2, _) = parts[0].Split("-", 2).Select(s => int.Parse(s)).ToArray();
            var character = parts[1][0];

            return (parts[2][pos1 - 1] == character) ^ (parts[2][pos2 - 1] == character);
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            yield return InputLines.Count(EvaluateRule);
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            yield return InputLines.Count(EvaluateRule2);
        }

        protected override void Asserts()
        {
        }
    }
}