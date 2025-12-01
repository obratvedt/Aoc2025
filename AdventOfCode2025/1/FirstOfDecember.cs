using AdventOfCode2025.IO;

namespace AdventOfCode2025._1;

public static class FirstOfDecember
{
    public static void Run()
    {
        var lines = GetFileInput();
        var combinations = lines
            .Select(l => 
                new Combination(l[0], int.Parse(l[1..]))
            );
        
        var dialPosition = 50;
        var countOnZero = 0;
        var count = 0;
        foreach (var combination in combinations)
        {
            if (combination.Direction == 'L'){
                count += CalculateTimesPassedThroughZero(dialPosition, combination.Distance, true);
                dialPosition -= combination.Distance;
            }
            
            else
            { 
                count += CalculateTimesPassedThroughZero(dialPosition, combination.Distance, false);
                dialPosition += combination.Distance;
            }
            dialPosition = GetDialPosition(dialPosition);
            if (dialPosition == 0)
            {
                countOnZero++;
            }
            
        }
        Console.WriteLine($"Part 1: {countOnZero}");
        Console.WriteLine($"Part 2: {count}");

    }

    private static int GetDialPosition(int rawDialPosition)
    {
        return (rawDialPosition % 100 + 100) % 100;
    }

    private static int CalculateTimesPassedThroughZero(int initialPosition, int changeDistance, bool isMovingLeft)
    {
        var resultingRawPosition = isMovingLeft ? initialPosition - changeDistance : initialPosition + changeDistance;
        
        switch (resultingRawPosition)
        {
            case 0:
                return 1;
            case < 0 and > -100:
                return initialPosition == 0 ? 0 : 1;
            case >= 100 and < 200:
                return 1;
            case <= -100:
            {
                var startCount = initialPosition == 0 ? -1 : 0;
                var isRound = Math.Abs(resultingRawPosition) % 100 == 0;
                var passedThroughZero = (int) Math.Ceiling(decimal.Abs(resultingRawPosition) / 100);
                return isRound ? passedThroughZero+1 + startCount : passedThroughZero + startCount;
            }
            case >= 200:
                return (int) Math.Floor(decimal.Abs(resultingRawPosition) / 100);
            default:
                return 0;
        }
    }
    

    private static List<string> GetFileInput()
    {
        return FileReader.ReadFile("1/input.txt");
    }

    private record Combination(char Direction, int Distance);
}