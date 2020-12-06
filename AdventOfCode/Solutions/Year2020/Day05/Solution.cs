using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day05 : ASolution
    {
        public Day05(Config config) : base(config, 05, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            yield return InputLines.Max(ConvertSeat);
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var hs = new HashSet<int>(InputLines.Select(ConvertSeat));

            foreach (var t1 in hs) {
                if (!hs.Contains(t1+1) && hs.Contains(t1+2)) {
                    yield return t1+1;
                }
            }
        }

        private int ConvertInp(string inp, char zero, char one) => Convert.ToInt32(inp.Replace(zero, '0').Replace(one, '1'), 2);

        private int ConvertRow(string inp) => ConvertInp(inp, 'F', 'B');

        private int ConvertCol(string inp) => ConvertInp(inp, 'L', 'R');

        private int ConvertSeat(string inp) => (8 * ConvertRow(inp.Substring(0, 7))) + ConvertCol(inp.Substring(7, 3));

        protected override void Asserts()
        {
            Assert.AreEqual(70, ConvertRow("BFFFBBF"));
            Assert.AreEqual(14, ConvertRow("FFFBBBF"));
            Assert.AreEqual(102, ConvertRow("BBFFBBF"));

            Assert.AreEqual(567, ConvertSeat("BFFFBBFRRR"));
        }
    }
}