using AdventOfCode2025.IO;

namespace AdventOfCode2025._2;

public static class SecondOfDecember
{
    public static void Run()
    {
        Part1();
        Part2();
    }
    private static void Part1()
    {
        var idRanges = ExtractIdRangesFromFile();
        var invalidIds = new List<long>();
        foreach (var idRange in idRanges)
        {
            var pointer = idRange.Start;
            while (pointer <= idRange.End)
            {
                var stringifiedPointer = pointer.ToString();
                if (stringifiedPointer.Length % 2 == 0)
                {
                    if (IsEven(stringifiedPointer))
                        invalidIds.Add(pointer);
                }
                pointer++;
            }
        }
        var sum = invalidIds.Sum();
        Console.WriteLine($"Part 1: {sum}");
    }

    private static void Part2()
    {
        var idRanges = ExtractIdRangesFromFile();
        var invalidIds = new List<long>();
        foreach (var idRange in idRanges)
        {
            var pointer = idRange.Start;
            while (pointer <= idRange.End)
            {
                var stringifiedPointer = pointer.ToString();
                var maxNumberOfNGrams = stringifiedPointer.Length / 2;
                for (var i = 1; i <= maxNumberOfNGrams; i++)
                {
                    if (stringifiedPointer.Length % i != 0)
                    {
                        continue;
                    }

                    var chunks = stringifiedPointer.Chunk(i).Select(c => new string(c)).ToList();

                    if (chunks.Distinct().Count() != 1) continue;
                    
                    invalidIds.Add(pointer);
                    break;
                }
                pointer++;
            }
        }
        var sum = invalidIds.Sum();
        Console.WriteLine($"Part 2: {sum}");
    }
    
    private static bool IsEven(string s) => s[..(s.Length / 2)] == s[(s.Length / 2)..];
    
    
    private static IEnumerable<IdRange> ExtractIdRangesFromFile()
    {
        var lines = FileReader.ReadFile("2/input.txt");
        var idRanges = lines[0]
            .Split(',')
            .Select(id => id.Split('-'))
            .Select(id => new IdRange(long.Parse(id[0]), long.Parse(id[1])));
        return idRanges;
    }
 
    private record IdRange(long Start, long End);
}