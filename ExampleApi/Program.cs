using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExampleApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(x => x.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseCors();

var apiGroup = app
    .MapGroup("/forecasts")
    .WithTags("Weather Forecasts");

apiGroup.MapGet("/", (
        [Description("The first date to get a forecast for. Defaults to today.")]
        DateOnly? startDate = null, 
        
        [Description("The number of forecasts to get. Max 100.")]
        [Range(1, 100)] 
        int limit = 10) =>
    {
        var forecast = Enumerable.Range(1, limit)
            .Select(index => WeatherForecast.GetRandom(startDate ?? DateOnly.FromDateTime(DateTime.Now.AddDays(index))))
            .ToArray();
        
        return forecast;
    })
    .WithName("GetWeatherForecasts")
    .WithSummary("Get Weather Forecasts")
    .WithDescription("Get the weather forecast for the specified number of days.");

/*
apiGroup.MapGet("/now", () => WeatherForecast.GetRandom(DateOnly.FromDateTime(DateTime.Now)))
    .WithName("GetWeatherForecast")
    .WithSummary("Get Weather Forecast")
    .WithDescription("Get the weather forecast for a specific date.");

apiGroup.MapGet("/byDate/{date:datetime}", (DateTime date) => WeatherForecast.GetRandom(DateOnly.FromDateTime(date)))
    .WithName("GetWeatherForecast")
    .WithSummary("Get Weather Forecast")
    .WithDescription("Get the weather forecast for a specific date.");
*/

app.Run();