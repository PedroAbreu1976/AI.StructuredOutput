
namespace AI.StructuredOutput.Schema.Builders
{
    internal class DateTimeSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return type == typeof(DateTime);
        }

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "string",
                Format = "date-time"
            };
        }
    }
}
