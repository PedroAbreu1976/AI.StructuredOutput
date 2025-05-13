using System.Text.Json.Serialization;

namespace AI.StructuredOutput.Engines.Gemini.Model
{
    public class Candidate
    {
        [JsonPropertyName("content")]
        public Content Content { get; set; }
    }
}
