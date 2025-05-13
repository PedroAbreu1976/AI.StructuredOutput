using System.Text.Json.Serialization;

namespace AI.StructuredOutput.Engines.Gemini.Model
{
    public class InlineData
    {
        [JsonPropertyName("mime_type")]
        public string MimeType { get; set; }
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
