using System.Diagnostics;

public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_12_2\AOC_12_2\AOC_12_2.txt";

        List<List<char>> garden = File.ReadAllLines(filePath).Select(x => x.ToCharArray().ToList()).ToList();

        int rowCount = garden.Count;
        int colCount = garden[0].Count;

        var directions = new List<(int y, int x)>()
        {
            (-1,  0),
            ( 0, -1),
            ( 0,  1),
            ( 1,  0)
        };

        (int area, int perimeter, List<(int y, int x)>) SearchGarden(List<List<char>> garden, int y, int x)
        {
            char c = garden[y][x];
            int corners = 0;
            int area = 1;
            var newPatches = new List<(int y, int x)>();
            if (c == '.')
            {
                return (0, 0, newPatches);
            }
            var coordinates = new List<(int y, int x)>()
            {
                (y, x)
            };

            for (int i = 0; i < coordinates.Count; i++)
            {
                y = coordinates[i].y;
                x = coordinates[i].x;

                corners += CheckCorner(garden, y, x, c);

                foreach (var direction in directions)
                {
                    int newY = y + direction.y;
                    int newX = x + direction.x;

                    if (IsInBounds(newY, newX, garden))
                    {
                        if (!coordinates.Contains((newY, newX)))
                        {
                            if (garden[newY][newX] == c)
                            {
                                area++;
                                coordinates.Add((newY, newX));
                            }
                            else if (garden[newY][newX] != '.')
                            {
                                newPatches.Add((newY, newX));
                            }
                        }
                    }
                }
            }
            foreach (var coordinate in coordinates)
            {
                garden[coordinate.y][coordinate.x] = '.';
            }
            return (area, corners, newPatches);
        }

        int CheckCorner(List<List<char>> garden, int y, int x, char c) 
        {
            int cornerCount = 0;
            var patterns = new List<List<(int y, int x, bool isChar)>>()
            {
                new List<(int y, int x, bool isChar)>(){(-1, 0, true),   (-1, 1, false),  (0, 1, true)},
                new List<(int y, int x, bool isChar)>(){(0, -1, true),   (1, -1, false),  (1, 0, true)},
                new List<(int y, int x, bool isChar)>(){(0, 1, true),    (1, 0, true),    (1, 1, false)},
                new List<(int y, int x, bool isChar)>(){(-1, -1, false), (-1, 0, true),   (0, -1, true)},
                new List<(int y, int x, bool isChar)>(){(0, 1, false),   (1, 0, false)},
                new List<(int y, int x, bool isChar)>(){(0, -1, false),  (1, 0, false)},
                new List<(int y, int x, bool isChar)>(){(-1, 0, false),  (0, 1, false)},
                new List<(int y, int x, bool isChar)>(){(-1, 0, false),  (0, -1, false)}
            };

            foreach (var pattern in patterns)
            {
                int patternCheck = 0;
                for (int i = 0; i < pattern.Count; i++)
                {
                    int newY = y + pattern[i].y;
                    int newX = x + pattern[i].x;

                    if (IsInBounds(newY, newX, garden))
                    {
                        if ((garden[newY][newX] == c) == pattern[i].isChar)
                        {
                            patternCheck++;
                        }
                    }
                    else if (!pattern[i].isChar)
                    {
                        patternCheck++;
                    }
                }
                if (patternCheck == pattern.Count)
                {
                    cornerCount++;
                }
            }
            return cornerCount;
        }

        bool IsInBounds(int x, int y, List<List<char>> garden)
        {
            return y >= 0 && y < rowCount && x >= 0 && x < colCount;
        }

        (int area, int corners, List<(int y, int x)> plants) = SearchGarden(garden, 0, 0);

        Int64 result = area * corners;

        while (plants.Count > 0)
        {
            var tempPlants = new List<(int y, int x)>();
            (area, corners, tempPlants) = SearchGarden(garden, plants[0].y, plants[0].x);
            plants.RemoveAt(0);
            if (area > 0)
            {
                plants = plants.Concat(tempPlants).ToList();
            }
            result += area * corners;
        }

        Console.WriteLine(result);
    }
}