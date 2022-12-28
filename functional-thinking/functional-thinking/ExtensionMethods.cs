namespace functional_thinking;

static public class ExtensionMethods
{
public static void ForEach<T>(this T[] array, Action<T> action)
{
    foreach (var t in array)
    {
        action(t);
    }
}
}
