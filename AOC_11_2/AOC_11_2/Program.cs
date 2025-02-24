using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

public static class Program
{
    public static void Main()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
       
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_11_2\AOC_11_2\AOC_11_2.txt";

        int blinkCount = 75;
        List<Int64> input = File.ReadAllText(filePath).Split(' ').Select(x => Int64.Parse(x)).ToList();
        //count all the stones, and turn them into Stone|Count pairs.
        var stones = input.ToDictionary(x => x, x => input.LongCount(y => y == x));
        //var calculationResults = new Dictionary<Int64, Int64>();

        Dictionary<Int64, Int64> blink(Dictionary<Int64, Int64> stones/*, int blinkCount, List<Int64> stones*/)
        {
            Dictionary<Int64, Int64> newStones = new Dictionary<Int64, Int64>();
            //Console.WriteLine(tempStones.Count);
            foreach (var stoneGroup in stones)
            {
                string stoneName = stoneGroup.Key.ToString();
                if (stoneGroup.Key == 0)
                {
                    if (!newStones.TryAdd(1, stoneGroup.Value))
                    {
                        newStones[1] += stoneGroup.Value;
                    }
                }
                else if (stoneName.Length % 2 == 0 && stoneName.Length > 1)
                {
                    (Int64 stone1, Int64 stone2) = DivideStone(stoneName);
                    if (!newStones.TryAdd(stone1, stoneGroup.Value))
                    {
                        newStones[stone1] += stoneGroup.Value;
                    }
                    if (!newStones.TryAdd(stone2, stoneGroup.Value))
                    {
                        newStones[stone2] += stoneGroup.Value;
                    }
                }
                else
                {
                    Int64 tempValue = MultiplyBy2024(stoneGroup.Key);
                    if(!newStones.TryAdd(tempValue, stoneGroup.Value))
                    {
                        newStones[tempValue] += stoneGroup.Value;
                    }
                }
                stones.Remove(stoneGroup.Key);
            }
            foreach (var newGroup in newStones)
            {
                if (!stones.TryAdd(newGroup.Key, newGroup.Value))
                {
                    stones[newGroup.Key] += newGroup.Value;
                }
            }
            return stones;
        }

        (Int64 stone1, Int64 stone2) DivideStone(string stone)
        {
            int middle = stone.Length / 2;
            string secondStone = stone[middle..];
            secondStone = Int64.Parse(secondStone).ToString();
            return (Int64.Parse(stone[..middle]), Int64.Parse(secondStone));
        }

        Int64 MultiplyBy2024(Int64 stone)
        {
            Int64 newStone = stone * 2024;
            return newStone;
        }

        //Dictionary<Int64, Int64> AddNewStone (Int64 stone, Int64 count, Dictionary<Int64, Int64> newStones)
        //{
        //    if (!newStones.TryAdd(stone, count))
        //    {
        //        newStones[stone] += count;
        //    }
        //}

        //List<Int64> tempStones = new List<Int64>();
        Int64 result = 0; 
        for (int i = 0; i < blinkCount; i++)
        {

            //var tempStones = new List<Int64>() { stone };
            stones = blink(stones);
        }

        result = stones.Values.Sum();


        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        double ts = stopWatch.Elapsed.TotalMilliseconds;

        // Format and display the TimeSpan value.
        //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //    ts.Hours, ts.Minutes, ts.Seconds,
        //    ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + ts);
        //foreach (var calculation in calculationResults)
        //{
        //    Console.WriteLine(calculation);
        //}

        //foreach (var group in multipleStoneResults)
        //{
        //    Console.WriteLine(group);
        //}
        //Console.WriteLine(calculationResults.Count);
        //Console.WriteLine(multipleStoneResults.Count);
        Console.WriteLine(result);
    }
}