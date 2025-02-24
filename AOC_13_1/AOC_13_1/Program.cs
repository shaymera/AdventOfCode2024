using System.Collections.Generic;
using System.Reflection.PortableExecutable;

public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_13_1\AOC_13_1\AOC_13_1.txt";

        List<string> file = File.ReadAllLines(filePath).ToList();

        var input = new List<Dictionary<string, (int y, int x)>>();
        input = ParseInput(file);

        List<Dictionary<string, (int y, int x)>> ParseInput (List<string> file)
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
            (int y, int x) prizeLocation = (machine["Prize"]);
            (int y, int x) buttonA = (machine["A"]);
            (int y, int x) buttonB = (machine["B"]);

            float pressedA = (float)(prizeLocation.y * buttonB.x - prizeLocation.x * buttonB.y) / (float)(buttonB.x * buttonA.y -  buttonB.y * buttonA.x);
            float pressedB = (float)(prizeLocation.y * buttonA.x - prizeLocation.x * buttonA.y) / (float)(buttonA.x * buttonB.y - buttonA.y * buttonB.x);
            if ((pressedA*10) % 10 != 0  || (pressedB*10) % 10 != 0 || pressedA > 100 || pressedB > 100)
            {
                return -1;
            }
            result = (int)(pressedA * 3 + pressedB);
            return result;
        }

        Console.WriteLine(tokenResult);
    }
}