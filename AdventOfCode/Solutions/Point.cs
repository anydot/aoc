/**
 * This utility class is largely based on:
 * https://github.com/jeroenheijmans/advent-of-code-2018/blob/master/AdventOfCode2018/Util.cs
 */

namespace AdventOfCode.Solutions
{
    public record Point(int X, int Y)
    {
        public Point Add(Point other) => new(this.X + other.X, this.Y + other.Y);
    }
}