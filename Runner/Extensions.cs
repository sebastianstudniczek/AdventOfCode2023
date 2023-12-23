namespace Runner;

public static class Extensions
{
    public static bool In<T>(this T value, IEnumerable<T> source) 
        => source.Contains(value);

    public static void Do<T>(this IEnumerable<T> source, Action<T> callback)
    {
        foreach (var item in source)
        {
            callback(item);
        }
    }
    public static Func<T1, R> Compose<T1, T2, R>(
        Func<T1, T2> f1,
        Func<T2, R> f2) =>
        x => f2(f1(x));

    public static Func<T1, R> Compose<T1, T2, T3, T4, R>(
        Func<T1, T2> f1,
        Func<T2, T3> f2,
        Func<T3, T4> f3,
        Func<T4, R> f4) => 
        x => f4(f3(f2(f1(x))));

    public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(Func<T1, T2, R> func) 
        => x => y => func(x, y);
}
