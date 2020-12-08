using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions;
using NUnit.Framework;

namespace AdventOfCode.Solutions.Year2020
{
    public class Day08 : ASolution
    {
        public const string JMP = "jmp";
        public const string NOP = "nop";
        public const string ACC = "acc";
        public const string END = "end";

        public const string SEEN = "seen";

        private record Instruction(string instruction, int value);

        public Day08(Config config) : base(config, 08, 2020, "")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            var instructions = ParseInstructions();
            long acc;

            Run(instructions, false, out acc);
            yield return acc;
        }

        private bool Run(IList<Instruction> instructions, bool part2, out long acc)
        {
            int ip = 0;
            acc = 0;

            while (true) {
                if (ip >= instructions.Count) {
                    return false;
                }

                var val = instructions[ip];
                instructions[ip] = new Instruction(SEEN, 0);

                switch (val.instruction) {
                    case NOP:
                        ip++;
                        break;
                    case SEEN:
                        return !part2;
                    case ACC:
                        acc += val.value;
                        ip++;
                        break;
                    case JMP:
                        ip += val.value;
                        break;
                    case END:
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            var instructions = ParseInstructions();
            long acc;

            for (int ip = 0; ip < instructions.Count; ip++) {
                if (instructions[ip].instruction switch {
                    JMP => false,
                    NOP => false,
                    _ => true
                }) {
                    continue;
                }

                var copy = new List<Instruction>(instructions);

                switch (copy[ip].instruction) {
                    case JMP:
                        copy[ip] = copy[ip] with { instruction = NOP };
                        break;
                    case NOP:
                        copy[ip] = copy[ip] with { instruction = JMP };
                        break;
                    default:
                        continue;
                }

                if (Run(copy, true, out acc)) {
                    yield return acc;
                }
            }
        }

        private IList<Instruction> ParseInstructions()
        {
            var instrs =  InputLines.Select(s => s.Split()).Select(d => new Instruction(d[0], int.Parse(d[1]))).ToList();
            instrs.Add(new Instruction(END, 0));

            return instrs;
        }

        protected override void Asserts()
        {
            DebugInput = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";
            CollectionAssert.AreEqual(new[] { 8 }, SolvePartTwo());
        }
    }
}