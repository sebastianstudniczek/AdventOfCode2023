namespace Runner;

public interface IExercise<out TOutput>
{
    string[] TestInput { get; }
    TOutput Test(string[] input);
}