using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day11 : ASolution
    {
        public Day11(Config config) : base(config, 11, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var input = InputLines.Select(s => s.ToCharArray()).ToList();
            var rows = input.Count;
            var cols = input[0].Length;

            char[,] seating = new char[rows + 2, cols + 2];

            for (int i = 0; i < cols + 2; i++) {
                seating[0, i] = seating[rows+1, i] = '.';
            }

            for (int i = 1; i < rows + 1; i++) {
                seating[i, 0] = seating[i, cols + 1] = '.';
            }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++) {
                    seating[i + 1, j + 1] = input[i][j];
                }

            var copy = (char[,]) seating.Clone();

            bool changed;
            do {
                changed = false;
                for (int i = 1; i <= rows; i++)
                    for (int j = 1; j <= cols; j++) {
                        int usedSeats = 0;

                        for (var boxi = i - 1; boxi <= i + 1; boxi++)
                            for (var boxj = j - 1; boxj <= j + 1; boxj++) {
                                if (boxi == i && boxj == j)
                                    continue;
                                
                                if (seating[boxi, boxj] == '#')
                                    usedSeats++;
                            }

                        switch (seating[i, j]) {
                            case '#' when usedSeats >= 4:
                                copy[i, j] = 'L';
                                changed = true;
                                break;
                            case 'L' when usedSeats == 0:
                                copy[i, j] = '#';
                                changed = true;
                                break;
                            default:
                                copy[i, j] = seating[i, j];
                                break;
                        }
                    }
                
                (seating, copy) = (copy, seating);
            } while (changed);

            yield return seating.Cast<char>().Count(c => c == '#');
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var input = InputLines.Select(s => s.ToCharArray()).ToList();
            var rows = input.Count;
            var cols = input[0].Length;

            char[,] seating = new char[rows + 2, cols + 2];

            for (int i = 0; i < cols + 2; i++) {
                seating[0, i] = seating[rows+1, i] = '.';
            }

            for (int i = 1; i < rows + 1; i++) {
                seating[i, 0] = seating[i, cols + 1] = '.';
            }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++) {
                    seating[i + 1, j + 1] = input[i][j];
                }

            var copy = (char[,]) seating.Clone();

            bool changed;
            do {
                changed = false;
                for (int i = 1; i <= rows; i++)
                    for (int j = 1; j <= cols; j++) {
                        int usedSeats = 0;

                        for (var inci = -1; inci < 2; inci++) {
                            for (var incj = -1; incj < 2; incj++) {
                                if (inci == 0 && incj == 0)
                                    continue;
                                
                                var curi = i;
                                var curj = j;

                                do {
                                    curi += inci;
                                    curj += incj;
                                } while (curi > 0 && curj > 0 && curi <= rows && curj <= cols && seating[curi, curj] == '.');

                                if (seating[curi, curj] == '#')
                                    usedSeats++;
                            }
                        }

                        switch (seating[i, j]) {
                            case '#' when usedSeats >= 5:
                                copy[i, j] = 'L';
                                changed = true;
                                break;
                            case 'L' when usedSeats == 0:
                                copy[i, j] = '#';
                                changed = true;
                                break;
                            default:
                                copy[i, j] = seating[i, j];
                                break;
                        }
                    }
                
                (seating, copy) = (copy, seating);
            } while (changed);

            yield return seating.Cast<char>().Count(c => c == '#');
        }

        protected override void Asserts()
        {
            DebugInput = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            CollectionAssert.AreEqual(new []{ 37 }, SolvePartOne());
            CollectionAssert.AreEqual(new []{ 26 }, SolvePartTwo());            
        }
    }
}