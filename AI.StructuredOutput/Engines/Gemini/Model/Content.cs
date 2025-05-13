using System.Text.Json.Serialization;

namespace AI.StructuredOutput.Engines.Gemini.Model
{
    public class Content
    {
        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; }
    }
}
