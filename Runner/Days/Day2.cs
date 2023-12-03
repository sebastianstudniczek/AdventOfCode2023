namespace Runner.Days;

public class Day2 : IExercise<int>
{
    public int Test(string[] input)
    {
        int cubesPower = 0;
        var countByColor = new Dictionary<string, int>
        {
            { Color.Red, 0 },
            { Color.Green, 0 },
            { Color.Blue, 0 }
        };
        for (int i = 0; i < input.Length; i++)
        {
            
            string currentGame = input[i];
            var cubeSets = currentGame.Split(':')[1].Split(';');

            for (int j = 0; j < cubeSets.Length; j++)
            {
                var currentSet = cubeSets[j].Split(',', StringSplitOptions.TrimEntries);
                for (int k = 0; k < currentSet.Length; k++)
                {
                    var cubePair = currentSet[k].Split(' ');
                    int cubeCount = int.Parse(cubePair[0]);
                    string color = cubePair[1];
                    if (cubeCount > countByColor[color])
                    {
                        countByColor[color] = cubeCount;
                    }
                }
            }

            cubesPower +=
                countByColor[Color.Red] *
                countByColor[Color.Green] *
                countByColor[Color.Blue];
            
            countByColor[Color.Red] = 0;
            countByColor[Color.Green] = 0;
            countByColor[Color.Blue] = 0;
        }

        return cubesPower;
    }

    private static class Color
    {
        public const string Red = "red";
        public const string Green = "green";
        public const string Blue = "blue";
    } 
}