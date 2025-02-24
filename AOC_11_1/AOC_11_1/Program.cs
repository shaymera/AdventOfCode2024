public static class Program
{
    public static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_11_1\AOC_11_1\AOC_11_1.txt";

        int blinkCount = 25;
        List<string> stones = File.ReadAllText(filePath).Split(' ').ToList();

        int blink(string stone, int blinkCount)
        {
            //Console.WriteLine(blinkCount);
            if (blinkCount == 0)
            {
                return 1;
            }
            else
            {
                int stoneCount = 0;
                if (stone == "0")
                {
                    stone = "1";
                    //Console.WriteLine(stone);
                    stoneCount += blink(stone, blinkCount - 1);
                }
                else if (stone.Length % 2 == 0 && stone.Length > 1)
                {
                    (string stone1, string stone2) = DivideStone(stone);
                    //Console.WriteLine(stone1 + " | " + stone2);
                    stoneCount += blink(stone1, blinkCount - 1);
                    stoneCount += blink(stone2, blinkCount - 1);
                }
                else
                {
                    stone = MultiplyBy2024(stone);
                    //Console.WriteLine(stone);
                    stoneCount += blink(stone, blinkCount - 1);
                }
                return stoneCount;
            }
        }

        (string stone1, string stone2) DivideStone(string stone)
        {
            int middle = stone.Length / 2;
            string secondStone = stone[middle..];
            secondStone = Int64.Parse(secondStone).ToString();
            return (stone[..middle], secondStone);
        }

        string MultiplyBy2024(string stone)
        {
            Int64 currentNumber = Int64.Parse(stone);
            Int64 bigNumber = currentNumber * 2024;
            return bigNumber.ToString();
        }

        Int64 result = 0;
        for (int i = 0; i < stones.Count; i++)
        {
             result += blink(stones[i], blinkCount);
        }

        Console.WriteLine(result);
    }
}