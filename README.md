# .NET AI Structured Output Generator

[![NuGet version (YourPackageName)](https://img.shields.io/nuget/v/AI.StructuredOutput.svg?style=flat-square)](https://www.nuget.org/packages/AI.StructuredOutput/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) <!-- Choose your license -->

Effortlessly query AI models (starting with Google's Gemini) and receive strongly-typed, structured C# objects as output. Instead of parsing messy JSON or string responses, define your desired C# class, and let the AI fill it in for you!

## Features

*   **Strongly-Typed Outputs:** Get C# objects directly from AI responses.
*   **Attribute-Driven Schema:** Use `[Description]` attributes on your class properties to guide the AI in populating the data.
*   **Data Type Support:** Handles common types like `string`, `decimal`, `DateTime`, `enum`, `double`, `bool`, etc.
*   **Constraints:** Use attributes like `[Range]` to provide further guidance to the AI.
*   **Easy Integration:** Simple setup with .NET's dependency injection.
*   **Provider-Based:** Currently supports Google Gemini, with a design that can be extended to other AI providers.

## Prerequisites

*   .NET 8.0 or later.
*   A Google Gemini API Key. You can obtain one from [Google AI Studio](https://aistudio.google.com/app/apikey).

## Installation

Install the package via NuGet Package Manager:

```powershell
Install-Package YourPackageName
```

Or via the .NET CLI:

```bash
dotnet add package YourPackageName
```

**(Remember to replace `YourPackageName` with your actual package name once published!)**

## Getting Started

### 1. Configure Services

When registering the services, add the Gemini structured output generator:

```csharp
using AI.StructuredOutput;

//services is IServiceCollection
services.AddGeminiStructuredOutputGenerator("YOUR_GEMINI_API_KEY");
```
**Important:** Securely manage your API key. Consider using user secrets, environment variables, or a configuration provider for production applications.

### 2. Define Your Output Structure

Create a C# class that represents the structure you want the AI to populate. Use `[Description]` attributes to guide the AI on what each property means.

```csharp
public class WheatherInfo // Renamed from WheatherInfo for correct spelling
{
    [Description("The city and country, e.g., 'London, UK' or 'Paris, France'")]
    public string Location { get; set; }

    [Description("The current temperature in Fahrenheit")]
    public decimal Fahrenheit { get; set; }

    [Description("The current temperature in Celcius")]
    public decimal Celcius { get; set; }

    [Description("Do not make a joke, just say something like ´good morning´")] 
    public string Joke { get; set; }

    [Description("The local date and time at the specified location when these weather conditions were recorded")]
    public DateTime TimeStamp { get; set; }

    [Description("The current wind conditions")]
    public WindCondition WindCondition { get; set; }

    [Description("Assign a random number")]
    [Range(0.0, 1.0)]
    public double Test { get; set; } // Perhaps rename to `RandomValue` for clarity
}

public enum WindCondition
{
    [Description("Calm, almost no wind")]
    NoWind = 0,
    [Description("A gentle breeze is present")]
    LightWind = 1,
    [Description("Noticeably windy, potentially strong gusts")]
    StrongWind = 2,
}
```
**Tip:** The more descriptive your `[Description]` attributes are, the better the AI will understand what data to provide. You can also guide the *format* of the data.

### 3. Query the AI

Retrieve the `IAiStructuredOutputGenerator` service and use the `AskAsync<T>()` method:

```csharp
var service = host.Services.GetRequiredService<IAiStructuredOutputGenerator>();

var city = "Angra do Heroismo";

Debug.WriteLine($"Fetching weather for {city}...");
var weatherInfo = await service.AskAsync<WheatherInfo>($"What´s the weather like in {city}?");

Debug.WriteLine($"--- Weather in {weatherInfo.Location} ({weatherInfo.TimeStamp}) ---");
Debug.WriteLine($"Temperature: {weatherInfo.Celcius}°C / {weatherInfo.Fahrenheit}°F");
Debug.WriteLine($"Wind: {weatherInfo.WindCondition}");
Debug.WriteLine($"Not a joke: {weatherInfo.Joke}");
Debug.WriteLine($"Random Test Number (between 0.0 and 1.0): {weatherInfo.Test}");
```

### Example Output

```
Fetching weather for Angra do Heroismo...
--- Weather in Angra do Heroismo, Portugal (2023-10-27 10:30:00) ---
Temperature: 12°C / 53.6°F
Wind: LightWind
AI's friendly note: Good morning!
Random Test Number: 0.734
```
*(Note: Actual output will vary based on the AI's response.)*

## How it Works (Briefly)

The SDK takes your C# class definition and uses the property names, types, and `[Description]` attributes to construct a schema or a set of instructions for the AI. It then sends your prompt along with these instructions to the configured AI model (e.g., Gemini). The AI attempts to generate a response that conforms to the requested structure, which the SDK then deserializes into an instance of your C# class.

## Supported AI Providers

*   **Google Gemini:** Configured using `services.AddGeminiStructuredOutputGenerator("YOUR_API_KEY")`.

*(Future providers can be added by implementing `IAiStructuredOutputGenerator` and providing a corresponding extension method.)*

## License

MIT
