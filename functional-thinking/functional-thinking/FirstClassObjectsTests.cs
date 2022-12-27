namespace functional_thinking;

[TestFixture]
public class FirstClassObjectsTests
{
    [Test]
    public void MultiplyTest()
    {
        Func<double, double, double> multiply = (x, y) => x * y;
        
        var myFunc = multiply;
        
        
        Assert.That(multiply(2, 3), Is.EqualTo(6));
    }
}
