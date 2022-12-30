namespace functional_thinking;

static public class ExtensionMethods
{
    // public static void ForEach<T>(this T[] array, Action<T> action)
    // {
    //     foreach (var t in array)
    //     {
    //         action(t);
    //     }
    // }

    // public static IEnumerable<TResult> Map<TResult, TSource>(this IEnumerable<TSource> items, Func<TSource, TResult> func)
    // {
    //     var result = new List<TResult>();
    //
    //     foreach (var item in items)
    //     {
    //         result.Add(func(item));
    //     }
    //
    //     return result;
    // }

    public static IEnumerable<TResult> Map<TResult, TSource>(this IEnumerable<TSource> items, Func<TSource, TResult> func)
    {
        foreach (var item in items)
        {
            yield return func(item);
        }
    }
}
