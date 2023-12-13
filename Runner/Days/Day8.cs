namespace Runner.Days;

// Binary tree?
// or some other tree?
// with recursion?
internal class Day8 : IExercise<int>
{
    public int Test(string[] input)
    {
        char[] instructions = input[0].ToCharArray();
        Dictionary<string, Direction> nodes = [];

        for (int i = 2; i < input.Length; i++)
        {
            var splitted = input[i].Split('=', StringSplitOptions.TrimEntries);
            string root = splitted[0];
            var directions = splitted[1]
                .TrimStart('(')
                .TrimEnd(')')
                .Split(',', StringSplitOptions.TrimEntries);

            nodes.Add(root, new(directions[0], directions[1]));
        }
        
        int stepsCount = 0;

        string[] currentNodes = nodes
            .Where(x => x.Key.EndsWith('A'))
            .Select(x => x.Key)
            .ToArray();

        var temp = new int[currentNodes.Length];

        for (int i = 0; i < currentNodes.Length; i++)
        {
            Console.WriteLine(currentNodes[i]);
        }

        int counter = 0;
        // Some LCM?
        for (int i = 0; i < currentNodes.Length; i++)
        {
            for (int j = 0; j < instructions.Length; j++)
            {
                string currentNode = currentNodes[i];
                var currentDirection = nodes[currentNode];
                currentNode = instructions[j] == 'L' ? currentDirection.Left : currentDirection.Right;
                currentNodes[i] = currentNode;
                counter++;

                if (currentNode.EndsWith('Z'))
                {
                    temp[i] = counter;
                }
            }

            counter = 0;
        }

        return stepsCount;
    }
}

internal readonly record struct Direction(string Left, string Right);