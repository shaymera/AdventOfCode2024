public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_12_1\AOC_12_1\AOC_12_1.txt";

        List<List<char>> garden = File.ReadAllLines(filePath).Select(x => x.ToCharArray().ToList()).ToList();

        int rowCount = garden.Count;
        int colCount = garden[0].Count;


        /* patterns possible */

        /*
         * XX
         * XX
         * 0
         * 
         * XX   XO  OX  OO
         * OO   XO  OX  XX
         * 2
         * 
         * XO   XX  XX  OX
         * XX   OX  XO  XX
         * 2
         * 
         * XO   OX  OO  OO
         * OO   OO  XO  OX
         * 2
         * 
         * OO
         * OO
         * exceptional -> go back one?
        */
        var directions = new List<(int y, int x)>()
        {
            (-1,  0),
            ( 0, -1),
            ( 0,  1),
            ( 1,  0)
        };

        (int area, int perimeter, List<(int y, int x)>) SearchGarden (List<List<char>> garden, int y, int x)
        {
            char c = garden[y][x];
            int perimeter = 0;
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
            
            for (int i = 0; i < coordinates.Count; i ++)
            {
                y = coordinates[i].y;
                x = coordinates[i].x;

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
                                perimeter++;
                                newPatches.Add((newY, newX));

                            }
                            else
                            {
                                perimeter++;
                            }
                        }
                    }
                    else
                    {
                        perimeter++;
                    }
                }
            }
            foreach (var coordinate in coordinates)
            {
                garden[coordinate.y][coordinate.x] = '.';
            }
            return (area, perimeter, newPatches);
        }

        bool IsInBounds (int x, int y, List<List<char>> garden)
        {
            return y >= 0 && y < rowCount && x >= 0 && x < colCount;
        }

        (int area, int perimeter, List<(int y, int x)> plants) = SearchGarden(garden, 0, 0);

        Int64 result = area * perimeter;

        while (plants.Count > 0)
        {
            var tempPlants = new List<(int y, int x)>();
            (area, perimeter, tempPlants) = SearchGarden(garden, plants[0].y, plants[0].x);
            plants.RemoveAt(0);
            if (area > 0)
            {
                plants = plants.Concat(tempPlants).ToList();
            }
            result += area * perimeter;
        }

        Console.WriteLine(result);
    }
}