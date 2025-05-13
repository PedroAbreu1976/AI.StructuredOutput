using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using AI.StructuredOutput.Schema.Decorators;

namespace AI.StructuredOutput.Schema
{
    public static class Extensions
    {
        public static ResponseSchema BuildSchema(this Type type)
        {
            return SchemaBuilderFactory.Build(type);
        }

        public static string ToSchemaJson(this Type type)
        {
            var schema = type.BuildSchema();
            return JsonSerializer.Serialize(schema, new JsonSerializerOptions { WriteIndented = true });
        }

        internal static ResponseSchema DecorateSchema(this ResponseSchema schema, PropertyInfo propertyInfo)
        {
            ResponseSchemaDecorators.Instance.Decorate(schema, propertyInfo);
            return schema;
        }
    }
}
