namespace AI.StructuredOutput.Engines.Gemini.Middleware
{
    public class GeminiGenerateContentRequestHandler(GeminiApiConfig geminiApiConfig) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = new Uri($"{request.RequestUri}/{geminiApiConfig.Model}:generateContent?key={geminiApiConfig.ApiKey}");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
