
namespace AI.StructuredOutput.Schema.Builders
{
    internal class NumberSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(decimal);
        }

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "number",
                Format = "double"
            };
        }
    }
}
