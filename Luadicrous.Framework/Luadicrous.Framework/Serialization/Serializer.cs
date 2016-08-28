using System;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public static class Serializer
    {
        public static VisualTreeElement Serialize(View view, Type control, XmlNode node)
        {
            VisualTreeElement element = (VisualTreeElement)Activator.CreateInstance(control);

            element.BindingContext = view.BindingContext;

            element.Bind(node);

            return element;
        }

    }
}
