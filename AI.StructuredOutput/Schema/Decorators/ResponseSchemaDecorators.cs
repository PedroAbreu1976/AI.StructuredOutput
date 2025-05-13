using System.Reflection;

namespace AI.StructuredOutput.Schema.Decorators
{
    public class ResponseSchemaDecorators: IResponseSchemaDecorator
    {
        private readonly static ResponseSchemaDecorators instance = new();
        public static ResponseSchemaDecorators Instance => instance;

        private List<IResponseSchemaDecorator> _decorators = new();

        private ResponseSchemaDecorators()
        {
            RegisterDecorator(new NullableResponseSchemaDecorator());
            RegisterDecorator(new DescriptionResponseSchemaDecorator());
            RegisterDecorator(new RangeResponseSchemaDecorator());
        }

        public void RegisterDecorator(IResponseSchemaDecorator decorator)
        {
            _decorators.Add(decorator);
        }

        public void Decorate(ResponseSchema schema, PropertyInfo propertyInfo)
        {
            foreach (var decorator in _decorators)
            {
                decorator.Decorate(schema, propertyInfo);
            }
        }
    }
}
