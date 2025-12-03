using AdventOfCode2025.IO;

namespace AdventOfCode2025._3;

public static class ThirdOfDecember
{
    public static void Run()
    {
        var lines = FileReader.ReadFile("3/input.txt");
        Part1(lines);
        Part2(lines);
    }

    private static void Part1(List<string> lines)
    {
        var total = lines
            .Select(l => l
                .Select(c => c - '0')
                .ToList()
            )
            .Select(e =>
            {
                var maxValue = e[..^1].Max();
                var maxValueAfterFirstMaxValue = e[(e.IndexOf(maxValue) + 1)..].Max();
                return int.Parse($"{maxValue}{maxValueAfterFirstMaxValue}");
            })
            .Sum();
        
        Console.WriteLine($"Part 1: {total}");
    }
    
    private static void Part2(List<string> lines)
    {
        
        var total = lines
            .Select(l => l
                .Select(c => c - '0')
                .ToList()
            )
            .Select(e =>
            {
                long maxValue = 0;
                var currentList = e;
                for (var numberOfTrailingElementsToKep = 12; numberOfTrailingElementsToKep >= 1; numberOfTrailingElementsToKep--)
                {
                    var maxValueInList = currentList[..^(numberOfTrailingElementsToKep-1)].Max();
                    var indexOfMaxValue = currentList.IndexOf(maxValueInList);
                    maxValue = maxValue * 10 + maxValueInList;                    
                    currentList = currentList.Slice(indexOfMaxValue +1, currentList.Count-indexOfMaxValue -1);
                }
                return maxValue;
            })
            .Sum();
        
        Console.WriteLine($"Part 2: {total}");
    }
}