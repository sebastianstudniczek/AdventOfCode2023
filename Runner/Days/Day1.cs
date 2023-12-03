namespace Runner.Days;

// Trebuchet
// https://adventofcode.com/2023/day/1
// Calibration value - first and last digit (two digit number)
public class Day1 : IExercise<int>
{
    private readonly Dictionary<string, string> _digitByString = new()
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };

    public int Test(string[] input)
    {
        int sum = 0;
        var valuesToCheck = _digitByString.Values
            .Union(_digitByString.Keys)
            .ToArray();
        
        foreach (var line in input)
        {
            string[] coordinates = GetCoordinates(line, valuesToCheck);
            string toParse = $"{coordinates[0]}{coordinates[1]}";
            sum += int.Parse(toParse);
        }

        return sum;
    }

    private string[] GetCoordinates(string line, IEnumerable<string> valuesToCheck)
    {
        var valueByIndex = new SortedDictionary<int, string>();
        foreach (var digit in valuesToCheck)
        {
            int startingIndex = 0;
            while (startingIndex >= 0)
            {
                int indexFound = line.IndexOf(digit, startingIndex);
                if (indexFound < 0)
                {
                    break;
                }

                valueByIndex.Add(indexFound, !int.TryParse(digit, out int _) ? _digitByString[digit] : digit);
                startingIndex = indexFound + 1;
            }
        }

        return new[] { valueByIndex.Values.First(), valueByIndex.Values.Last()};
    }
}