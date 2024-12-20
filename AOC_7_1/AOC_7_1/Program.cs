﻿class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\paola\advent_of_code\AOC_7_1\AOC_7_1\AOC_7_1.txt";

        var file = File.ReadAllLines(filePath).Select(x => x.Split(':'));
        var input = new HashSet<(Int64 result, List<Int64> numbers)>();

        foreach (var line in file)
        {
            input.Add((Int64.Parse(line[0]), line[1].Trim(' ').Split(' ').Select(x => Int64.Parse(x)).ToList()));
        }

        int rowCount = input.Count;
        
        bool ValidateEquation(Int64 result, List<Int64> numbers)
        {
            Int64 multiplyResult = multiply(numbers[0], numbers[1]);
            Int64 addResult = add(numbers[0], numbers[1]);

            if (numbers.Count == 2)
            {
                if (result == multiplyResult ||
                    result == addResult)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var multiplyNumbers = new List<Int64>(numbers.GetRange(1, numbers.Count - 1));
            multiplyNumbers[0] = multiplyResult;

            var addNumbers = new List<Int64>(numbers.GetRange(1, numbers.Count - 1));
            addNumbers[0] = addResult;

            if (ValidateEquation(result, multiplyNumbers) || ValidateEquation(result, addNumbers))
            {
                return true;
            }
            return false;
        }
        
        Int64 multiply(Int64 x, Int64 y)
        {
            return x * y;
        }

        Int64 add(Int64 x, Int64 y)
        {
            return x + y;
        }

        Int64 sum = 0;

        foreach (var row in input) 
        {
            if (ValidateEquation(row.result, row.numbers))
            {
                sum += row.result;
            }
        }

        Console.WriteLine(sum);
    }
}