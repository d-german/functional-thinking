namespace functional_thinking;

public class ConstVsStaticReadonlyDemo
{
    private static readonly string StaticReadonlyStr = "Hello, world!";
    private const string ConstStr = "Hello, world!";

    private static readonly List<string> StaticReadonlyList = new()
    {
        "Hello, world!",
        ConstStr
    };

    public static void PrintValues()
    {
        Console.WriteLine(StaticReadonlyStr);
        Console.WriteLine(ConstStr);
        Console.WriteLine(StaticReadonlyList[0]);
    }
}
