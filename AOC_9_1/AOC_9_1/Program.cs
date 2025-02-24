static class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_9_1\AOC_9_1\AOC_9_1.txt";

        if (File.Exists(filePath))
        {
            var input = File.ReadAllText(filePath).ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

            var files = new List<int>();
            files = ParseInput(input);

            List<int> ParseInput(List<int> input)
            {
                int i = 0;

                while (input.Count > 0)
                {
                    files = AddRows(i, input[0]);
                    i++;
                    if (input.Count >= 2)
                    {
                        files = AddRows(-1, input[0]);
                    }
                }

                return files;
            }

            List<int> AddRows(int placeholder, int size)
            {
                for (int i = 0; i < size; i++)
                {
                    files.Add(placeholder);
                }
                input.RemoveAt(0);
                return files;
            }

            List<int> CompressFile(List<int> files)
            {
                var compressed = new List<int>();

                int left = 0; int right = files.Count - 1;

                while (left < right)
                {
                    if (files[right] < 0)
                    {
                        right--;
                    }
                    else if (files[left] < 0)
                    {
                        compressed.Add(files[right]);
                        left++; right--;
                    }
                    else
                    {
                        compressed.Add(files[left]);
                        left++;
                    }
                }

                return compressed;
            }

            List<int> compressed = new List<int>(CompressFile(files));
            Int64 result = 0;

            for (int i = 0; i < compressed.Count; i++)
            {
                result += compressed[i] * i;
            }

            Console.WriteLine(result);
        }
    }
}