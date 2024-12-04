using System.Runtime.CompilerServices;

void main()
{
    String filePath = @"C:\Users\paola\advent_of_code\AOC_4_1\AOC_4_1\AOC_4_1.txt";

    if (File.Exists(filePath))
    {
        String[] fileRows = File.ReadAllLines(filePath);

        String word = "XMAS";

        char[][] puzzle = new char[fileRows.Length][];

        for (int i = 0; i < fileRows.Length; i++)
        {
            puzzle[i] = fileRows[i].ToCharArray();
        }

        int xmasCount = findXmas(puzzle, word);

        Console.WriteLine(xmasCount);
    }
}

int findXmas(char[][] puzzle, String word)
{
    int[][] directions = new int[][]
        {
            new int[]{ -1, -1 },
            new int[]{ -1,  0 },
            new int[]{ -1,  1 },
            new int[]{ 0, -1 },
            new int[]{ 0,  1 },
            new int[]{ 1, -1 },
            new int[]{ 1,  0 },
            new int[]{ 1,  1 }
        };
    
    int result = 0;

    for (int i = 0; i < puzzle.Length; i++)
    {
        for (int j = 0; j < puzzle[i].Length; j++)
        {
            if (puzzle[i][j] == word[0])
            {
                foreach (int[] directionsSet in directions)
                {
                    if (isMatch(i, j, directionsSet, puzzle, word)) {  result++; }
                }
            }
        }
    }

    return result;
}

bool isMatch(int i, int j,  int[] directions, char[][] puzzle, String word)
{
    for (int k = 1; k < word.Length; k++)
    {
        int newi = i + k * directions[0];
        int newj = j + k * directions[1];

        if (Enumerable.Range(0, puzzle.Length).Contains(newj) && Enumerable.Range(0, puzzle[0].Length).Contains(newi))
        {
            if (puzzle[newi][newj] != word[k]) {  return false; }
        } else { return false; }
    }

    return true;
}

main();