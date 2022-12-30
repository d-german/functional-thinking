namespace functional_thinking;

public static class ExtensionMethods
{
    public static IEnumerable<TResult> Map<TResult, TSource>(this IEnumerable<TSource> items, Func<TSource, TResult> func)
    {
        foreach (var item in items)
        {
            yield return func(item);
        }
    }

    public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> predicate)
    {
        foreach (var item in items)
        {
            if (!predicate(item)) continue;
            yield return item;
        }
    }
}
