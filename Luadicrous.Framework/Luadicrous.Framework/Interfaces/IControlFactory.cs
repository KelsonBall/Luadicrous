using System.Xml;

namespace Luadicrous.Framework.Interfaces
{
    public interface IControlFactory
    {
        VisualTreeElement CreateControl(View view, XmlNode node);
    }
}
