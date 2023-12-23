using System.Numerics;
using static Runner.Extensions;

namespace Runner.Days;

public class Day11 : IExercise<int>
{
    private const char _GalaxyTag = '#';

    public int Test(string[] input)
    {
        var expandVertically = Curry<char, string[], List<List<char>>>(ExpandVertically);
        var expandHorizontally = Curry<char, List<List<char>>, List<List<char>>>(ExpandHorizontally);
        var expandUniverse = Compose(
            expandVertically(_GalaxyTag),
            Rotate,
            expandHorizontally(_GalaxyTag),
            Rotate);

        var expandedUniverse = expandUniverse(input);

        int counter = 0;
        List<int> signs = [];
        Dictionary<int, Vector2> vectorBySign = [];

        for (int i = 0; i < expandedUniverse.Count; i++)
        {
            for (int j = 0; j < expandedUniverse[0].Count; j++)
            {
                if (expandedUniverse[i][j] == _GalaxyTag)
                {
                    var sign = Convert.ToChar(counter);
                    vectorBySign.Add(counter, new(i, j));
                    expandedUniverse[i][j] = sign;
                    signs.Add(sign);
                    counter++;
                }
            }
        }

        var calculateShortestPaths = Curry<Dictionary<int, Vector2>, List<(int From, int To)>, int>(CalculateShortestPaths);
        var getShortestPathsLength = Compose<IEnumerable<int>, List<(int From, int To)>, int>(
            GetCombinationsToCheck,
            calculateShortestPaths(vectorBySign));

        return getShortestPathsLength(signs);
    }

    private int CalculateShortestPaths(Dictionary<int, Vector2> vectorBySign, List<(int From, int To)> combinations)
    {
        var globalPathLength = 0;

        foreach ((int From, int To) in combinations)
        {
            var fromVector = vectorBySign[From];
            var toVector = vectorBySign[To];
            var pathLenght =
                Math.Abs(fromVector.X - toVector.X) +
                Math.Abs(fromVector.Y - toVector.Y);

            globalPathLength += (int)pathLenght;
        }

        return globalPathLength;
    }

    private static List<List<char>> ExpandVertically(char galaxySign, string[] input)
    {
        List<List<char>> galaxy = [];
        for (int i = 0; i < input.Length; i++)
        {
            string currentLine = input[i];

            galaxy.Add([.. currentLine.ToCharArray()]);
            if (!currentLine.Contains(galaxySign))
            {
                galaxy.Add([.. currentLine.ToCharArray()]);
            }
        }

        return galaxy;
    }

    private static List<List<char>> ExpandHorizontally(char galaxySign, List<List<char>> input)
    {
        List<List<char>> newResult = [];
        for (int i = 0; i < input.Count; i++)
        {
            var currentLine = input[i];
            newResult.Add(currentLine);
            if (!currentLine.Contains(galaxySign))
            {
                newResult.Add(currentLine);
            }
        }

        return newResult;
    }

    private static List<(int From, int To)> GetCombinationsToCheck(IEnumerable<int> input)
    {
        List<(int, int)> output = [];
        var temp = input.ToList();
        foreach (var item in temp)
        {
            foreach (var nest in temp)
            {
                if (item != nest && !(output.Contains((item, nest)) || output.Contains((nest, item))))
                {
                    output.Add((item, nest));
                }
            }
        }

        return output;
    }

    private static List<List<char>> Rotate(List<List<char>> source)
    {
        List<List<char>> temp = [];

        for (int i = 0; i < source[0].Count; i++)
        {
            List<char> innerTemp = [];
            for (int j = 0; j < source.Count; j++)
            {
                innerTemp.Add(source[j][i]);
            }
            temp.Add([.. innerTemp]);
            innerTemp.Clear();
        }

        return temp;
    }

    private static void Print(List<List<char>> source)
    {
        for (int i = 0; i < source.Count; i++)
        {
            for (int j = 0; j < source[i].Count; j++)
            {
                Console.Write(source[i][j]);
            }
            Console.WriteLine();
        }
    }
}