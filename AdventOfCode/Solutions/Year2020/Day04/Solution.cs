using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AdventOfCode.Solutions;
using System.Linq;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day04 : ASolution
    {
        private readonly HashSet<string> Required = new(new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"});
        private readonly HashSet<string> EclValues = new(new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" });

        public Day04(Config config) : base(config, 04, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            yield return P1().Count();
        }

        private IEnumerable<IDictionary<string, string>> P1()
        {
            var passports = Input
                .Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None)
                .Select(b => b.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => s.Split(':')).ToDictionary(s => s[0], s => s[1]));
            return passports.Where(p => new HashSet<string>(p.Keys).IsSupersetOf(Required));
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            yield return P1().Count(IsValidP2);
        }

        private bool IsValidP2(IDictionary<string, string> arg)
        {
            return arg.All(kv => kv.Key switch
             {
                 "byr" => int.TryParse(kv.Value, out var i) && i >= 1920 && i <= 2002,
                 "iyr" => int.TryParse(kv.Value, out var i) && i >= 2010 && i <= 2020,
                 "eyr" => int.TryParse(kv.Value, out var i) && i >= 2020 && i <= 2030,
                 "hgt" => ValidateHgt(kv.Value),
                 "pid" => Regex.IsMatch(kv.Value, "^[0-9]{9}$"),
                 "hcl" => Regex.IsMatch(kv.Value, "^#[0-9a-f]{6}$"),
                 "cid" => true,
                 "ecl" => EclValues.Contains(kv.Value),
                 _ => throw new ArgumentOutOfRangeException(kv.Key)
             });
        }

        private static bool ValidateHgt(string val)
        {
            var v = int.Parse(val.TrimEnd(new char[] { 'c', 'm', 'i', 'n' }));

            if (val.EndsWith("cm"))
            {
                return v >= 150 && v <= 193;
            }
            else if (val.EndsWith("in"))
            {
                return v >= 59 && v <= 76;
            }
            else
            {
                return false;
            }
        }

        protected override void Asserts()
        {
            try
            {
                DebugInput = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";

                var part1 = SolvePartOne();
                CollectionAssert.AreEqual(new object[] { 2 }, part1);

                DebugInput = @"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007";

                CollectionAssert.AreEqual(new object[] { 0 }, SolvePartTwo());

                DebugInput = @"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719";
                CollectionAssert.AreEqual(new object[] { 4 }, SolvePartTwo());
            }
            finally
            {
                DebugInput = null;
            }
        }
    }
}