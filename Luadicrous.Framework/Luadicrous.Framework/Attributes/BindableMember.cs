using System;

namespace Luadicrous.Framework.Attributes
{
    public class BindableMemberAttribute : Attribute
    {
        public readonly BindingMode Mode;
        public BindableMemberAttribute(BindingMode mode = BindingMode.TwoWay)
        {
            this.Mode = mode;
        }
    }
}
