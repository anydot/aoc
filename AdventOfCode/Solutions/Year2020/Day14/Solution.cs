using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day14 : ASolution
    {
        public Day14(Config config) : base(config, 14, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var mem = new Dictionary<long, long>();
            long andmask = 0, ormask = 0;

            foreach (var line in InputLines) {
                var match = Regex.Match(line, @"mask = (.*)|mem\[(\d+)\] = (.*)");

                if (!match.Success)
                    Fail();

                if (match.Groups[1].Success) {
                    var maskStr = match.Groups[1].Value;
                    andmask = Convert.ToInt64(maskStr.Replace("X", "1"), 2);
                    ormask = Convert.ToInt64(maskStr.Replace("X", "0"), 2);
                }
                else {
                    long pos = long.Parse(match.Groups[2].Value);
                    long val = long.Parse(match.Groups[3].Value);
                    mem[pos] = (val & andmask) | ormask;
                }
            }

            yield return mem.Values.Sum();
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var mem = new Dictionary<long, long>();
            long xmask = 0, ormask = 0;

            foreach (var line in InputLines) {
                var match = Regex.Match(line, @"mask = (.*)|mem\[(\d+)\] = (.*)");

                if (!match.Success)
                    Fail();

                if (match.Groups[1].Success) {
                    var maskStr = match.Groups[1].Value;
                    ormask = Convert.ToInt64(maskStr.Replace("X", "0"), 2);
                    xmask = Convert.ToInt64(maskStr.Replace("1", "0").Replace("X", "1"), 2);
                }
                else {
                    long pos = long.Parse(match.Groups[2].Value);
                    long val = long.Parse(match.Groups[3].Value);
                    PolyFill(mem, pos | ormask, val, xmask);
                }
            }

            yield return mem.Values.Sum();
        }

        private void PolyFill(Dictionary<long, long> mem, long pos, long val, long xmask)
        {
            if (xmask == 0) {
                mem[pos] = val;
                return;
            }

            var bitpos = System.Numerics.BitOperations.TrailingZeroCount(xmask);
            var bitmask = ~(1L << bitpos);

            PolyFill(mem, pos & bitmask, val, xmask & bitmask); // posbit set to zero
            PolyFill(mem, pos | (~bitmask), val, xmask & bitmask); // posbit set to one
        }

        protected override void Asserts()
        {
            DebugInput = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
            CollectionAssert.AreEqual(new [] {165L}, SolvePartOne());

            DebugInput = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";
            CollectionAssert.AreEqual(new [] { 208 }, SolvePartTwo());
        }
    }
}