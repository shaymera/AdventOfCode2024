public class Program()
{
    static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_10_2\AOC_10_2\AOC_10_2.txt";

        List<string> input = File.ReadAllLines(filePath).ToList();
        var map = new List<List<int>>();
        foreach (var line in input)
        {
            map.Add(line.Select(x => int.Parse(x.ToString())).ToList());
        }

        int rowCount = map.Count;
        int colCount = map[0].Count;

        var trailHeads = FindTrailHeads();

        var directions = new List<(int dY, int dX)>
        {
            (-1, 0),
            (0, -1),
            (0, 1),
            (1, 0)
        };

        List<(int row, int col)> FindTrailHeads()
        {
            var trailHeads = new List<(int row, int col)>();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (map[i][j] == 0)
                    {
                        trailHeads.Add((i, j));
                    }
                }
            }
            return trailHeads;
        }

        int CheckPaths((int row, int col) trailHead)
        {
            int currentHeight = map[trailHead.row][trailHead.col];
            if (currentHeight == 9)
            {
                return 1;
            }
            else
            {
                int result = 0;
                foreach (var direction in directions)
                {
                    int newRow = trailHead.row + direction.dY;
                    int newCol = trailHead.col + direction.dX;

                    if (newRow >= 0 && newRow < rowCount && newCol >= 0 && newCol < colCount)
                    {
                        int newHeight = map[newRow][newCol];

                        if (CheckSlope(currentHeight, newHeight))
                        {
                            result += CheckPaths((newRow, newCol));
                        }
                    }
                }
                return result;
            }
        }

        bool CheckSlope(int currentHeight, int climbHeight)
        {
            if (climbHeight == currentHeight + 1)
            {
                return true;
            }
            return false;
        }

        int result = 0;

        foreach (var trailHead in trailHeads)
        {
            result += CheckPaths(trailHead);
        }

        Console.WriteLine(result);
    }
}