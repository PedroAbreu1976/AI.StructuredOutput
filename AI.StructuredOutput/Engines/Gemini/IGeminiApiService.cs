using AI.StructuredOutput.Engines.Gemini.Model;
using Refit;

namespace AI.StructuredOutput.Engines.Gemini
{
    public interface IGeminiApiService
    {
        [Post("")]
        Task<GeminiResponse> QueryAsync(GeminiQueryRequest request);
    }
}
