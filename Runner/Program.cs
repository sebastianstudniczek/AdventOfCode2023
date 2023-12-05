using Runner.Days;

var day = new Day3();

string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day3)}.txt";
string[] input = File.ReadAllLines(filePath);
var result = day.Test(input);
Console.WriteLine(result);