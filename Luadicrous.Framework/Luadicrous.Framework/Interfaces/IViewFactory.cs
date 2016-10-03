using System.Xml;

namespace Luadicrous.Framework.Interfaces
{
    public interface IViewFactory
    {
        IControlFactory ControlFactory { get; }

        View CreateView(XmlNode node);

        View CreateView(XmlNode node, string key, dynamic model);
    }
}
