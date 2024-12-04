void main()
{
    String filePath = @"C:\Users\paola\advent_of_code\AOC_4_2\AOC_4_2\AOC_4_2.txt";

    if (File.Exists(filePath))
    {
        String[] fileRows = File.ReadAllLines(filePath);

        char[][] puzzle = new char[fileRows.Length][];

        for (int i = 0; i < fileRows.Length; i++)
        {
            puzzle[i] = fileRows[i].ToCharArray();
        }

        int xmasCount = findXmas(puzzle);

        Console.WriteLine(xmasCount);
    }
}

int findXmas(char[][] puzzle)
{
    int[][] directions = new int[][]
    {
        new int[] { -1, -1 },
        new int[] { -1, 1 },
        new int[] { 1, -1 },
        new int[] { 1, 1 },
    };

    char[][] charSets = new char[][] 
    {
        new char[] {'M', 'M', 'S', 'S' },
        new char[] {'M', 'S', 'M', 'S'},
        new char[] { 'S', 'M', 'S', 'M'},
        new char[] { 'S', 'S', 'M', 'M'}
    };

    int result = 0;

    for (int i = 0; i < puzzle.Length; i++)
    {
        for (int j = 0; j < puzzle[i].Length; j++)
        {
            if (puzzle[i][j] == 'A')
            {
                foreach (char[] charSet in charSets)
                {
                    if (isMatch(i, j, directions, puzzle, charSet)) 
                    { 
                        result++;
                    }
                }
            }
        }
    }

    return result;
}

bool isMatch(int i, int j, int[][] directions, char[][] puzzle, char[] pattern)
{
    int result = 0;
    for (int k = 0; k < pattern.Length; k++)
    {
        //for each direction check if it matches the letter, if this occurs for all instances of k, we have a full match
        int newi = i + directions[k][0];
        int newj = j + directions[k][1];

        if (Enumerable.Range(0, puzzle.Length).Contains(newj) && Enumerable.Range(0, puzzle[0].Length).Contains(newi))
        {
            if (puzzle[newi][newj] == pattern[k]) { result++; }
        } else { return false; }
    }
    if (result == pattern.Length) {  return true; }
    return false;
}

main();