namespace functional_thinking;

public class FirstClassObjects
{
    // ReSharper disable once UnusedMember.Local
#pragma warning disable CA1823
    private FirstClassObjects _firstClassObjects = new FirstClassObjects();
#pragma warning restore CA1823
    
private string _logMessage = "Hello, world!"; // enforce this to be const

public void Print()
{
    
    string hello = "hello"; // enforce this to be const

    Console.WriteLine(hello);
    
    Console.WriteLine(_logMessage);
}
}
