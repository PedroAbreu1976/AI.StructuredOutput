
namespace AI.StructuredOutput.Schema.Builders
{
    internal class BooleanSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return type == typeof(bool);
        }

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "boolean"
            };
        }
    }
}
