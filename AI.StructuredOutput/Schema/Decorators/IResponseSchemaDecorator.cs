using System.Reflection;

namespace AI.StructuredOutput.Schema.Decorators
{
    public interface IResponseSchemaDecorator
    {
        void Decorate(ResponseSchema schema, PropertyInfo propertyInfo);
    }

    internal abstract class ResponseSchemaDecorator : IResponseSchemaDecorator
    {
        public abstract void Decorate(ResponseSchema schema, PropertyInfo propertyInfo);
    }
}
