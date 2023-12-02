using Runner.Days;

var day1 = new Day1();

string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day1)}.txt";
string[] input = File.ReadAllLines(filePath);
var result = day1.Test(input);
Console.WriteLine(result);