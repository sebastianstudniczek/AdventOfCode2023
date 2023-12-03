namespace Runner.Days;

// Constraints: 12 red, 13 green, 14 blue
// 2239
public class Day2 : IExercise<int>
{
    private readonly Dictionary<string, int> _maxCountByColor = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };
    
    public int Test(string[] input)
    {
        int possibleGameIdsSum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string currentGame = input[i];
            var splitted = currentGame.Split(':');
            int gameId = int.Parse(splitted[0].Split(' ')[1]);
            var cubeSets = splitted[1].Split(';');
            
            bool isGamePossible = true;
            int j = 0;
            while (isGamePossible && j < cubeSets.Length)
            {
                var currentSet = cubeSets[j].Split(',', StringSplitOptions.TrimEntries);
                for (int k = 0; k < currentSet.Length; k++)
                {
                    var spl = currentSet[k].Split(' ');
                    int cubeCount = int.Parse(spl[0]);
                    string color = spl[1];
                    if (cubeCount > _maxCountByColor[color])
                    {
                        isGamePossible = false;
                        break;
                    }
                }
                j++;
            }

            if (isGamePossible)
            {
                possibleGameIdsSum += gameId;
            }
        }

        return possibleGameIdsSum;
    }
}