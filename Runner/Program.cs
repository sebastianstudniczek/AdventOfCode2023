using Runner.Days;


var day = new Day11();

Console.WriteLine("Running app");
string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day11)}.txt";
string[] input = File.ReadAllLines(filePath);
var result = day.Test(input);
Console.WriteLine(result);