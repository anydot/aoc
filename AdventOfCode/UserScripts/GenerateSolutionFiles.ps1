param(
    [int]$Year = (Get-Date).Year
)

$template = @"
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AdventOfCode.Solutions;

namespace AdventOfCode.Solutions.Year<YEAR>
{
    public class Day<DAY> : ASolution
    {
        public Day<DAY>(Config config) : base(config, <DAY>, <YEAR>, `"`")
        {
        }

        protected override IEnumerable<object> SolvePartOne()
        {
            return null;
        }

        protected override IEnumerable<object> SolvePartTwo()
        {
            return null;
        }

        protected override void Asserts()
        {
        }
    }
}
"@

$newDirectory = Join-Path $PSScriptRoot ".." "Solutions" "Year$Year" 

if(!(Test-Path $newDirectory)) {
    New-Item $newDirectory -ItemType Directory | Out-Null
}

for($i = 1; $i -le 25; $i++) {
    $newFile = Join-Path $newDirectory "Day$("{0:00}" -f $i)"  "Solution.cs"  
    if(!(Test-Path $newFile)) {
        New-Item $newFile -ItemType File -Value ($template -replace "<YEAR>", $Year -replace "<DAY>", "$("{0:00}" -f $i)") -Force | Out-Null
    }
}

Write-Host "Files Generated"
