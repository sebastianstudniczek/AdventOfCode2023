namespace Runner.Days;

internal class Day7 : IExercise<int>
{
    private const char _Joker = 'J';

    public int Test(string[] input)
    {
        var comparer = new HandComparer();
        var sortedHands = input
            .Select(x => new HandBid(
                Hand: x.Split(' ')[0],
                Bid: int.Parse(x.Split(' ')[1]))
            )
            .OrderBy(x => x.Hand, comparer)
            .ToList();

        return sortedHands
            .Select((x, i) => x.Bid * (i + 1))
            .Sum();
    }

    private sealed class HandComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            else if (x == null)
            {
                return -1;
            }
            else if (y == null)
            {
                return 1;
            }

            int leftStrength = GetStrength(x);
            int rightStrength = GetStrength(y);

            if (leftStrength < rightStrength)
            {
                return -1;
            }
            else if (leftStrength > rightStrength)
            {
                return 1;
            }

            var cardComparer = new CardComparer();
            int cardCompareResult = 0;
            for (int i = 0; i < x.Length; i++)
            {
                cardCompareResult = cardComparer.Compare(x[i], y[i]);
                if (cardCompareResult != 0)
                {
                    break;
                }
            }

            return cardCompareResult;
        }

        private static int GetStrength(string input)
        {
            var grouped = input
                .GroupBy(x => x)
                .Select(x => new
                {
                    x.Key,
                    Count = x.Count()
                })
                .ToDictionary(x => x.Key, x => x.Count);

            foreach ((CheckType check, int strength) in TypeChecks.All)
            {
                if (check(grouped))
                {
                    return strength;
                }
            }

            return 1;
        }
    }

    private sealed class CardComparer : IComparer<char>
    {
        public int Compare(char x, char y)
        {
            int leftStrength = GetStrength(x);
            int rightStrength = GetStrength(y);

            if (leftStrength < rightStrength)
            {
                return -1;
            }
            else if (leftStrength > rightStrength)
            {
                return 1;
            }

            return 0;
        }

        private static int GetStrength(char x) => x switch
        {
            _Joker => 0,
            >= '2' and <= '9' => int.Parse(x.ToString()) - 1,
            'T' => 9,
            'Q' => 10,
            'K' => 11,
            'A' => 12,
            _ => -1
        };
    }

    private delegate bool CheckType(IDictionary<char, int> input);

    private static class TypeChecks 
    {
        public static readonly IEnumerable<(CheckType, int)> All = 
            [
                (IsFiveOfAKind, 7),
                (IsFourOfAKind, 6),
                (IsFullHouse, 5),
                (IsThreeOfAKind, 4),
                (IsTwoPair, 3),
                (IsOnePair, 2)
            ];

        private static bool IsFiveOfAKind(IDictionary<char, int> input) => 
                input.Count == 1 
            || (input.ContainsKey(_Joker) && input.Count == 2);

        private static bool IsFourOfAKind(IDictionary<char, int> input) =>
               (input.Count == 2 && input.Any(x => x.Value == 4))
            || (input.TryGetValue(_Joker, out int joker) && input.Count == 3 && (joker == 2 || input.Any(x => x.Value == 3)));

        private static bool IsFullHouse(IDictionary<char, int> input) =>
               (input.Count == 2 && input.Any(x => x.Value == 3) )
            || (input.ContainsKey(_Joker) && input.Count == 3);

        private static bool IsThreeOfAKind(IDictionary<char, int> input) =>
               (input.Count == 3 && input.Any(x => x.Value == 3))
            || (input.ContainsKey(_Joker) && input.Count == 4);

        private static bool IsTwoPair(IDictionary<char, int> input) => 
               (input.Count == 3 && input.Count(x => x.Value == 2) == 2)
            || (input.ContainsKey(_Joker) && input.Count == 4);

        private static bool IsOnePair(IDictionary<char, int> input) =>
                (input.Count == 4 && input.Count(x => x.Value == 2) == 1)
            || (input.ContainsKey(_Joker) && input.Count == 5);
    }

    private sealed record HandBid(string Hand, int Bid);
}