using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day10 : ASolution
    {
        public Day10(Config config) : base(config, 10, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            int cur = 0;
            int d1 = 0, d3 = 1;

            foreach (var i in InputInts.OrderBy(t => t)) {
                switch (i - cur) {
                    case 1:
                        d1++;
                        break;
                    case 2:
                        break;
                    case 3:
                        d3++;
                        break;
                    default:
                        Fail();
                        break;
                }

                cur = i;
            }

            yield return d1 * d3;
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var adapters = InputInts.Union(new []{ 0 }).OrderBy(t => t).ToList().AsReadOnly();

            yield return SubSolve2(adapters, 0, new Dictionary<int, long>());
        }

        private long SubSolve2(ReadOnlyCollection<int> adapters, int current, IDictionary<int, long> cache) {
            int cur = adapters[current];
            long result = 0;

            if (cache.TryGetValue(current, out var val)) {
                return val;
            }

            if (current == adapters.Count - 1) {
                return 1;
            }

            for (var i = current + 1; i < adapters.Count && i <= current + 3; i++) {
                if (adapters[i] - cur <= 3) {
                    result += SubSolve2(adapters, i, cache);
                }
            }

            cache[current] = result;
            return result;
        }

        protected override void Asserts()
        {
            DebugInput = @"16
10
15
5
1
11
7
19
6
12
4";
            CollectionAssert.AreEquivalent(new[] {8}, SolvePartTwo());

            DebugInput = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";
            CollectionAssert.AreEquivalent(new[] {19208}, SolvePartTwo());
        }
    }
}