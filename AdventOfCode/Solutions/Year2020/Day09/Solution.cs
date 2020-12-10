using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day09 : ASolution
    {
        public Day09(Config config) : base(config, 09, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            yield return BreakCypher(InputLongs, 25, 25);
        }

        private long BreakCypher(IList<long> numbers, int groupSize, int offset)
        {
            for (; offset < numbers.Count; offset++) {
                long num = numbers[offset];
                var preamble = new HashSet<long>(numbers.Skip(offset - groupSize).Take(groupSize));

                foreach (var i in preamble) {
                    if (preamble.Contains(num - i) && i != num - i)
                    {
                        continue;
                    }
                }

                return numbers[offset];
            }

            throw new InvalidOperationException("Should not happen");
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            return null;
        }

        protected override void Asserts()
        {
            var testInput = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".Sp
        }
    }
}