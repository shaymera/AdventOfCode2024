using System.Collections.Generic;
using System.Reflection.PortableExecutable;

public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_13_2\AOC_13_2\AOC_13_2.txt";

        List<string> file = File.ReadAllLines(filePath).ToList();

        var input = new List<Dictionary<string, (int y, int x)>>();
        input = ParseInput(file);

        List<Dictionary<string, (int y, int x)>> ParseInput(List<string> file)
        {
            var result = new List<Dictionary<string, (int y, int x)>>();
            string name = "";
            int tmpX = 0;
            int tmpY = 0;
            var tempDict = new Dictionary<string, (int y, int x)>();
            foreach (string machine in file)
            {
                if (machine == "")
                {
                    result.Add(tempDict);
                    tempDict = new Dictionary<string, (int y, int x)>();
                }
                else
                {
                    name = machine.Split(':')[0];
                    if (name == "Prize")
                    {
                        tmpX = int.Parse(machine.Split(':')[1].Split(',')[0].Split('=')[1].Trim());
                        tmpY = int.Parse(machine.Split(':')[1].Split(',')[1].Split('=')[1].Trim());
                    }
                    else
                    {
                        name = name.Split(' ')[1];
                        tmpX = int.Parse(machine.Split(':')[1].Split(',')[0].Split('+')[1].Trim());
                        tmpY = int.Parse(machine.Split(':')[1].Split(',')[1].Split('+')[1].Trim());
                    }
                    tempDict.Add(name, (tmpY, tmpX));
                }
            }
            result.Add(tempDict);
            tempDict = new Dictionary<string, (int y, int x)>();
            return result;
        }

        Int64 tokenResult = 0;

        foreach (var machine in input)
        {
            Int64 tokens = FindPrize(machine);
            if (tokens >= 0)
            {
                tokenResult += tokens;
            }
        }

        Int64 FindPrize(Dictionary<string, (int y, int x)> machine)
        {
            Int64 result = 0;
            (Int64 y, Int64 x) prizeLocation = (machine["Prize"].y + 10000000000000, machine["Prize"].x + 10000000000000);
            (Int64 y, Int64 x) buttonA = (machine["A"]);
            (Int64 y, Int64 x) buttonB = (machine["B"]);

            double pressedA = (double)(prizeLocation.y * buttonB.x - prizeLocation.x * buttonB.y) / (double)(buttonB.x * buttonA.y - buttonB.y * buttonA.x);
            double pressedB = (double)(prizeLocation.y * buttonA.x - prizeLocation.x * buttonA.y) / (double)(buttonA.x * buttonB.y - buttonA.y * buttonB.x);

            if (pressedA != Math.Floor(pressedA) || pressedB != Math.Floor(pressedB))
            {
                return -1;
            }
            result = (Int64)(pressedA * 3 + pressedB);
            return result;
        }

        Console.WriteLine(tokenResult);
    }
}