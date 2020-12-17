using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day12 : ASolution
    {
        public Day12(Config config) : base(config, 12, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var instructions = InputLines.Select(l => (l[0], int.Parse(l.Substring(1))));

            var pos = new Point(0, 0);
            var facing = new Point(1, 0);

            foreach (var (instr, val) in instructions) {
                switch (instr) {
                    case 'N':
                        pos = pos.Add(new Point(0, val));
                        break;
                    case 'S':
                        pos = pos.Add(new Point(0, -val));
                        break;
                    case 'E':
                        pos = pos.Add(new Point(val, 0));
                        break;
                    case 'W':
                        pos = pos.Add(new Point(-val, 0));
                        break;
                    case 'F':
                        pos = pos.Add(new Point(facing.X * val, facing.Y * val));
                        break;
                    case 'L':
                        facing = Left(facing, val);
                        break;
                    case 'R':
                        facing = Right(facing, val);
                        break;
                    default:
                        Fail();
                        break;
                }
            }

            yield return Utilities.ManhattanDistance(new Point(0, 0), pos);
        }

        private Point Right(Point facing, int val) => (val % 360) switch {
            0 => facing,
            90 => new Point(facing.Y, -facing.X),
            180 => new Point(facing.X * -1, facing.Y * -1),
            270 => new Point(-facing.Y, facing.X),
            _ => Fail<Point>()
        };

        private Point Left(Point facing, int val) => Right(facing, 360 - val);

        protected override IEnumerable<object> SolvePartTwo()
        {
                        var instructions = InputLines.Select(l => (l[0], int.Parse(l.Substring(1))));

            var pos = new Point(0, 0);
            var waypoint = new Point(10, 1);

            foreach (var (instr, val) in instructions) {
                switch (instr) {
                    case 'N':
                        waypoint = waypoint.Add(new Point(0, val));
                        break;
                    case 'S':
                        waypoint = waypoint.Add(new Point(0, -val));
                        break;
                    case 'E':
                        waypoint = waypoint.Add(new Point(val, 0));
                        break;
                    case 'W':
                        waypoint = waypoint.Add(new Point(-val, 0));
                        break;
                    case 'F':
                        pos = pos.Add(new Point(waypoint.X * val, waypoint.Y * val));
                        break;
                    case 'L':
                        waypoint = Left(waypoint, val);
                        break;
                    case 'R':
                        waypoint = Right(waypoint, val);
                        break;
                    default:
                        Fail();
                        break;
                }
            }

            yield return Utilities.ManhattanDistance(new Point(0, 0), pos);
        }

        protected override void Asserts()
        {
            DebugInput = @"F10
N3
F7
R90
F11";
            CollectionAssert.AreEqual(new []{ 25 }, SolvePartOne());
            CollectionAssert.AreEqual(new []{ 286 }, SolvePartTwo());
        }
    }
}