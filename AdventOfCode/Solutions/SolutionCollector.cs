using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    internal sealed class SolutionCollector
    {
        private readonly ICollection<ASolution> Solutions;

        public static SolutionCollector Load(Config config, int year, int[] days)
        {
            return new SolutionCollector(config, year, days);
        }

        public void Solve()
        {
            foreach (var solution in Solutions)
            {
                if (!solution.Solve()) {
                    return;
                }
            }
        }

        private SolutionCollector(Config config, int year, int[] days) => Solutions = LoadSolutions(config, year, days).ToArray();

        private static IEnumerable<ASolution> LoadSolutions(Config config, int year, int[] days)
        {
            if(days.Sum() == 0)
            {
                var now = DateTimeOffset.UtcNow;

                if (now.Month == 12)
                {
                    days = Enumerable.Range(1, now.Day).ToArray();
                }
                else {
                    days = Enumerable.Range(1, 25).ToArray();
                }
            }

            foreach(int day in days)
            {
                var solution = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day:D2}");
                if (solution != null)
                {
                    yield return (ASolution)Activator.CreateInstance(solution, config);
                }
            }
        }
    }
}
