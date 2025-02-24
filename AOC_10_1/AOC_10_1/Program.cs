public class Program()
{
    static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_10_1\AOC_10_1\AOC_10_1.txt";

        List<string> input = File.ReadAllLines(filePath).ToList();
        var map = new List<List<int>>();
        foreach(var line in input)
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

        HashSet<(int row, int col)> CheckPaths((int row, int col) trailHead, HashSet<(int row, int col)> trailPeaks)
        {
            int currentHeight = map[trailHead.row][trailHead.col];
            (int row, int col) trailNode = (trailHead.row, trailHead.col);
            if (currentHeight == 9)
            {
                trailPeaks.Add(trailNode);
                return trailPeaks;
            }
            else
            {
                foreach (var direction in directions)
                {
                    int newRow = trailHead.row + direction.dY;
                    int newCol = trailHead.col + direction.dX;

                    if (newRow >= 0 && newRow < rowCount && newCol >= 0 && newCol < colCount)
                    {
                        int newHeight = map[newRow][newCol];

                        if (CheckSlope(currentHeight, newHeight))
                        {
                            trailPeaks = CheckPaths((newRow, newCol), trailPeaks);
                        }
                    }
                }
                return trailPeaks;
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
            var trailPeaks = new HashSet<(int col, int row)>();
            trailPeaks = CheckPaths(trailHead, trailPeaks);
            result += trailPeaks.Count;
        }

        Console.WriteLine(result);
    }
}