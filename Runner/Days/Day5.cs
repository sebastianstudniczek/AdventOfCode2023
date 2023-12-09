using System.Collections.Concurrent;

namespace Runner.Days;

// TODO: How to speed this up?
internal class Day5
{
    public uint Test(string[] input)
    {
        string[] seedsLine = input[0]
            .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        List<List<uint>> seeds = [];
        for (int i = 0; i < seedsLine.Length; i += 2)
        {
            var seedsRange = Range(uint.Parse(seedsLine[i]), uint.Parse(seedsLine[i + 1]));
            seeds.Add(seedsRange.ToList());
        }

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

        ConcurrentBag<uint> locations = [];
        var chunked = seeds.SelectMany(x => x).Chunk(10_000);
        Parallel.ForEach(chunked, (seed) =>
        {
            var result = GetLocation(seed, maps);
            locations.Add(result);
        });

        return locations.Min();
    }

    private static uint GetLocation(IList<uint> seedsChunk, IEnumerable<List<GardenMap>> maps)
    {
        uint minLocation = uint.MaxValue;
        for (int i = 0; i < seedsChunk.Count; i++)
        {
            uint init = seedsChunk[i];
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

    private static IEnumerable<uint> Range(uint start, uint count)
    {
        List<uint> result = [];
        uint index = start;
        while (index < start + count)
        {
            result.Add(index++);
        }

        return result;
    }

    private sealed record GardenMap(uint DestRangeStart, uint SrcRangeStart, uint RangeLength);
}
