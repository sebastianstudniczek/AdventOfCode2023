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

            int toRemove = 0;
            for (int i = mainInterpolations.Count - 1; i > 0; i--)
            {
                toRemove = mainInterpolations[i][0] - toRemove;
            }

            int final = mainInterpolations[0][0] - toRemove;

            sum += final;
        }

        return sum;
    }
}