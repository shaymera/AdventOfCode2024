void main()
{
    String filePath = @"C:\Users\paola\advent_of_code\AOC_5_2\AOC_5_2\AOC_5_2.txt";

    if (File.Exists(filePath))
    {
        string[] inputLines = File.ReadAllLines(filePath);

        List<List<int>> rules = new List<List<int>>();
        List<List<int>> input = new List<List<int>>();

        int split = Array.IndexOf(inputLines, "");

        rules = createRules(inputLines.Take(split).ToArray());
        input = createInput(inputLines.Skip(split + 1).Take(inputLines.Length - 1).ToArray());

        int result = 0;

        for (int i = 0; i < input.Count; i++)
        {
            result += checkInput(input[i], rules);
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

int checkInput(List<int> input, List<List<int>> rules)
{
    for (int i = 0; i < rules.Count; i++)
    {
        int indexKey = input.IndexOf(rules[i][0]);
        int indexValue = input.IndexOf(rules[i][1]);
        if (indexKey > indexValue && indexKey != -1 && indexValue != -1)
        {
            input = fixInput(input, rules);
            return findMiddle(input);
        }
    }
    return 0;
}

List<int> fixInput(List<int> input, List<List<int>> rules)
{
    List<int> result = new List<int>();
    for (int i = 0; i < input.Count; i++)
    {
        int losses = 0; 
        for (int j = 0; j < input.Count; j++)
        {
            if (inRules(new List<int>() { input[j], input[i] }, rules))
            {
                losses++;
            }
        }
        if (losses == 0)
        {
            List<int> tmp = input
                                 .Where((_, index) => index != i)
                                 .ToList();
            tmp = fixInput(tmp, rules);
            result.Add(input[i]);
            foreach(int value in tmp)
            {
                result.Add(value);
            }
        }
    }
    return result;
}

bool inRules(List<int> check, List<List<int>> rules)
{
    for (int i = 0; i < rules.Count; i++)
    {
        if (check[0] == rules[i][0] && check[1] == rules[i][1])
        {
            return true;
        }
    }
    return false;
}

int findMiddle(List<int> update)
{
    int middle = update[update.Count / 2];
    return middle;
}

main();