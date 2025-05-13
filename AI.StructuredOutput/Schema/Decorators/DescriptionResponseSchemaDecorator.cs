using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace AI.StructuredOutput.Schema.Decorators
{
    internal class DescriptionResponseSchemaDecorator : ResponseSchemaDecorator
    {
        public override void Decorate(ResponseSchema schema, PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
            StringBuilder sb = new StringBuilder();
            sb.Append(attribute?.Description);
            if (propertyInfo.PropertyType.IsEnum)
            {
                sb.AppendLine(":");
                var enumInfo = propertyInfo.PropertyType.GetEnumInfo();
                foreach (var item in enumInfo)
                {
                    sb.AppendLine($"* '{item.Name}' - {item.Description ?? item.Name}");
                }
            }
            schema.Description = sb.ToString();
        }


    }
}
