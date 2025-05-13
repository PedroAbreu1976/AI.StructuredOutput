using AI.StructuredOutput.Schema.Builders;

namespace AI.StructuredOutput.Schema
{
    public class SchemaBuilderFactory
    {
        private static readonly SchemaBuilderFactory instance = new SchemaBuilderFactory();
        public static SchemaBuilderFactory Instance => instance;

        private List<ISchemaBuilder> _builders { get; } = new List<ISchemaBuilder>();
        private ISchemaBuilder _defaultBuilder = new ObjectSchemaBuilder();

        private SchemaBuilderFactory()
        {
            RegisterBuilder(new IntegerSchemaBuilder());
            RegisterBuilder(new NumberSchemaBuilder());
            RegisterBuilder(new BooleanSchemaBuilder());
            RegisterBuilder(new StringSchemaBuilder());
            RegisterBuilder(new DateTimeSchemaBuilder());
            RegisterBuilder(new ArraySchemaBuilder());
            RegisterBuilder(new DateTimeSchemaBuilder());
            RegisterBuilder(new EnumSchemaBuilder());
        }

        public void RegisterBuilder(ISchemaBuilder builder)
        {
            _builders.Add(builder);
        }

        public ISchemaBuilder GetBuilder(Type type)
        {
            var result = _builders.FirstOrDefault(x => x.AppliesTo(type));
            return result ?? _defaultBuilder;
        }

        public static ResponseSchema Build(Type type)
        {
            var builder = Instance.GetBuilder(type);
            return builder.Build(type);
        }
    }
}
