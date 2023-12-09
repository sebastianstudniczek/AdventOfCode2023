namespace Runner.Days;

public class Day4 : IExercise<int>
{
    public int Test(string[] input)
    {
        Dictionary<int, int> matchesByCardId = [];
        Dictionary<int, int> countByCardId = Enumerable
            .Range(1, input.Length)
            .ToDictionary(x => x, _ => 1);

        for (int i = 0; i < input.Length; i++)
        {
            var getNumbers = (string input) => input
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var splitted = input[i].Split(':');
            string cardId = splitted[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
            var numbers = splitted[1].Trim().Split('|');
            var winningNumbers = getNumbers(numbers[0]);
            var myNumbers = getNumbers(numbers[1]);
            int matches = winningNumbers.Count(myNumbers.Contains);
            int cardNumber = int.Parse(cardId);

            matchesByCardId.Add(cardNumber, matches);
        }

        foreach (var item in matchesByCardId)
        {
            int copies = countByCardId[item.Key];
            int start = item.Key + 1;
            for (int j = start; j < start + item.Value; j++)
            {
                countByCardId[j] += copies;
            }
        }

        return countByCardId.Values.Sum();
    }
}
