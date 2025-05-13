
namespace AI.StructuredOutput.Schema.Builders
{
    internal class StringSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return type == typeof(string);
        }

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "string"
            };
        }
    }
}
