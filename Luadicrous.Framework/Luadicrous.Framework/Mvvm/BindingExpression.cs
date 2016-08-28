namespace Luadicrous.Framework.Mvvm
{
    public class BindingExpression
    {
        public readonly string Source;
        public readonly string Target;
        public readonly bool IsDatabound;

        public BindingExpression(string source, string target, bool isDatabound)
        {
            Source = source;
            Target = target;
            IsDatabound = isDatabound;
        }
    }
}
