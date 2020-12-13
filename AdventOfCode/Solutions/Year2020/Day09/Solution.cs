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
                        goto next;
                    }
                }

                return numbers[offset];

                next:
                ;
            }

            throw new InvalidOperationException("Should not happen");
        }

        private long FindSum(IList<long> numbers, long targetSum) {
            for (int i = 0; i < numbers.Count - 1; i++) {
                long runningSum = numbers[i];

                for (int j = i + 1; j < numbers.Count; j++)
                {
                    runningSum += numbers[j];

                    if (targetSum == runningSum) {
                        var seq = numbers.Skip(i).Take(j - i + 1);

                        return seq.Min() + seq.Max();
                    }
                    
                    if (runningSum > targetSum) {
                        break;
                    }
                }
            }

            throw new InvalidOperationException("Should not happen");
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var target = (long)SolvePartOne().First();

            yield return FindSum(InputLongs, target);
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
576".ToIntArray("\r\n").Select(i => (long)i).ToList();

            var result = BreakCypher(testInput, 5, 5);

            Assert.AreEqual(127L, result);

            Assert.AreEqual(62L, FindSum(testInput, 127L));
        }
    }
}