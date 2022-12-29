namespace functional_thinking;

public record Food();

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
        var foods = new[] { new Food(), new Food(), new Food() };

        void Cook(Food food)
        {
            // cook the food
        }

        void Eat(Food food)
        {
            // eat the food
        }
        
        for (var i = 0; i < foods.Length; i++)
        {
            Cook(foods[i]);
        }
        
        for (var i = 0; i < foods.Length; i++)
        {
            Eat(foods[i]);
        }
        
        for (var i = 0; i < foods.Length; i++)
        {
            Cook(foods[i]);
            Eat(foods[i]);
        }
        
        foreach (var food in foods)
        {
            Cook(food);
        }
        
        foreach (var food in foods)
        {
            Eat(food);
        }
        
        foreach (var food in foods)
        {
            Cook(food);
            Eat(food);
        }
        
        Array.ForEach(foods, Cook);
        Array.ForEach(foods, Eat);
        Array.ForEach(foods, food =>
        {
            Cook(food);
            Eat(food);
        });
        
        foods.ForEach(Cook);
        foods.ForEach(Eat);
        foods.ForEach(food =>
        {
            Cook(food);
            Eat(food);
        });
        
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
        
        WithLogging(() => Cook(foods[-1])); // throws 

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
}
