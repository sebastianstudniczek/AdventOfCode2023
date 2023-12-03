using Runner.Days;

var day = new Day2();

string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day2)}.txt";
string[] input = File.ReadAllLines(filePath);
var result = day.Test(input);
Console.WriteLine(result);