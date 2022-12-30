namespace functional_thinking;

public record Food();

[TestFixture]
public class FirstClassObjectsTests
{
    private static readonly Food[] Foods = new[] { new Food(), new Food(), new Food() };

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

    [Test]
    public void IfTest()
    {
        T If<T>(bool condition, Func<T> trueFunc, Func<T> falseFunc)
        {
            if (condition)
                return trueFunc();
            else
                return falseFunc();
        }

        var result = If(true, () => 1, () => 2);

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void ForLoopTest()
    {
        // creat an array of Food

        void Cook(Food food)
        {
            // cook the food
        }

        void Eat(Food food)
        {
            // eat the food
        }

        for (var i = 0; i < Foods.Length; i++)
        {
            Cook(Foods[i]);
        }

        for (var i = 0; i < Foods.Length; i++)
        {
            Eat(Foods[i]);
        }

        for (var i = 0; i < Foods.Length; i++)
        {
            Cook(Foods[i]);
            Eat(Foods[i]);
        }

        foreach (var food in Foods)
        {
            Cook(food);
        }

        foreach (var food in Foods)
        {
            Eat(food);
        }

        foreach (var food in Foods)
        {
            Cook(food);
            Eat(food);
        }

        Array.ForEach(Foods, Cook);
        Array.ForEach(Foods, Eat);
        Array.ForEach(Foods, food =>
        {
            Cook(food);
            Eat(food);
        });

        // Foods.ForEach(Cook);
        // Foods.ForEach(Eat);
        // Foods.ForEach(food =>
        // {
        //     Cook(food);
        //     Eat(food);
        // });

        void WithLogging(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        WithLogging(() => Cook(Foods[-1])); // throws 

        Action<T> WrapLogging<T>(Action<T> action)
        {
            return t =>
            {
                try
                {
                    action(t);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            };
        }

        var cookFoodWithLogging = WrapLogging<Food>(Cook);

        cookFoodWithLogging(new Food()); // logs exception

        var eatFoodWithLogging = WrapLogging<Food>(Eat);

        eatFoodWithLogging(new Food()); // logs exception

        var cookAndEatFoodWithLogging = WrapLogging<Food>(food =>
        {
            Cook(food);
            Eat(food);
        });

        cookAndEatFoodWithLogging(new Food()); // logs exception
    }

    [Test]
    public void METHOD()
    {
        Action WrapLogging(Action action)
        {
            return () =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            };
        }

        void Cook(Food food)
        {
            // cook the food
        }

        var cookFoodWithLogging = WrapLogging(() => Cook(Foods[-1])); // defer execution of Cook, nothing is logged

        cookFoodWithLogging(); // logs exception
    }

    [Test]
    public void MakeAdderTest()
    {
        Func<int, int> MakeAdder(int x)
        {
            return y => x + y;
        }

        var add2 = MakeAdder(2);
        var add3 = MakeAdder(3);

        Assert.That(add2(3), Is.EqualTo(5));
    }

    [Test]
    public void DeriveMap()
    {
        IEnumerable<int> Add2(IEnumerable<int> numbers)
        {
            List<int> result = new List<int>();

            foreach (var number in numbers)
            {
                result.Add(number + 2);
            }

            return result;
        }

        IEnumerable<string> IntToString(IEnumerable<int> numbers)
        {
            List<string> result = new List<string>();

            foreach (var number in numbers)
            {
                result.Add(number.ToString());
            }

            return result;
        }

        var numbers = new[] { 1, 2, 3 };
        var numbersPlus2 = numbers.Map(x => x + 2);
        var strings = numbers.Map(x => x.ToString());

        Assert.That(numbersPlus2, Is.EqualTo(new[] { 3, 4, 5 }));
        Assert.That(strings, Is.EqualTo(new[] { "1", "2", "3" }));
    }
}
