using System.Text.Json.Serialization;
using AI.StructuredOutput.Schema;

namespace AI.StructuredOutput.Engines.Gemini.Model
{

    public class GeminiGenerationConfig
    {
        [JsonPropertyName("responseMimeType")]
        public string MimeType { get; set; } = "application/json";
        [JsonPropertyName("responseSchema")]
        public ResponseSchema Schema { get; set; } = new();

        public static GeminiGenerationConfig FromType(Type type)
        {
            return new GeminiGenerationConfig()
            {
                MimeType = "application/json",
                Schema = type.BuildSchema()
            };
        }
    }
}
