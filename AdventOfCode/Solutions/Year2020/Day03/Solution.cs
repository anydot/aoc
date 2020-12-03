using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day03 : ASolution
    {
        private const char Tree = '#';

        public Day03(Config config) : base(config, 03, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne() => SolvePartOne(1, 3);

        private IEnumerable<object> SolvePartOne(int incrX, int incrY)
        {
            var lines = InputLines;
            var lineLen = lines[0].Length;

            Assert.IsTrue(lines.All(l => l.Length == lineLen));
            Assert.IsTrue(lines.All(l => l.Trim('.', '#')?.Length == 0));

            var trees = 0;
            var (x, y) = (0, 0);

            do
            {
                // Console.WriteLine($"X: {x}, Y: {y}, content: {lines[x][y]}");
                if (lines[x][y] == Tree)
                {
                    trees++;
                }

                x += incrX;
                y = (y + incrY) % lineLen;
            } while (x < lines.Length);

            yield return trees;
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var traversals = new (int incrX, int incrY)[] { (1, 1), (1, 3), (1, 5), (1, 7), (2, 1)};

            yield return traversals.Aggregate(1L, (start, incr) => start * SolvePartOne(incr.incrX, incr.incrY).Cast<int>().First());
        }

        protected override void Asserts()
        {
            try
            {
                DebugInput = @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

                CollectionAssert.AreEqual(new object[] { 7 }, SolvePartOne());
                CollectionAssert.AreEqual(new object[] { 336 }, SolvePartTwo());
            }
            finally
            {
                DebugInput = null;
            }
        }
    }
}