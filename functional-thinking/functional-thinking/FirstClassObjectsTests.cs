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

    [Test]
    public void Lazy()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var numbersPlus2 = numbers.Map(x => x + 2);

// At this point, the numbers are not yet mapped to numbersPlus2 because the mapping is lazy

        var count = 0;
        foreach (var number in numbersPlus2)
        {
            Console.WriteLine(number); // 3 4 5 6 7
            count++;
            if (count == 5) break;
        }

// At this point, only the first 5 numbers are mapped to numbersPlus2 because the foreach loop only iterates 5 times

        IEnumerable<int> LessThan5(IEnumerable<int> numbers)
        {
            List<int> result = new List<int>();

            foreach (var number in numbers)
            {
                if (number < 5) result.Add(number);
            }

            return result;
        }
    }

    [Test]
    public void Chain()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var numberLessThan5Plus2 = numbers
            .Filter(x => x < 5)
            .Map(x => x + 2);

// At this point, the numbers not yet mapped

        foreach (var number in numberLessThan5Plus2)
        {
            Console.WriteLine(number); // 3 4 5 6
        }
    }

    private record Person(string Name, int Age);

    [Test]
    public void ReduceAge()
    {
        var ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Person PersonFromReducedAges(IEnumerable<int> ages)
        {
            var result = new Person("John", 0);

            foreach (var age in ages)
            {
                result = result with { Age = result.Age + age };
            }

            return result;
        }

        var person = PersonFromReducedAges(ints);
        Assert.That(person.Age, Is.EqualTo(55));
    }

    [Test]
    public void ReduceTest()
    {
        var ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Person PersonFromReducedAges(IEnumerable<int> ages)
        {
            var result = new Person("John", 0);

            foreach (var age in ages)
            {
                result = result with { Age = result.Age + age };
            }

            return result;
        }

        var person = PersonFromReducedAges(ints);
        Assert.That(person.Age, Is.EqualTo(55));
    }

    [Test]
    public void ReduceTotal()
    {
        var ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int TotalFromReducedNumbers(IEnumerable<int> numbers)
        {
            var result = 0;

            foreach (var number in numbers)
            {
                result += number;
            }

            return result;
        }

        var total = TotalFromReducedNumbers(ints);
        Assert.That(total, Is.EqualTo(55));
    }

    [Test]
    public void ReduceExtensionTotal()
    {
        var ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var total = ints.Reduce(0, (acc, x) => acc + x);
        Assert.That(total, Is.EqualTo(55));
        
        var person = ints.Reduce(new Person("John", 0), (acc, x) => acc with { Age = acc.Age + x });
        Assert.That(person.Age, Is.EqualTo(55));
        
    }
}
