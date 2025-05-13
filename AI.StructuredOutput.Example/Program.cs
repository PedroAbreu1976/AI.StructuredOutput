using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AI.StructuredOutput;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddGeminiStructuredOutputGenerator("AIzaSyCqHaMf5L4z9MFgoh6GdMzgJfO1osFGXGk");
            })
            .Build();
await host.StartAsync();


var service = host.Services.GetRequiredService<IAiStructuredOutputGenerator>();
var weatherInfo = await service.AskAsync<WheatherInfo>("What´s the weather like in Angra do Heroismo");
if(weatherInfo == null)
{
    Console.WriteLine("Oops, something went wrong.");
}
else
{
    Console.WriteLine($"Weather conditions in {weatherInfo.Location} at {weatherInfo.TimeStamp}");
    Console.WriteLine($"Fahrenheit: {weatherInfo.Fahrenheit}");
    Console.WriteLine($"Celcius: {weatherInfo.Celcius}");
    Console.WriteLine($"Joke: {weatherInfo.Joke}");
    Console.WriteLine($"Wind: {weatherInfo.WindCondition}");
    Console.WriteLine($"Random Number [0.0, 1.0]: {weatherInfo.Test}");
}
Console.Write("Press any key to close...");
Console.ReadKey();

public class WheatherInfo
{
    [Description("The location")]
    public string Location { get; set; }

    [Description("The Fahrenheit degrees")]
    public decimal Fahrenheit {  get; set; }

    [Description("The Celcius degrees")]
    public decimal Celcius { get; set; }

    [Description("Do not make a joke, just say something like ´good morning´")]
    public string Joke {  get; set; }

    [Description("The time at the location when the conditions were recorded")]
    public DateTime TimeStamp { get; set; }

    [Description("The wind conditions")]
    public WindCondition WindCondition { get; set; }

    [Description("Assign a random number")]
    [Range(0.0, 1.0)]
    public double Test {  get; set; }
}

public enum WindCondition
{
    [Description("Almost no wind")] NoWind = 0,
    [Description("Some light wind")] LightWind = 1,
    [Description("Very windy")] StrongWind = 2,
}