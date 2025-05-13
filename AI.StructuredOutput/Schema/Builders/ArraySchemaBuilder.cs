
namespace AI.StructuredOutput.Schema.Builders
{
    internal class ArraySchemaBuilder : SchemaBuilder
    {
        public override bool AppliesTo(Type type)
        {
            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        public override ResponseSchema Build(Type type)
        {
            if(!GetEnumerableGenericType(type, out Type? genericType))
            {
                return new ResponseSchema();
            }
            return new ResponseSchema
            {
                Type = "array",
                Items = SchemaBuilderFactory.Build(genericType!)
            };
        }

        private bool GetEnumerableGenericType(Type type, out Type? genericType)
        {
            genericType = null;
            if (type == null || type == typeof(string))
            {
                return false;
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                genericType = type.GetGenericArguments().First();
                return true;
            }
            Type? ienumerableInterface = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (ienumerableInterface == null)
            {
                return false;
            }
            genericType = ienumerableInterface?.GetGenericArguments().First();
            return true;
        }
    }
}
