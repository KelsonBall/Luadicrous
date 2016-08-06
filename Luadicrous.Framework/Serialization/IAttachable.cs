using System.Collections.Generic;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    internal interface IAttachable
    {
        void AttachProperties(VisualTreeElement element, XmlNode node);
    }
}
