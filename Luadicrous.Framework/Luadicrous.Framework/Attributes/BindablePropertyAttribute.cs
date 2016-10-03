namespace Luadicrous.Framework.Attributes
{
    public class BindablePropertyAttribute : BindableMemberAttribute
    {
        public readonly string ChangeHandler;

        public BindablePropertyAttribute(string handler = null) : base(handler == null ? BindingMode.FromViewModel : BindingMode.TwoWay)
        {
            ChangeHandler = handler;
        }
    }
}
