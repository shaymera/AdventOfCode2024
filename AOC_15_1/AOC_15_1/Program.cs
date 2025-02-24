using System.ComponentModel.Design;
using System.Security;

public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_15_1\AOC_15_1\AOC_15_1.txt";

        var file = File.ReadAllLines(filePath).ToList();

        var map = new List<List<char>>();
        var path = new List<char>();
        string tmp = "";
        bool splitFound = false;

        foreach (string line in file)
        {
            if (line == "")
            {
                splitFound = true;
            }
            else if (splitFound)
            {
                tmp = tmp + line;
            }
            else
            {
                map.Add(line.ToCharArray().ToList());
            }
        }

        path = tmp.ToCharArray().ToList();

        (int y, int x) FindStartingPosition(List<List<char>> map)
        {
            for (int i = 0; i <  map.Count; i++)
            {
                for (int j = 0;  j < map[i].Count; j++)
                {
                    if (map[i][j] == '@')
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        List<List<char>> walk (List<List<char>> map, List<char> path)
        {
            var position = FindStartingPosition(map);

            var directions = new Dictionary<char, (int dy, int dx)>()
            {
                { '^', (-1, 0 ) },
                { '>', (0,  1 ) },
                { 'v', (1,  0 ) },
                { '<', (0,  -1) },
            };
            for (int i = 0; i < path.Count; i++)
            {
                (int y, int x) = move(position, directions[path[i]]);
                if (map[y][x] == 'O')
                {
                    bool moved = false;
                    (moved, map) = MoveBarrels(map, position, directions[path[i]]);
                    if (moved)
                    {
                        position = (y, x);
                    }
                }
                if (map[y][x] == '.')
                {
                    map[y][x] = '@';
                    map[position.y][position.x] = '.';
                    position = (y, x);
                }
            }
            return map;
        }

        (int y, int x) move ((int y, int x) position, (int dy, int dx) direction)
        {
            position = (position.y + direction.dy, position.x + direction.dx);
            return position;
        }

        (bool, List<List<char>>) MoveBarrels (List<List<char>> map, (int y, int x) position, (int dy, int dx) direction)
        {
            var tmp = new List<(int y, int x)>();
            bool moved = false;
            tmp.Add(position);
            //go to initial barrel
            position = move(position, direction);
            tmp.Add(position);
            while (map[position.y][position.x] == 'O')
            {
                position = move(position, direction);
                tmp.Add(position);
            }
            if (map[position.y][position.x] == '.')
            {
                map[tmp[0].y][tmp[0].x] = '.';
                map[tmp[1].y][tmp[1].x] = '@';
                moved = true;
                for (int i = tmp.Count - 1; i > 1; i--)
                {
                    map[tmp[i].y][tmp[i].x] = 'O';
                }
            }
            return (moved, map);
        }

        List<List<char>> outcome = walk(map, path);

        foreach( var line in outcome)
        {
            for (int i = 0; i <  line.Count; i++)
            {
                Console.Write(line[i]);
            }
            Console.WriteLine("");
        }

        Int64 CalculateGPS(List<List<char>> map)
        {
            Int64 result = 0;
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == 'O')
                    {
                        result += (100 * i) + j;
                    }
                }
            }
            return result;
        }

        Console.WriteLine(CalculateGPS(outcome));
    }
}