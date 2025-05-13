using System.Text.Json.Serialization;

namespace AI.StructuredOutput.Engines.Gemini.Model
{
    public class GeminiResponse
    {
        [JsonPropertyName("candidates")]
        public List<Candidate> Candidates { get; set; }
    }
}
