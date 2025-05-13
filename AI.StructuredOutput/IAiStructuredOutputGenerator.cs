namespace AI.StructuredOutput
{
    /// <summary>
    /// Generator for structured outputs
    /// </summary>
    public interface IAiStructuredOutputGenerator
    {
        /// <summary>
        /// Queries the AI for a structured output
        /// </summary>
        /// <typeparam name="TResult">The expected result type</typeparam>
        /// <param name="question">The question</param>
        /// <param name="files">Files to be uploaded</param>
        /// <returns>The structured output</returns>
        Task<TResult?> AskAsync<TResult>(string question, params FileInfo[] files);
    }
}
