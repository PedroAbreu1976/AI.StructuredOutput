using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AI.StructuredOutput.Schema.Decorators
{
    internal class RangeResponseSchemaDecorator : ResponseSchemaDecorator
    {
        public override void Decorate(ResponseSchema schema, PropertyInfo propertyInfo)
        {
            var range = propertyInfo.PropertyType.GetCustomAttribute<RangeAttribute>();
            if(range != null)
            {
                schema.Maximum = Convert.ToDouble(range.Maximum);
                schema.Minimum = Convert.ToDouble(range.Minimum);
                if(range.MaximumIsExclusive)
                    schema.ExclusiveMaximum = true;
                if (range.MinimumIsExclusive)
                    schema.ExclusiveMinimum = true;
            }
        }
    }
}
