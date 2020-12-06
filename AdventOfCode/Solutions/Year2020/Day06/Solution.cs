using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day06 : ASolution
    {
        public Day06(Config config) : base(config, 06, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            yield return Input
                .Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None)
                .Select(b => b
                    .Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .SelectMany(s => s.ToCharArray())
                    .Distinct()
                    .Count()
                )
                .Sum();
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            yield return Input
                .Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None)
                .Select(b => b
                    .Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s => s.ToCharArray().ToHashSet())
                    .Aggregate((prev, cur) => { prev.IntersectWith(cur); return prev; })
                    .Count()
                )
                .Sum();
        }

        protected override void Asserts()
        {
        }
    }
}