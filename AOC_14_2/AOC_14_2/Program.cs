using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_14_1\AOC_14_1\AOC_14_1.txt";
        List<string> list = File.ReadAllLines(filePath).ToList();
        List<((int y, int x) position, (int dY, int dX) velocity)> robots = new List<((int y, int x), (int dY, int dX))>();

        foreach (string line in list)
        {
            string pattern = @"p=(\d{1,3}),(\d{1,3}) v=(\d{1,3}|-\d{1,3}),(\d{1,3}|-\d{1,3})";
            Match matches = Regex.Match(line, pattern);
            if (matches.Success)
            {
                robots.Add(((int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[1].Value)), (int.Parse(matches.Groups[4].Value), int.Parse(matches.Groups[3].Value))));
            }
        }

        int roomWidth = 101;
        int roomHeight = 103;

        (int y, int x) PredictPosition(((int y, int x) position, (int dY, int dX) velocity) robot, int seconds)
        {
            (int y, int x) newPosition = (0, 0);

            int dY = seconds * robot.velocity.dY;
            int dX = seconds * robot.velocity.dX;
            newPosition = (robot.position.y + dY, robot.position.x + dX);

            float temp = (float)newPosition.y / (float)roomHeight;
            newPosition.y = (int)Math.Round((temp - Math.Floor(temp)) * roomHeight);
            temp = (float)newPosition.x / (float)roomWidth;
            newPosition.x = (int)Math.Round((temp - Math.Floor(temp)) * roomWidth);

            if (newPosition.y < 0)
            {
                newPosition.y = roomHeight + newPosition.y;
            }
            if (newPosition.x < 0)
            {
                newPosition.x = roomWidth + newPosition.x;
            }
            return newPosition;
        }

        int FindTree (List<((int y, int x) position, (int dY, int dX) velocity)> robots)
        {
            int timer = 10000;
            int result = 0;
            int lowestResult = 100000000;

            var list = new List<List<char>>();
            for (int k = 0; k < roomHeight; k++)
            {
                var tmplist = new List<char>();
                for (int l = 0; l < roomWidth; l++)
                {
                    tmplist.Add('.');
                }
                list.Add(tmplist);
            }
            for (int i = 0;  i < timer; i++)
            {
                var robotPositions = new List<(int y, int x)>();

                foreach (var robot in robots)
                {
                    robotPositions.Add(PredictPosition(robot, i));
                }

                int averageY = robotPositions.Sum(x => x.y) / robotPositions.Count;
                int averageX = robotPositions.Sum(x => x.x) / robotPositions.Count;
                int varianceY = Math.Abs(robotPositions.Sum(x => (x.y - averageY) * (x.y - averageY)));
                int varianceX = Math.Abs(robotPositions.Sum(x => (x.x - averageX) * (x.x - averageX)));
                int variance = varianceX + varianceY;
                if (lowestResult > variance)
                {
                    lowestResult = variance;
                    result = i;
                }

                if (i == 6516)
                {
                    for (int j = 0; j < robotPositions.Count; j++)
                    {
                        list[robotPositions[j].y][robotPositions[j].x] = 'R';
                    }

                    foreach (var line in list)
                    {
                        foreach (char c in line)
                        {
                            Console.Write(c);
                        }
                        Console.WriteLine(" ");
                    }
                }
            }
            return result;
        }



        Console.WriteLine(FindTree(robots));
    }
}