using System.Reflection;

namespace AI.StructuredOutput.Schema.Decorators
{
    internal class NullableResponseSchemaDecorator : ResponseSchemaDecorator
    {
        public override void Decorate(ResponseSchema schema, PropertyInfo propertyInfo)
        {
            schema.Nullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
        }
    }
}
