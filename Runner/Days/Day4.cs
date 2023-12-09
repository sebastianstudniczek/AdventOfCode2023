namespace Runner.Days;

public class Day4 : IExercise<int>
{
    public int Test(string[] input)
    {
        int sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var getNumbers = (string input) => input
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var numbers = input[i].Split(':')[1].Trim().Split('|');
            var winningNumbers = getNumbers(numbers[0]);
            var myNumbers = getNumbers(numbers[1]).ToHashSet();
            int count = winningNumbers.Count(myNumbers.Contains);
            if (count == 0)
            {
                continue;
            }
            sum += (int)Math.Pow(2, count - 1);
        }

        return sum;
    }
}
