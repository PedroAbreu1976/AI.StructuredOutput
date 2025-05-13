namespace AI.StructuredOutput.Engines.Gemini
{
    public class GeminiApiConfig
    {
        public string Url { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models";
        public string Model { get; set; } = "gemini-1.5-flash";
        public string ApiKey { get; set; }
    }
}
