using System.Text.RegularExpressions;
public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_14_1\AOC_14_1\AOC_14_1.txt";
        List<string> list = File.ReadAllLines(filePath).ToList();
        List<((int y, int x) position, (int dY, int dX) velocity)> robots = new List<((int y, int x), (int dY, int dX))>();

        foreach(string line in list)
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

        (int y, int x) PredictPosition (((int y, int x) position, (int dY, int dX) velocity) robot, int seconds)
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

        var newRobots = new List<(int y, int x)>();

        foreach (var robot in robots)
        {
            newRobots.Add(PredictPosition(robot, 100));
        }

        int findRobotsInQuadrants (List<(int y, int x)> robotPositions, int middleY, int middleX)
        {
            (int first, int second, int third, int fourth) quadrants = (0, 0, 0, 0);

            for (int i = 0; i < robotPositions.Count; i++)
            {
                if (robotPositions[i].y < middleY && robotPositions[i].x < middleX)
                {
                    quadrants.first++; 
                }
                else if (robotPositions[i].y < middleY && robotPositions[i].x > middleX)
                {
                    quadrants.second++;
                } 
                else if (robotPositions[i].y > middleY && robotPositions[i].x < middleX)
                {
                    quadrants.third++;
                }
                else if (robotPositions[i].y > middleY && robotPositions[i].x > middleX)
                {
                    quadrants.fourth++;
                }
            }

            int result = quadrants.first * quadrants.second * quadrants.third * quadrants.fourth; 
            return result;
        }

        Console.WriteLine(findRobotsInQuadrants(newRobots, roomHeight/2 , roomWidth/2));
        
    }
}