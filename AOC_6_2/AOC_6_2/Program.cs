void main()
{
    string filePath = @"C:\Users\paola\advent_of_code\AOC_6_2\AOC_6_2\AOC_6_2.txt";

    if (File.Exists(filePath))
    {
        var input = File.ReadAllLines(filePath).ToList();

        //Count all rows & columns 
        int rowCount = input.Count();
        int columnCount = input[0].Count();

        var barrels = new HashSet<(int row, int col)>();
        (int row, int col) guardPosition = (0, 0);
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                if (input[i][j] == '#')
                {
                    barrels.Add((i, j));
                }
                else if (input[i][j] == '^')
                {
                    guardPosition = (i, j);
                }
            }
        }

        var directions = new (int dRow, int dCol)[]
        {
            (-1, 0),
            (0, 1),
            (1, 0),
            (0, -1)
        };

        var startPosition = (guardPosition.row, guardPosition.col, 0);
        var (_, initialPath) = simulatePatrol(barrels, startPosition, rowCount, columnCount, directions);

        // Count the number of positions that cause a loop when a wall is added
        var uniqueVisitedPositions = initialPath
            .Select(pos => (pos.row, pos.col))
            .Distinct();

        int loopCount = 0;
        
        foreach(var pos in uniqueVisitedPositions)
        {
            if (pos == guardPosition)
            {
                continue;
            }
            var testBarrel = new HashSet<(int row, int col)>(barrels) { pos };
            var (foundLoop, _) = simulatePatrol(testBarrel, startPosition, rowCount, columnCount, directions);

            if (foundLoop)
            {
                loopCount++;
            }
        }

        Console.WriteLine(loopCount);
    }
}



int changeDirection(int direction)
{
    return (direction + 1) % 4;
}

bool IsInBounds(int row, int col, int rowCount, int columnCount)
{
    return row >= 0 && row < rowCount && col >= 0 && col < columnCount;
}

(int row, int col, int direction) moveGuard(HashSet<(int row, int col)> barrels, (int row, int col, int direction) position, (int dRow, int dCol)[] directions)
{
    var (currentRow, currentCol, currentDir) = position;
    var (dRow, dCol) = directions[currentDir];
    var newPosition = (currentRow + dRow, currentCol + dCol);

    if (barrels.Contains(newPosition))
    {
        return (currentRow, currentCol, changeDirection(currentDir));
    }
    else
    {
        return (currentRow + dRow, currentCol + dCol, currentDir);
    }
}

(bool foundLoop, HashSet<(int row, int col, int direction)> path) simulatePatrol 
    (HashSet<(int row, int col)> barrels, (int row, int col, int direction) initialPosition, int rowCount, int columnCount, (int dRow, int dCol)[] directions)
{
    var visited = new HashSet<(int row, int col, int direction)>();
    var (currentRow, currentCol, currentDir) = initialPosition;

    while (true)
    {
        if (!IsInBounds(currentRow, currentCol, rowCount, columnCount))
        {
            return (false, visited);
        } 
        else if (visited.Contains((currentRow, currentCol, currentDir))) 
        {
            return (true, visited);
        }

        visited.Add((currentRow, currentCol, currentDir));
        (currentRow, currentCol, currentDir) = moveGuard(barrels, (currentRow, currentCol, currentDir), directions);
    }
}

main();