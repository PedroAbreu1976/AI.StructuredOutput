using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace AI.StructuredOutput.Engines.Gemini.Model
{
    public partial class GeminiQueryRequest
    {
        public List<Content> Contents { get; set; } = new();

        [JsonPropertyName("generationConfig")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GeminiGenerationConfig? GenerationConfig { get; set; }

        public GeminiQueryRequest AddFile(string fileName, FileInfo file)
        {
            return AddFile(fileName, File.ReadAllBytes(file.FullName));
        }

        public GeminiQueryRequest AddFile(string fileName, byte[] data)
        {
            return AddFile(fileName, Convert.ToBase64String(data));
        }

        public GeminiQueryRequest AddFile(string fileName, string base64)
        {
            var content = this.Contents.FirstOrDefault();
            if(content == null)
            {
                content = new Content();
                this.Contents.Add(content);
            }
            content.Parts.Add(new Part()
            {
                InlineData = new InlineData()
                {
                    MimeType = GetMimeType(Path.GetExtension(fileName)),
                    Data = base64
                }
            });
            return this;
        }

        public static GeminiQueryRequest CreateFromPrompt(string prompt, Type responseType)
        {
            return new GeminiQueryRequest()
            {
                Contents = new List<Content>()
                {
                    new Content()
                    {
                        Parts = new List<Part>()
                        {
                            new Part()
                            {
                                Text = prompt
                            }
                        }
                    }
                },
                GenerationConfig = GeminiGenerationConfig.FromType(responseType)
            };
        }

        private string GetMimeType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".pdf": return "application/pdf";
                case ".js": return "application/x-javascript";
                case ".py": return "application/x-python";
                case ".txt": return "text/plain";
                case ".htm":
                case ".html": return "text/html";
                case ".css": return "text/css";
                case ".md": return "text/md";
                case ".csv": return "text/csv";
                case ".xml": return "text/xml";
                case ".rtf": return "text/rtf";
                default: throw new Exception($"Unsupported file type: {fileExtension}");
            }
        }
    }
}
