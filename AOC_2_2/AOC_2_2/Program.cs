void main()
{
    string filePath = "AOC_2_1.txt";

    if (File.Exists(filePath))
    {
        string[] tempReports = File.ReadAllLines(filePath);

        int succesCount = 0;

        foreach (string report in tempReports)
        {
            List<int> reportLevels = report.Split(' ').Select(Int32.Parse).ToList();
            if (safetyCheck(reportLevels))
            {
                succesCount++;
            }
            else
            {
                for (int i = 0; i < reportLevels.Count; i++)
                {
                    List<int> tempArray = new List<int>(reportLevels);
                    tempArray.RemoveAt(i);
                    if (safetyCheck(tempArray) == true)
                    {
                        succesCount++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(succesCount);
    }
    else
    {
        Console.WriteLine("No file found");
    }
}

bool safetyCheck(List<int> report)
{
    //sort values so they're increasing
    List<int> sortedIncreasing = new List<int>(report);
    sortedIncreasing.Sort();
    //sort values so they're decreasing
    List<int> sortedDecreasing = report.OrderByDescending(x => x).ToList();

    if (Enumerable.SequenceEqual(sortedIncreasing, report) ||
       (Enumerable.SequenceEqual(sortedDecreasing, report)))
    {
        for (int i = report.Count - 1; i > 0; i--)
        {
            int incrementSize = Math.Abs(report[i] - report[i - 1]);
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