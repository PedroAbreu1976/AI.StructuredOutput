
namespace AI.StructuredOutput.Schema.Builders
{
    internal class DateSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return type == typeof(DateOnly);
        }

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "string",
                Format = "date"
            };
        }
    }
}
