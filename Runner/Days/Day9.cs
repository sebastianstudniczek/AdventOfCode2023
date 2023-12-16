namespace Runner.Days;

internal class Day9 : IExercise<int>
{
    public int Test(string[] input)
    {
        int sum = 0;

        for (int j = 0; j < input.Length; j++)
        {
            int[] history = input[j]
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            List<List<int>> mainInterpolations = [[..history]];
            List<int> temp = [];
            while (mainInterpolations[^1].Any(x => x != 0))
            {
                for (int i = 1; i < mainInterpolations[^1].Count; i++)
                {
                    temp.Add(mainInterpolations[^1][i] - mainInterpolations[^1][i - 1]);
                }
                mainInterpolations.Add(new(temp));
                temp.Clear();
            }

            // This might not be accurate in every case
            int final = mainInterpolations
                .Select(x => x[^1])
                .Sum();

            sum += final;
        }

        return sum;
    }
}