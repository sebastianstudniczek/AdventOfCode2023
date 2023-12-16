using Runner.Days;


var day = new Day9();

Console.WriteLine("Running app");
string filePath = $"{Directory.GetCurrentDirectory()}\\Inputs\\{nameof(Day9)}.txt";
string[] input = File.ReadAllLines(filePath);
var result = day.Test(input);
Console.WriteLine(result);