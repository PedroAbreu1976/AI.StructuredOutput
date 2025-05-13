namespace AI.StructuredOutput.Schema.Builders
{
    internal class EnumSchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return type.IsEnum;
        }
        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "string",
                Enum = Enum.GetNames(type),
            };
        }
    }
}
