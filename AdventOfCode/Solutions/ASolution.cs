using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AdventOfCode.Solutions
{
    public abstract class ASolution
    {
        private readonly Lazy<string> _input;

        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public string DebugInput { get; set; }
        public string Input => DebugInput ?? (string.IsNullOrEmpty(_input.Value) ? null : _input.Value);
        public string[] InputLines => Input.SplitByNewline();
        public int[] InputInts => InputLines.Select(int.Parse).ToArray();

        private protected ASolution(Config config, int day, int year, string title)
        {
            Day = day;
            Year = year;
            Title = title;
            _input = new Lazy<string>(() => LoadInput(config));
        }

        public void Solve()
        {
            if (Input == null) return;

            var sb = new StringBuilder();

            sb.AppendLine($"--- Day {Day}: {Title} ---");

            Asserts();

            if(DebugInput != null)
            {
                sb.AppendLine("!!! DebugInput used: {DebugInput})");
            }

            ProcessSolutionPart(1, SolvePartOne, sb);
            ProcessSolutionPart(2, SolvePartTwo, sb);

            Console.WriteLine(sb);
        }

        private static void ProcessSolutionPart(int part, Func<IEnumerable<object>> resultFunc, StringBuilder sb)
        {
            var sw = Stopwatch.StartNew();
            var result = (resultFunc() ?? Array.Empty<object>()).ToList();

            sw.Stop();

            if (result.Count == 0)
            {
                sb.AppendLine($"Part {part}: Unsolved");
                return;
            }

            sb.AppendLine($"Part {part}, solved in {sw.ElapsedMilliseconds} ms");
            sb.AppendLine(result.Select(r => $"== {r}{Environment.NewLine}").JoinAsStrings());
        }

        private string LoadInput(Config config)
        {
            string INPUT_FILEPATH = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{Year}/Day{Day:D2}/input"));
            string INPUT_URL = $"https://adventofcode.com/{Year}/day/{Day}/input";
            string input = "";

            if(File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
            {
                input = File.ReadAllText(INPUT_FILEPATH);
            }
            else
            {
                try
                {
                    DateTime CURRENT_EST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
                    if(CURRENT_EST < new DateTime(Year, 12, Day)) throw new InvalidOperationException();

                    using var client = new WebClient();
                    client.Headers.Add(HttpRequestHeader.Cookie, config.Cookie);
                    input = client.DownloadString(INPUT_URL).Trim();
                    File.WriteAllText(INPUT_FILEPATH, input);
                }
                catch(WebException e)
                {
                    var statusCode = ((HttpWebResponse)e.Response).StatusCode;
                    if(statusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine($"Day {Day}: Error code 400 when attempting to retrieve puzzle input through the web client. Your session cookie is probably not recognized.");
                    }
                    else if(statusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Day {Day}: Error code 404 when attempting to retrieve puzzle input through the web client. The puzzle is probably not available yet.");
                    }
                    else
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine($"Day {Day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                }
            }
            return input;
        }

        protected abstract IEnumerable<object> SolvePartOne();
        protected abstract IEnumerable<object> SolvePartTwo();

        protected virtual void Asserts()
        {
        }
    }
}
