using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AdventOfCode.Solutions;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day01 : ASolution
    {
        public Day01(Config config) : base(config, 01, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var set = new HashSet<int>(Input.SplitInput());

            foreach (var a in set)
            {
                int tryB = 2020 - a;

                if (set.Contains(tryB))
                {
                    yield return a * tryB;
                }
            }
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var set = new HashSet<int>(Input.SplitInput());

            foreach (var a in set)
            {
                int tryBC = 2020 - a;

                foreach (var tryB in set)
                {
                    var tryC = tryBC - tryB;

                    if (set.Contains(tryC))
                    {
                        yield return a * tryB * tryC;
                    }
                }
            }
        }
    }
}