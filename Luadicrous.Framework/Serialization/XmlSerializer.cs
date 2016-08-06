using System;
using System.Windows.Forms;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public static class XmlSerializer
    {
        internal static VisualTreeElement Serialize(XmlNode node, Component root)
        {
            Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> parse = null;
            switch (node.Name)
            {
                case "Component":
                    parse = Component.ParseNested(node, root);
                    break;
                case "VerticalPanel":
                    parse = VerticalPanel.Parse(node, root);
                    break;
                case "HorizontalPanel":
                    parse = HorizontalPanel.Parse(node, root);
                    break;
                case "Button":
                    parse = Button.Parse(node, root);
                    break;
                case "Label":
                    parse = Label.Parse(node, root);
                    break;
                case "Text":
                    parse = Textbox.Parse(node, root);                    
                    break;
                case "ListBox":
                    parse = ListBox.Parse(node, root);
                    break;
                case "Grid":
                    parse = Grid.Parse(node, root);
                    break;
                default:
                    break;
            }
            if (parse != null)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Comment)
                        continue;
                    var nextElement = Serialize(child, root);
                    if (parse.Item1 is IAttachable)
                        ((IAttachable)parse.Item1).AttachProperties(nextElement, child);
                    if (nextElement != null)
                        parse.Item2(nextElement);
                }
                ((Control)parse.Item1.Widget)?.Show();
                return parse.Item1;
            }
            return null;
        }
    }
}
