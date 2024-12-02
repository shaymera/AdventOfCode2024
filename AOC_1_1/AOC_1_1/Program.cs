// See https://aka.ms/new-console-template for more information

using System.Reflection;

static void main()
{
    string fileDir = @"C:\Users\paola\advent_of_code\AOC_1_1\AOC_1_1\AOC_1_1.txt";

    if (File.Exists(fileDir))
    {
        string[] locationIDs = File.ReadAllLines(fileDir);
        List<int> locationOne = new List<int>();
        List<int> locationTwo = new List<int>();

        foreach (string locationID in locationIDs)
        {
            string[] locations = locationID.Split("   ");

            int resOne = int.Parse(locations[0]);
            int resTwo = int.Parse(locations[1]);
            locationOne.Add(resOne);
            locationTwo.Add(resTwo);
        }

        locationOne.Sort();
        locationTwo.Sort();

        int result = 0;

        for(int i = locationOne.Count - 1; i >= 0; i--) 
        {
            result += Math.Abs(locationOne[i] - locationTwo[i]);
        }
    Console.WriteLine(result);
    }
}

main();