namespace Luadicrous.Framework.Attributes
{
    /// <summary>
    /// Indicates a collection that the view serializer should bind to a BindableCollection type.
    /// </summary>
    public class BindableCollectionAttribute : BindableMemberAttribute
    {
        public readonly string Source;
        public readonly string TemplateProperty;
        public readonly string AdderName;
        public readonly string RemoverName;

        public BindableCollectionAttribute(string source, string template, string adder, string remover)
        {
            Source = source;
            TemplateProperty = template;
            AdderName = adder;
            RemoverName = remover;
        }
    }
}
