using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day13 : ASolution
    {
        public Day13(Config config) : base(config, 13, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var (line1, line2, _) = InputLines;
            var earliest = int.Parse(line1);
            var busses = line2.Split(',').Where(s => s != "x").Select(s => int.Parse(s));

            var earliestBus = busses.Select(b => (busid: b, wait: (earliest+b-1)/b*b)).Aggregate((curmin, x) => x.wait < curmin.wait ? x : curmin);
            yield return earliestBus.busid * (earliestBus.wait - earliest);
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var (_, line2, _) = InputLines;
            var (bus1, busses) =  line2.Split(',').Select(s => s == "x" ? 0 : int.Parse(s)).Zip(Enumerable.Range(0, int.MaxValue), (first, second) => (busid: first, remainder: second)).Where(b => b.busid != 0).ToList();
            long mod = bus1.busid;
            
            long t = bus1.busid;

            foreach (var bus in busses) {
                while ((t + bus.remainder)  % bus.busid != 0)
                    t += mod;
                mod *= bus.busid;
            }

            yield return t;
        }

        protected override void Asserts()
        {
            DebugInput = @"939
7,13,x,x,59,x,31,19";
            CollectionAssert.AreEqual(new [] {295}, SolvePartOne());
            CollectionAssert.AreEqual(new [] {1068781L}, SolvePartTwo());
        }
    }
}