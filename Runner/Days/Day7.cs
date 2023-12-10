namespace Runner.Days;

internal class Day7 : IExercise<int>
{
    public int Test(string[] input)
    {
        var comparer = new HandComparer();
        var sortedHands = input
            .Select(x => new
            {
                Hand = x.Split(' ')[0],
                Bid = int.Parse(x.Split(' ')[1])
            })
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

            if (TypeChecks.IsFiveOfAKind(grouped))
            {
                return 7;
            }
            else if (TypeChecks.IsFourOfAKind(grouped))
            {
                return 6;
            }
            else if (TypeChecks.IsFullHouse(grouped))
            {
                return 5;
            }
            else if (TypeChecks.IsThreeOfAKind(grouped))
            {
                return 4;
            }
            else if (TypeChecks.IsTwoPair(grouped))
            {
                return 3;
            }
            else if (TypeChecks.IsOnePair(grouped))
            {
                return 2;
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
            } else if (leftStrength > rightStrength)
            {
                return 1;
            }

            return 0;
        }

        private static int GetStrength(char x) => x switch
        {
             >= '2' and <= '9' => int.Parse(x.ToString()) - 1,
            'T' => 9,
            'J' => 10,
            'Q' => 11,
            'K' => 12,
            'A' => 13,
            _ => 0
        };
    }

    private static class TypeChecks
    {
        public static bool IsFiveOfAKind(IDictionary<char, int> input) => 
            input.Count == 1;

        public static bool IsFourOfAKind(IDictionary<char, int> input) =>
            input.Count == 2 && input.Any(x => x.Value == 4);

        public static bool IsFullHouse(IDictionary<char, int> input) =>
            input.Count == 2 && input.Any(x => x.Value == 3);

        public static bool IsThreeOfAKind(IDictionary<char, int> input) =>
            input.Count == 3 && input.Any(x => x.Value == 3);

        public static bool IsTwoPair(IDictionary<char, int> input) =>
            input.Count == 3 && input.Count(x => x.Value == 2) == 2;

        public static bool IsOnePair(IDictionary<char, int> input) =>
            input.Count == 4 && input.Count(x => x.Value == 2) == 1;
    }
}