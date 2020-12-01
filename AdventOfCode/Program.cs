using AdventOfCode.Solutions;
using AdventOfCode;

var config = Config.Get("config.json");
var solutions = SolutionCollector.Load(config, config.Year, config.Days);

solutions.Solve();