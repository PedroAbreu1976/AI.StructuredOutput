using System.ComponentModel;
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

        internal static List<(int Value, string Name, string? Description)> GetEnumInfo(this Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", nameof(enumType));
            }

            var enumInfoList = new List<(int Value, string Name, string? Description)>();

            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                object enumValueObject = Enum.Parse(enumType, name);
                int value = Convert.ToInt32(enumValueObject);
                string? description = null;
                var memberInfo = enumType.GetMember(name).FirstOrDefault();

                if (memberInfo != null)
                {
                    var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                    if (descriptionAttribute != null)
                    {
                        description = descriptionAttribute.Description;
                    }
                }

                enumInfoList.Add((value, name, description));
            }

            return enumInfoList;
        }
    }
}
