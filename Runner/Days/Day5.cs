namespace Runner.Days;

internal class Day5 : IExercise<uint>
{
    public uint Test(string[] input)
    {
        string[] seeds = input[0]
            .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        List<List<GardenMap>> maps = [];
        List<GardenMap> tempMap = [];
        int currentMap = 0;

        for (int i = 3; i < input.Length; i++)
        {
            string currentLine = input[i];
            if (string.IsNullOrWhiteSpace(currentLine) || i == input.Length - 1)
            {
                var sortedBySrc = tempMap
                    .OrderBy(x => x.SrcRangeStart)
                    .ToList();

                maps.Add(sortedBySrc);
                tempMap.Clear();
                currentMap++;
                i++;
                continue;
            }

            var values = currentLine
                .Split(' ')
                .Select(uint.Parse)
                .ToArray();

            GardenMap localMap = new(values[0], values[1], values[2]);
            tempMap.Add(localMap);
        }


        uint minLocation = int.MaxValue;
        for (int i = 0; i < seeds.Length; i++)
        {
            uint init = uint.Parse(seeds[i]);
            foreach (var filter in maps)
            {
                var fit = filter.Find(x => x.SrcRangeStart <= init && x.SrcRangeStart + x.RangeLength > init);
                if (fit is null)
                {
                    continue;
                }

                init += fit.DestRangeStart - fit.SrcRangeStart;
            }

            if (init < minLocation)
            {
                minLocation = init;
            }
        }


        return minLocation;
    }

    private sealed record GardenMap(uint DestRangeStart, uint SrcRangeStart, uint RangeLength);
}
