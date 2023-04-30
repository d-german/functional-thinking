using System.Collections.Immutable;
using Newtonsoft.Json;
using NuGet.Frameworks;

namespace functional_thinking;

public class WeatherDtoTests
{
    record Coordinate(int I, int I1)
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    static Coordinate TranslateX(IEnumerable<int> numbers)
    {
        var result = new Coordinate(0, 0);

        foreach (var number in numbers)
        {
            result = result with {X = result.X + number};
        }

        return result;
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TranslateXTest()
    {
        var ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var finalCoordinate = TranslateX(ints);
        Assert.That(finalCoordinate.X, Is.EqualTo(55));
        
// var finalCoordinate = ints.Reduce(new Coordinate(0, 0), (acc, x) => acc with { X = acc.X + x });
// Assert.That(finalCoordinate.X, Is.EqualTo(55));
    }

    [Test]
    public void Test1()
    {
        var weather = new WeatherDto { City = "London", CurrentTemperature = 15, ForecastedTemperatures = ImmutableArray.Create(16, 17, 18, 19, 20) };
        var weather2 = weather with { };
        var weather3 = weather with { City = "London", CurrentTemperature = 15 };
        var weather4 = new WeatherDto { City = "London", CurrentTemperature = 15, ForecastedTemperatures = ImmutableArray.Create(16, 17, 18, 19, 20) };
        var weather5 = new WeatherDto {
            City = "London", CurrentTemperature = 15, ForecastedTemperatures = ImmutableArray.Create(16, 17, 18, 19, 21)
        }; // different forecasted temperatures
        Assert.Multiple(() =>
        {
            // https://stackoverflow.com/questions/63813872/record-types-with-collection-properties-collections-with-value-semantics

            Assert.That(weather2, Is.EqualTo(weather));
            Assert.That(weather2, Is.EqualTo(weather3));
            Assert.That(weather2, Is.Not.EqualTo(weather4));
        });
        AssertAreEqualByJson(weather, weather4);
        AssertAreNotEqualByJson(weather, weather5);
    }

    private static void AssertAreEqualByJson(object expected, object actual)
    {
        var expectedJson = JsonConvert.SerializeObject(expected);
        var actualJson = JsonConvert.SerializeObject(actual);
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    private static void AssertAreNotEqualByJson(object expected, object actual)
    {
        var expectedJson = JsonConvert.SerializeObject(expected);
        var actualJson = JsonConvert.SerializeObject(actual);
        Assert.That(actualJson, Is.Not.EqualTo(expectedJson));
    }
}
