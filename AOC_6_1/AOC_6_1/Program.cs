void main()
{
    string filePath = @"C:\Users\paola\advent_of_code\AOC_6_1\AOC_6_1\AOC_6_1.txt";

    if (File.Exists(filePath))
    {
        string[] input = File.ReadAllLines(filePath);
        List<List<char>> map = new List<List<char>>();
        foreach (string line in input)
        {
            map.Add(line.ToList());
        }

        List<List<int>> visited = new List<List<int>>();

        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == '^')
                {
                    visited.Add(new List<int> { i, j });
                }
            }
        }

        List<List<int>> directions = new List<List<int>>()
        {
            new List<int>(){-1, 0},
            new List<int>(){0, 1},
            new List<int>(){1, 0},
            new List<int>(){0, -1}
        };

        visited = moveGuard(map, visited, directions);
        
        int result = visited.Count;

        Console.WriteLine(result);
    }
}

List<List<int>> moveGuard(List<List<char>> map, List<List<int>> visited, List<List<int>> directions) 
{
    bool guardPresent = true;
    int directionIndex = 0; 
    int locationIndex = 0;
    while (guardPresent)
    {
        int tmpY = visited[locationIndex][0] + directions[directionIndex][0];
        int tmpX = visited[locationIndex][1] + directions[directionIndex][1];

        if (Enumerable.Range(0, map.Count).Contains(tmpY) && Enumerable.Range(0, map[0].Count).Contains(tmpX)) 
        {
            if (map[tmpY][tmpX] == '#') 
            {
                directionIndex = changeDirection(directions, directionIndex);
            } 
            else 
            {
                int tmpIndex = findIndex(visited, tmpY, tmpX);
                if (tmpIndex != -1) 
                {
                    locationIndex = tmpIndex;
                } 
                else 
                {
                    locationIndex = visited.Count; 
                    visited.Add(new List<int>() { tmpY, tmpX});
                }
            }
        }
        else { guardPresent = false; }
    }
    return visited; 
}

int findIndex(List<List<int>> visited, int y, int x)
{
    for (int i = 0; i < visited.Count; i++)
    {
        if (visited[i][0] == y && visited[i][1] == x)
        {
            return i;
        }
    }
    return -1;
}

int changeDirection(List<List<int>> directions, int currentDirection)
{
    if (currentDirection == (directions.Count-1))
    {
        return 0;
    }
    else
    {
        currentDirection++;
        return currentDirection;
    }
}

main();