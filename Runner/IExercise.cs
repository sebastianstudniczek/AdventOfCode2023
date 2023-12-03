namespace Runner;

public interface IExercise<out TOutput>
{
    TOutput Test(string[] input);
}