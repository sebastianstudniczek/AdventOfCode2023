namespace Runner.Days;

internal class Day6 : IExercise<long>
{
    public long Test(string[] input)
    {
        var getValues = (string input) =>
        {
            var temp = input
                .Split(':', StringSplitOptions.TrimEntries)[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return long.Parse(string.Concat(temp));
        };

        long time = getValues(input[0]);
        long recordDistance = getValues(input[1]);

        long waysCount = 0;
        for (long j = 0; j < time; j++)
        {
            long currentDistance = (time - j) * j;
            if (currentDistance > recordDistance)
            {
                waysCount++;
            }
        }

        return waysCount;
    }
}
