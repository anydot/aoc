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
                solution.Solve();
            }
        }

        private SolutionCollector(Config config, int year, int[] days) => Solutions = LoadSolutions(config, year, days).ToArray();

        public ASolution GetSolution(int day)
        {
            try
            {
                return Solutions.Single(s => s.Day == day);
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }

        private static IEnumerable<ASolution> LoadSolutions(Config config, int year, int[] days)
        {
            if(days.Sum() == 0)
            {
                days = Enumerable.Range(1, 25).ToArray();
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
