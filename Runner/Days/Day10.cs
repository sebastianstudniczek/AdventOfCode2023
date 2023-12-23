using System.Numerics;
using System.Reflection;

namespace Runner.Days;

// TEMP
public class Day10 : IExercise<int>
{
    private const char _Start = 'S';
    private readonly Dictionary<char, Vector2[]> _offsetsBySign = new() {
        { Pipe.NS, [ Direction.North, Direction.South ] },
        { Pipe.WE, [ Direction.West, Direction.East ] },
        { Pipe.SW, [ Direction.East, Direction.North ] },
        { Pipe.NW, [ Direction.South, Direction.East ] },
        { Pipe.NE, [ Direction.East, Direction.South ] },
        { Pipe.SE, [ Direction.North, Direction.West ] }
    };

    public int Test(string[] input)
    {
        Vector2 startingPoint = new();
        // Find starting point
        bool foundStart = false;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == _Start)
                {
                    startingPoint = new Vector2(j, i);
                    foundStart = true;
                    break;
                }
            }

            if (foundStart)
            {
                break;
            }
        }

        var previous = startingPoint;
        char sign = ' ';
        Vector2 current = new();
        foreach (var offset in Direction.GetOffsets())
        {
            current = previous + offset;
            if (current.X < 0 || current.Y < 0)
            {
                continue;
            }
            sign = input[(int)current.Y][(int)current.X];
            if (sign.In(Pipe.GetAll()) && offset.In(_offsetsBySign[sign]))
            {
                break;
            }
        }

        int distance = 1;
        Vector2 toAdd = new();
        char previousSign = input[(int)previous.Y][(int)previous.X];
        List<(char PreviousSign, Vector2 Current, char Sign)> path = [(previousSign, current, sign)];

        while (current != startingPoint)
        {
            distance++;
            sign = input[(int)current.Y][(int)current.X];
            switch (sign)
            {
                case Pipe.NS:
                    toAdd = current.Y > previous.Y
                        ? Direction.South
                        : Direction.North;
                    break;
                case Pipe.WE:
                    toAdd = current.X > previous.X
                        ? Direction.East
                        : Direction.West;
                    break;
                case Pipe.NE:
                    toAdd = current.Y != previous.Y
                        ? Direction.East
                        : Direction.North;
                    break;
                case Pipe.NW:
                    toAdd = current.Y != previous.Y
                        ? Direction.West
                        : Direction.North;
                    break;
                case Pipe.SW:
                    toAdd = current.X != previous.X
                        ? Direction.South
                        : Direction.West;
                    break;
                case Pipe.SE:
                    toAdd = current.X != previous.X
                        ? Direction.South
                        : Direction.East;
                    break;
            }

            previous = current;
            current += toAdd;
            previousSign = input[(int)previous.Y][(int)previous.X];
            path.Add((previousSign, current, sign));
        }

        // find square
        char[] breakablePipes = [
            Pipe.NW,
            Pipe.SW,
            Pipe.SE,
            Pipe.NE];

        (char Previous, Vector2 Current, char Sign) firstBreak = (new(), new(), ' ');
        for (int i = 0; i < path.Count; i++)
        {
            var currentSign = path[i].Sign;
            previousSign = path[i - 1].Sign;

            if (currentSign.In(breakablePipes)) {
                firstBreak = (previousSign, path[i].Current, path[i].Sign);
            }
        }

        var test = path
            .Where(x => x.Sign.In(breakablePipes))
            .ToList();

        char nextToSearch = firstBreak switch
        {
            // J -> 7 -> F
            { Previous: Pipe.SE, Sign: Pipe.SW } => Pipe.NW,
            // F -> 7 -> J
            { Previous: Pipe.NE, Sign: Pipe.SW } => Pipe.NW,

            // F -> L -> J
            { Previous: Pipe.SE, Sign: Pipe.NE } => Pipe.NW,
            // J -> L -> F
            { Previous: Pipe.NW, Sign: Pipe.NE } => Pipe.SE,

            // 7 -> F -> L
            { Previous: Pipe.SW, Sign: Pipe.SE } => Pipe.NE,
            // L -> F -> 7
            { Previous: Pipe.NE, Sign: Pipe.SE } => Pipe.SW,

            // 7 -> J -> L
            { Previous: Pipe.SW, Sign: Pipe.NW } => Pipe.NE,
            // L -> J -> 7
            { Previous: Pipe.NE, Sign: Pipe.NW } => Pipe.SW,

            _ => 'N'
        };
        

        //Console.Clear();
        //Console.SetCursorPosition((int)startingPoint.X, (int)startingPoint.Y);
        //Console.Write('S');
        //// TODO: How to crack this with breakable points?
        //path.Where(vector =>
        //    {
        //        char sign = input[(int)vector.Y][(int)vector.X];
        //        return sign.In(breakablePipes);
        //    })
        //    .Do(vector =>
        //    {
        //        Console.SetCursorPosition((int)vector.X, (int)vector.Y);
        //        char sign = input[(int)vector.Y][(int)vector.X];
        //        Console.Write(sign);
        //    });

        //for (int i = 0; i < path.Count; i++)
        //{
        //    var currentNode = path[i];
        //    char currentSign = input[(int)currentNode.Y][(int)currentNode.X];

        //    if (currentSign.In(breakablePipes))
        //    {

        //    }
        //}
        Console.SetCursorPosition(0, 20);

        return distance / 2;
    }

    public static class Pipe
    {
        public const char NS = '|';
        public const char WE = '-';
        public const char NE = 'L';
        public const char NW = 'J';
        public const char SW = '7';
        public const char SE = 'F';

        public static char[] GetAll() => typeof(Pipe)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(x => x.GetValue(null))
            .Cast<char>()
            .ToArray();
    }

    public sealed class Direction
    {
        private static readonly Dictionary<int, Direction> _innerMap = [];

        public static readonly Direction North = new(0, new(0, -1));
        public static readonly Direction East = new(1, new(1, 0));
        public static readonly Direction South = new(2, new(0, 1));
        public static readonly Direction West = new(3, new(-1, 0));

        public int Value { get; }
        public Vector2 Vector { get; }

        private Direction(int value, Vector2 vector)
        {
            Value = value;
            Vector = vector;
            _innerMap[value] = this;
        }

        public static Vector2[] GetOffsets() =>
            _innerMap.Values.Select(x => x.Vector).ToArray();

        public static implicit operator Vector2(Direction dir) => dir.Vector;
    }
}