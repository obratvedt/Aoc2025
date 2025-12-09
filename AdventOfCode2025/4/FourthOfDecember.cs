namespace AdventOfCode2025._4;

public static class FourthOfDecember
{
    public static void Run()
    {
        var lines = File.ReadAllLines("4/input.txt").Select(s => s.ToList());
        var positions = new List<List<Position>>();
        foreach (var (index,item ) in lines.Index())
        {
            positions.Add(item.Index().Select(valueTuple => new Position(index, valueTuple.Index, valueTuple.Item.ToString())).ToList());
        }

        Part1(positions);
        Part2(positions);

    }

    private static void Part1(List<List<Position>> positions)
    {
        var maxCol = positions[0].Count - 1;
        var maxRow = positions.Count - 1;
        var flattened = positions.SelectMany(l => l);
        var positionsWithPaper = flattened.Where(p => p.Value == "@");

        var withPaper = positionsWithPaper.ToList();
        var numberOfAccessibleRolls = withPaper
            .Select(position => GetAdjacentPositions(position)
                .Where(p => p.Item1 >= 0 && p.Item1 <= maxRow && p.Item2 >= 0 && p.Item2 <= maxCol))
            .Select(adjacentPositions => adjacentPositions
                .Count(adjacentPosition => withPaper
                    .Any(p => p.Row == adjacentPosition.Item1 && p.Column == adjacentPosition.Item2)))
            .Count(count => count < 4);
        Console.WriteLine($"Part 1: {numberOfAccessibleRolls}");
    }

    private static void Part2(List<List<Position>> positions)
    {
        var maxCol = positions[0].Count - 1;
        var maxRow = positions.Count - 1;
        var flattened = positions.SelectMany(l => l);
        var numberOfRemovedRolls = 0;
        while (true)
        {
            var positionsWithPaper = flattened.Where(p => p.Value == "@");
            var withPaper = positionsWithPaper.ToList();
            var removedInThisIteration = 0;
            foreach (var position in from position in withPaper let adjacentPositions = GetAdjacentPositions(position)
                         .Where(p => p.Item1 >= 0 && p.Item1 <= maxRow && p.Item2 >= 0 && p.Item2 <= maxCol)
                         .ToList() let countWithPaper = adjacentPositions
                         .Count(adjacentPosition => withPaper
                             .Any(p => p.Row == adjacentPosition.Item1 && p.Column == adjacentPosition.Item2)) where countWithPaper < 4 select position)
            {
                numberOfRemovedRolls++;
                removedInThisIteration++;
                position.Value = ".";
            }

            if (removedInThisIteration == 0)
            {
                break;
            }
           
        }
        Console.WriteLine($"Part 2: {numberOfRemovedRolls}");
    }

    private static List<(int, int)> GetAdjacentPositions(Position position)
    {
        var leftPosition = (position.Row, position.Column - 1);
        var rightPosition = (position.Row, position.Column + 1);
        var topLeftPosition = (position.Row - 1, position.Column - 1);
        var topPosition = (position.Row - 1, position.Column);
        var topRightPosition = (position.Row - 1, position.Column + 1);
        var bottomLeftPosition = (position.Row + 1, position.Column - 1);
        var bottomPosition = (position.Row + 1, position.Column);
        var bottomRightPosition = (position.Row+ 1, position.Column + 1);
        var adjacentPositions = new List<(int,int)> {leftPosition,rightPosition,topLeftPosition,topPosition,topRightPosition,bottomLeftPosition,bottomPosition,bottomRightPosition};
        return adjacentPositions;
    }


    private class Position(int row, int column, string value)
    {
        public int Row { get; init; } = row;
        public int Column { get; init; } = column;
        public string Value { get; set; } = value;
    }
}
    