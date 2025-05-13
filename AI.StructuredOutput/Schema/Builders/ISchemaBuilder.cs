namespace AI.StructuredOutput.Schema.Builders
{
    public interface ISchemaBuilder
    {
        bool AppliesTo(Type type);
        ResponseSchema Build(Type type);
    }
}
