using System.Text.RegularExpressions;

void main()
{
    string filePath = @"C:\Users\paola\advent_of_code\AOC_3_1\AOC_3_1\AOC_3_1.txt";

    if (File.Exists(filePath))
    {
        string input = File.ReadAllText(filePath);
        string pattern = @"mul\((\d+),(\d+)\)";

        int result = 0;

        foreach (Match match in Regex.Matches(input, pattern))
        {
            result += multiply(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }
        Console.WriteLine(result);
    }
}

int multiply(int a, int b) { return a * b; }

main();