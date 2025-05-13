using AI.StructuredOutput.Engines.Gemini;
using AI.StructuredOutput.Engines.Gemini.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AI.StructuredOutput
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeminiStructuredOutputGenerator(this IServiceCollection services,
            string url,
            string model,
            string apiKey)
        {
            var geminiConfig = new GeminiApiConfig
            {
                Url = url,
                Model = model,
                ApiKey = apiKey
            };

            services.AddScoped<GeminiGenerateContentRequestHandler>();
            services.AddSingleton<GeminiApiConfig>(geminiConfig);

            services.AddRefitClient<IGeminiApiService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri( geminiConfig.Url))
                .AddHttpMessageHandler<GeminiGenerateContentRequestHandler>();

            services.AddScoped<IAiStructuredOutputGenerator, GeminiStructuredOutputGenerator>();

            return services;
        }

        public static IServiceCollection AddGeminiStructuredOutputGenerator(this IServiceCollection services,
            string apiKey)
        {
            var geminiConfig = new GeminiApiConfig
            {
                ApiKey = apiKey
            };

            return services.AddGeminiStructuredOutputGenerator(
                geminiConfig.Url,
                geminiConfig.Model,
                geminiConfig.ApiKey);
        }
    }
}
