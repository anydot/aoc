using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day07 : ASolution
    {
        public Day07(Config config) : base(config, 07, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var rules = InputLines.Select(ParseRule).ToDictionary(r => r.bag, r => r.content);
            var resolvedRules = ResolveRules(rules);

            yield return resolvedRules.Count(kv => kv.Key != "shiny gold" && kv.Value.ContainsKey("shiny gold"));
        }

        private IDictionary<string, IDictionary<string, long>> ResolveRules(IDictionary<string, IDictionary<string, long>> rules)
        {
            Dictionary<string, IDictionary<string, long>> resolvedRules = new();

            foreach (var bag in rules.Keys) {
                ResolveRule(ref resolvedRules, rules, bag);
            }

            return resolvedRules;
        }

        private IDictionary<string, long> ResolveRule(ref Dictionary<string, IDictionary<string, long>> resolvedRules, IDictionary<string, IDictionary<string, long>> rules, string bag)
        {
            {
                if (resolvedRules.TryGetValue(bag, out var dep)) {
                    return dep;
                }
            }

            Dictionary<string, long> resolvedRule = new();
            resolvedRule[bag] = 1;

            foreach (var depKv in rules[bag]) {
                var dep = ResolveRule(ref resolvedRules, rules, depKv.Key);
                var reps = depKv.Value;

                foreach (var depBagKv in dep) {
                    resolvedRule.TryGetValue(depBagKv.Key, out var count);
                    resolvedRule[depBagKv.Key] = count + (reps * depBagKv.Value);
                }
            }

            resolvedRules[bag] = resolvedRule;
            return resolvedRule;
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var rules = InputLines.Select(ParseRule).ToDictionary(r => r.bag, r => r.content);
            var resolvedRules = ResolveRules(rules);

            yield return resolvedRules["shiny gold"].Values.Sum() - 1;
        }

        private (string bag, IDictionary<string, long> content) ParseRule(string rule)
        {
            var m = Regex.Match(rule, @"^(\w+\s\w+) bags contain(?: no other bags|(?:,? (\d+) (\w+\s\w+) bags?)+)\.$");

            Assert.IsTrue(m.Success);

            var bagName = m.Groups[1].Value;

            var content = Enumerable.Range(0, m.Groups[2].Captures.Count).ToDictionary(i => m.Groups[3].Captures[i].Value, i => long.Parse(m.Groups[2].Captures[i].Value));

            return (bag: bagName, content: content);
        }

        protected override void Asserts()
        {
            var result = ParseRule("light red bags contain 1 bright white bag, 2 muted yellow bags.");

            Assert.AreEqual("light red", result.bag);
            CollectionAssert.AreEqual(new Dictionary<string, long> { 
                ["bright white"] = 1,
                ["muted yellow"] = 2
            }, result.content);

            DebugInput = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";
            CollectionAssert.AreEqual(new long[] { 4 }, SolvePartOne());

        }
    }
}