namespace Runner.Days;

internal class Day6 : IExercise<int>
{
    public int Test(string[] input)
    {
        var getValues = (string input) => input
            .Split(':', StringSplitOptions.TrimEntries)[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);

        var times = getValues(input[0]).ToArray();
        var distances = getValues(input[1]).ToArray();

        List<int> ways = [];
        int localCount = 0;
        for (int i = 0; i < times.Length; i++)
        {
            int currentRecord = distances[i];
            for (int j = 0; j < times[i]; j++)
            {
                int currentDistance = (times[i] - j) * j;
                if (currentDistance > currentRecord)
                {
                    localCount++;
                }
            }

            ways.Add(localCount);
            localCount = 0;
        }


        return ways.Aggregate((cur, x) => cur * x);
    }
}
