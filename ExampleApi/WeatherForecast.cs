using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExampleApi;

[Description("A weather forecast.")]
public sealed record WeatherForecast
{
    [property: Required]
    [property: Description("The date that the weather forecast applies to.")]
    public required DateOnly Date { get; init; }

    [property: Required]
    [property: Description("The temperature in Celsius")]
    public required int TemperatureC { get; init; }
    
    [property: Required]
    [property: Description("A summary of the forecast for this day")]
    [property: MaxLength(100)]
    public required string Summary { get; init; }
    
    [property: Description("The temperature in Fahrenheit")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public static WeatherForecast GetRandom(DateOnly date) => new()
    {
        Date = date,
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    };
    
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
}
