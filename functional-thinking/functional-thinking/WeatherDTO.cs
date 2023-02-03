using System.Collections.Immutable;

namespace functional_thinking
{
    public record WeatherDto
    {
        public string? City { get; init; }
        public int CurrentTemperature { get; init; }
        public ImmutableArray<int> ForecastedTemperatures { get; init; }
    }
}
