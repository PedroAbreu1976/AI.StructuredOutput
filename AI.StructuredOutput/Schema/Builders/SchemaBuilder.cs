namespace AI.StructuredOutput.Schema.Builders
{
    internal abstract class SchemaBuilder: ISchemaBuilder
    {
        public abstract bool AppliesTo(Type type);
        public abstract ResponseSchema Build(Type type);
    }
}
