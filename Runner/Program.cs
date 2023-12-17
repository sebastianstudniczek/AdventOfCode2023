using Runner.Days;


var day = new Day10();

Console.WriteLine("Running app");
string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day10)}Test3.txt";
string[] input = File.ReadAllLines(filePath);
var result = day.Test(input);
Console.WriteLine(result);