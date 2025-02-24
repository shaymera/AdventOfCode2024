using System.ComponentModel.Design;

public record Item(int index, int size);
static class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\Paola\Code\adventofcode2024\AOC_9_2\AOC_9_2\AOC_9_2.txt";

        if (File.Exists(filePath))
        {
            var input = File.ReadAllText(filePath).ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

            var files = new List<Item>();
            files = ParseInput(input);
            
            List<Item> ParseInput(List<int> input)
            {
                int index = 0;
                
                for (int i = 0; i < input.Count; i = i + 2)
                {
                    files.Add(new Item(index, input[i]));
                    index++;
                    if ((i + 1) < input.Count)
                    {
                        files.Add(new Item(-1, input[i + 1]));
                    }
                }

                return files;
            }

            List<Item> CompressFile(List<Item> files)
            {
                var compressed = new List<Item>(files);
                for (int i = compressed.Count - 1; i >= 0; i--)
                {
                    if (compressed[i].index >= 0)
                    {
                        compressed = FindSpace(compressed, i);
                    }
                }
                return compressed;
            }

            List<Item> FindSpace(List<Item> compressed, int index)
            {
                for (int i = 0; i < index; i++)
                {
                    int remainingSpace = compressed[i].size - compressed[index].size;
                    if (compressed[i].index < 0 && remainingSpace >= 0)
                    {
                        compressed[i] = compressed[index];
                        Item temp = new (-1, compressed[index].size);
                        compressed[index] = temp;
                        if (remainingSpace > 0)
                        {
                            compressed.Insert(i + 1, new (-1, remainingSpace));
                        }
                        return compressed;
                    }
                }
                return compressed;
            }

            List<int> FormatCompression(List<Item> compressed)
            {
                var final = new List<int>();
                foreach (var file in compressed)
                {
                    for (int i = 0; i <  file.size; i++)
                    {
                        final.Add(file.index);
                    }
                }
                return final;
            }

            var compressed = new List<Item>(CompressFile(files));
            var formatted = new List<int>(FormatCompression(compressed));

            Int64 result = 0;

            for (int i = 0; i < formatted.Count; i++)
            {
                if (formatted[i] >= 0)
                {
                    result += formatted[i] * i;
                }
            }

            Console.WriteLine(result);
        } 
        else
        {
            Console.WriteLine("error: can't find file!");
        }
    }
}