namespace AI.StructuredOutput
{
    public interface IAiStructuredOutputGenerator
    {
        Task<TResult?> AskAsync<TResult>(string question, params FileInfo[] files);
    }
}
