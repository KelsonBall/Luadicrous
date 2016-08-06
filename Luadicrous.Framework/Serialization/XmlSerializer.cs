using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public static class XmlSerializer
    {
        internal static VisualTreeElement Serialize(XmlNode node, Control root)
        {
            Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> parse = null;
            switch (node.Name)
            {
                case "Control":
                    parse = Control.ParseNested(node, root);
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
                return parse.Item1;
            }
            return null;
        }
    }
}
