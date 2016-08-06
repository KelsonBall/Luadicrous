using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public static class XmlExtensions
    {
        public static bool IsBinding(this XmlAttribute attribute)
        {
            return attribute?.Value.StartsWith("{Binding ") ?? false;
        }
    }
}
