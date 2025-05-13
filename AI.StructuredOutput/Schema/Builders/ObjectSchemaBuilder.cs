using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AI.StructuredOutput.Schema.Builders
{
    internal class ObjectSchemaBuilder: SchemaBuilder
    {
        public override bool AppliesTo(Type type) => true;

        public override ResponseSchema Build(Type type)
        {
            return new ResponseSchema
            {
                Type = "object",
                Properties = type.GetProperties()
                    .Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                    .ToDictionary(
                        x => GetPropertyName(x), 
                        x => SchemaBuilderFactory
                            .Build(GetType(x))
                            .DecorateSchema(x)),
            };
        }

        private string GetPropertyName(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            return attribute?.Name ?? JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.Name);
        }

        private Type GetType(PropertyInfo propertyInfo)
        {
            if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null)
            {
                return Nullable.GetUnderlyingType(propertyInfo.PropertyType)!;
            }
            return propertyInfo.PropertyType;
        }
    }
}
