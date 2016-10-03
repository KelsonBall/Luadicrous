using Luadicrous.Framework.Mvvm;
using System;
using System.Xml;

namespace Luadicrous.Framework.Extensions
{
    public static class XmlExtensions
    {
        public static XmlAttribute Attribute(this XmlNode node, string name)
        {
            return (XmlAttribute)node.Attributes.GetNamedItem(name);
        }

        public static bool IsBinding(this XmlAttribute attribute)
        {
            return attribute?.Value.StartsWith("{Binding ") ?? false;
        }

        public static BindingExpression BindingExpression(this XmlAttribute attribute)
        {
            if (attribute.IsBinding())
            {
                string[] segments = attribute.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string target = segments[1];
                if (target.EndsWith("}"))
                {
                    target = target.Substring(0, target.Length - 1);
                }
                return new BindingExpression(attribute.Value, target, true);
            }

            return new BindingExpression(attribute.Value, string.Empty, false);
        }
    }
}
