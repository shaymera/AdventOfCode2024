using System.Collections.Generic;
using System.Reflection.Metadata;

void main()
{
    String filePath = @"C:\Users\paola\advent_of_code\AOC_5_1\AOC_5_1\AOC_5_1.txt";

    if (File.Exists(filePath))
    {
        string[] inputLines = File.ReadAllLines(filePath);

        List<List<int>> rules = new List<List<int>>();
        List<List<int>> input = new List<List<int>>();
            
        int split = Array.IndexOf(inputLines, "");

        rules = createRules(inputLines.Take(split).ToArray());
        input = createInput(inputLines.Skip(split+1).Take(inputLines.Length-1).ToArray());

        int result = 0;

        for (int i = 0; i < input.Count; i++)
        {
            if (validateInput(input[i], rules))
            {
                result += findMiddle(input[i]);
            }
        }

        Console.WriteLine(result);

    }
}

List<List<int>> createRules(string[] lines)
{
    List<List<int>> rules = new List<List<int>>();
    for (int i = 0; i < lines.Length; i++)
    {
        rules.Add(lines[i].Split('|').Select(x => int.Parse(x)).ToList());
    }
    return rules;
}

List<List<int>> createInput(string[] lines)
{
    List<List<int>> input = new List<List<int>>();
    for (int i = 0; i < lines.Length; i++)
    {
        input.Add(lines[i].Split(',').Select(x => int.Parse(x)).ToList());
    }
    return input;
}

bool validateInput(List<int> input, List<List<int>> rules)
{
    for (int i = 0; i < rules.Count; i++)
    {
        int indexKey = input.IndexOf(rules[i][0]);
        int indexValue = input.IndexOf(rules[i][1]);
        if (indexKey > indexValue && indexKey != -1 && indexValue != -1)
        {
            return false;
        }
    }
    return true;
}

int findMiddle(List<int> update)
{
    int middle = update[update.Count / 2];
    return middle;
}

main();