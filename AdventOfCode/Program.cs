using AdventOfCode.Solutions;

namespace AdventOfCode
{
    public static class Program
    {
        public static void Main()
        {
            var config = Config.Get("config.json");
            var solutions = SolutionCollector.Load(config, config.Year, config.Days);

            solutions.Solve();
        }
    }
}
