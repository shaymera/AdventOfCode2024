using System.Text.RegularExpressions;

void main()
{
    string filePath = @"C:\Users\paola\advent_of_code\AOC_3_2\AOC_3_2\AOC_3_2.txt";

    if (File.Exists(filePath))
    {
        string input = File.ReadAllText(filePath);
        
        string pattern = @"don't\(\)|do\(\)|mul\((\d+),(\d+)\)";

        int result = 0;
        bool enabled = true;

        foreach (Match match in Regex.Matches(input, pattern))
        {
            if (match.Groups[0].Value == "do()")
            {
                enabled = true;
            }
            else if (match.Groups[0].Value == "don't()")
            {
                enabled = false;
            }
            else if (enabled)
            {
                result += multiply(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
        }
        Console.WriteLine(result);
    }
}

int multiply(int a, int b) { return a * b; }

main();

//so we can check for the indexes of the don't and do... then we can basically split the strings between a do and a dont and only apply the regex on those right? -> snip them out of the string
