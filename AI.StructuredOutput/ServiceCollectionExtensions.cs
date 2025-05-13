using AI.StructuredOutput.Engines.Gemini;
using AI.StructuredOutput.Engines.Gemini.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AI.StructuredOutput
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register gemini as the AI to handle structured outputs
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="url">The gemini Url (default: 'https://generativelanguage.googleapis.com/v1beta/models'</param>
        /// <param name="model">The gemini model (default: 'gemini-1.5-flash')</param>
        /// <param name="apiKey">Your gemini api key</param>
        /// <returns>The service collection</returns>
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

        /// <summary>
        /// Register gemini as the AI to handle structured outputs
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="apiKey">Your gemini api key</param>
        /// <returns>The service collection</returns>
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
