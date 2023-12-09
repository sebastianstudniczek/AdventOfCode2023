using Runner.Days;

var day = new Day4();

Console.WriteLine("Running app");
string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day4)}.txt";
string[] input = File.ReadAllLines(filePath);
var result = day.Test(input);
Console.WriteLine(result);