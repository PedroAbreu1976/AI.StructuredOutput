
namespace AI.StructuredOutput.Schema.Builders
{
    internal class IntegerSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(short) ||
                type == typeof(UInt16) || 
                type == typeof(UInt32) ||
                type == typeof(UInt64) ||
                type == typeof(byte);
        }

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "integer"
            };
        }
    }
}
