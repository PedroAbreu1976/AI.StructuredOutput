using System.Text.Json;
using AI.StructuredOutput.Engines.Gemini.Model;

namespace AI.StructuredOutput.Engines.Gemini
{
    public class GeminiStructuredOutputGenerator(IGeminiApiService geminiApiService) : IAiStructuredOutputGenerator
    {
        public async Task<TResult?> AskAsync<TResult>(string question, params FileInfo[] files)
        {
            var request = GeminiQueryRequest.CreateFromPrompt(question, typeof(TResult));
            foreach (var file in files?? Array.Empty<FileInfo>())
            {
                request.AddFile(file.Name, file);
            }
            var response = await geminiApiService.QueryAsync(request);
            var json = response.Candidates
                    .SelectMany(x => x.Content.Parts)
                    .Select(x => x.Text)
                    .First();
            return JsonSerializer.Deserialize<TResult>(json);
        }
    }
}
