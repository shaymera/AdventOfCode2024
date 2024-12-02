void main()
{
    string filePath = @"C:\Users\paola\advent_of_code\AOC_2_1\AOC_2_1\AOC_2_1.txt";

    if (File.Exists(filePath))
    {
        string[] tempReports = File.ReadAllLines(filePath);

        int succesCount = 0;

        foreach (string report in tempReports)
        {
            if (safetyCheck(report))
            {
                succesCount++;
            }
        }

        Console.WriteLine(succesCount);
    }
    else {
        Console.WriteLine("No file found");
    }
}

bool safetyCheck(string report)
{
    int[] reportValues = report.Split(' ').Select(Int32.Parse).ToArray();
    //sort values so they're increasing
    int[] sortedIncreasingValues = (int[])reportValues.Clone();
    Array.Sort(sortedIncreasingValues);
    //sort values so they're decreasing
    int[] sortedDecreasingValues = (int[])reportValues.Clone();
    Array.Sort(sortedDecreasingValues, (x, y) => y.CompareTo(x));

    if (Enumerable.SequenceEqual(sortedIncreasingValues, reportValues) || 
       (Enumerable.SequenceEqual(sortedDecreasingValues, reportValues)))
    {
        for (int i = reportValues.Length - 1; i > 0; i--)
        {
            int incrementSize = Math.Abs(reportValues[i] - reportValues[i - 1]);
            if (incrementSize == 0 || incrementSize > 3)
            {
                return false;
            }
        }
        return true;
    }
    return false;
}

main();