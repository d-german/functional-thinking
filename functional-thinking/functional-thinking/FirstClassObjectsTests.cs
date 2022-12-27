namespace functional_thinking;

[TestFixture]
public class FirstClassObjectsTests
{
    [Test]
    public void MultiplyTest()
    {
        // declare a function that multiplies two numbers
        Func<double, double, double> multiply = (x, y) => x * y;

        Assert.That(multiply(2, 3), Is.EqualTo(6));

        // assign the function to a variable
        var multiply2 = multiply;

        // inset the functions into an array
        var multiplications = new[] { multiply, multiply2 };

        // call the function from the array
        foreach (var multiplication in multiplications)
        {
            Assert.That(multiplication(2, 3), Is.EqualTo(6));
        }

        // Pass the function to another function
        var result = ApplyFunction(multiply, 2, 3);

        double ApplyFunction(Func<double, double, double> func, double x, double y)
        {
            return func(x, y);
        }

        Assert.That(result, Is.EqualTo(6));
    }
}
