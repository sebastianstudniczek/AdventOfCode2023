using System.Text;

namespace Runner.Days;
// TODO: Not finished
// TODO: Think how to refactor it
// TODO: Check better solution
public class Day3 : IExercise<int>
{
    private readonly Coordinate[] _matrixOffsets =
    [
        new(-1,  1),
        new( 0,  1),
        new( 1,  1),
        new( 1,  0),
        new( 1, -1),
        new( 0, -1),
        new(-1, -1),
        new(-1,  0),
    ];
    
    public int Test(string[] input)
    {
        List<Coordinate> gearCoordinates = [];
        List<Coordinate> numberCoordinates = []; // A później podzielić to tam gdzienie ma ciągłości
        List<Coordinate> matchedNumberCoordinates = [];
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                char currentChar = input[i][j];
                if (char.IsDigit(currentChar))
                {
                    numberCoordinates.Add(new (j, i));
                } else if (currentChar == '*')
                {
                    gearCoordinates.Add(new(j, i));
                }
            }
        }
        
        // This check should be enhanced
        foreach (var coordinate in gearCoordinates)
        {
            int adjacentsCount = 0;
            List<Coordinate> temp = [];
            // Clockwise shift
            for (int i = 1; i < _matrixOffsets.Length; i++)
            {
                var current = coordinate + _matrixOffsets[i];
                var item = input[current.Y][current.X];
                if (char.IsDigit(item))
                {
                    var previous = coordinate + _matrixOffsets[i -1];
                    //var previousItem = input[]
                    if ((previous.Y != current.Y) 
                        || Math.Abs(previous.X - current.X) != 1)
                    {
                        adjacentsCount++;
                        temp.Add(current);
                    }
                }
            }

            if (adjacentsCount == 2) {
                matchedNumberCoordinates.AddRange(temp);
            }
        }

        // Build matched number
        List<List<Coordinate>> grouped = [];
        List<Coordinate> numberBuilder = [];
        for (int i = 0; i < numberCoordinates.Count; i++)
        {
            if (i > 0 && numberCoordinates[i].X - numberCoordinates[i - 1].X != 1)
            {
                bool isMatched = numberBuilder.Any(x => matchedNumberCoordinates.Contains(x));
                if (isMatched)
                {
                    grouped.Add(new List<Coordinate>(numberBuilder));
                }
                numberBuilder.Clear();
            } else if (i == numberCoordinates.Count - 1)
            {
                numberBuilder.Add(numberCoordinates[i]);
                bool isMatched = numberBuilder.Any(x => matchedNumberCoordinates.Contains(x));
                if (isMatched)
                {
                    grouped.Add(new List<Coordinate>(numberBuilder));
                }
            }
            numberBuilder.Add(numberCoordinates[i]);
        }

        List<int> numbers = [];
        var numBuilder = new StringBuilder();
        foreach (var coordinates in grouped)
        {
            foreach (var item in coordinates)
            {
                numBuilder.Append(input[item.Y][item.X]);
            }

            numbers.Add(int.Parse(numBuilder.ToString()));
            numBuilder.Clear();
        }

        return numbers.Sum();
    }

    private readonly record struct Coordinate(int X, int Y)
    {
        public static Coordinate operator +(Coordinate left, Coordinate right) =>
            new(left.X + right.X, left.Y + right.Y);
        
    }
}