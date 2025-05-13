using System.ComponentModel;
using System.Reflection;

namespace AI.StructuredOutput.Schema.Decorators
{
    internal class DescriptionResponseSchemaDecorator : ResponseSchemaDecorator
    {
        public override void Decorate(ResponseSchema schema, PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
            schema.Description = attribute?.Description;
        }
    }
}
