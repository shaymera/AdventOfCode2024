using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\paola\advent_of_code\AOC_8_1\AOC_8_1\AOC_8_1.txt";

        var map = File.ReadAllLines(filePath).Select(x => x.ToCharArray().ToList()).ToList();

        int rowCount = map.Count;
        int colCount = map[0].Count;

        //hash set of antennas -> once each set is grouped -> 
        var antennas = FindAntennas();

        Dictionary<char, List<(int x, int y)>> FindAntennas()
        {
            var antennas = new Dictionary<char, List<(int x, int y)>>();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    char c = map[i][j];
                    if (map[i][j] != '.')
                    {
                        if (antennas.ContainsKey(c))
                        {
                            antennas[c].Add((i, j));
                        } 
                        else
                        {
                            antennas.Add(c, new List<(int x, int y)> { (i, j) });
                        }
                    }
                }
            }
            return antennas;
        }  

        var antinodes = new HashSet<(int x, int y)>();

        foreach (var antennaGroup in antennas)
        {
            for (int i = 0; i < antennaGroup.Value.Count - 1; i++)
            {
                FindAntinodes(antennaGroup, i);
            }
        }
        
        HashSet<(int x, int y)> FindAntinodes(KeyValuePair<char, List<(int x, int y)>> antennaGroup, int index)
        {
            var locations = new List<(int x, int y)>(antennaGroup.Value);

            for (int i = 0; i < locations.Count; i++)
            {
                if (i != index)
                {
                    int dX = locations[i].x - locations[index].x;
                    int dY = locations[i].y - locations[index].y;

                    (int x, int y) firstAnti = (locations[i].x + dX, locations[i].y + dY);
                    (int x, int y) secondAnti = (locations[index].x - dX, locations[index].y - dY);

                    AddAntinode(firstAnti); AddAntinode(secondAnti);
                }
            }
            return antinodes;
        }

        void AddAntinode((int x, int y) antinode) {
            if (IsInBounds(antinode) && !antinodes.Contains(antinode))
            {
                antinodes.Add(antinode);
            }
        }

        bool IsInBounds((int x, int y) antinode)
        {
            return antinode.y >= 0 && antinode.y < rowCount && antinode.x >= 0 && antinode.x < colCount;
        }

        Console.WriteLine(antinodes.Count);
    }
}