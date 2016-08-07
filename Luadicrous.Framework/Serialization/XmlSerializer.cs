using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Luadicrous.Framework.Widgets.Components;

namespace Luadicrous.Framework.Serialization
{
    public static class XmlSerializer
    {
        internal static VisualTreeElement Serialize(XmlNode node, Control root)
        {
            ElementPair parse = null;
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
                case "Calendar":
                    parse = Calendar.Parse(node, root);
                    break;
				case "Panel":
					parse = Panel.Parse (node, root);
					break;
				case "VerticalSlider":
					parse = VerticalSlider.Parse (node, root);
					break;
				case "HorizontalSlider":
					parse = HorizontalSlider.Parse (node, root);
					break;
                default:
                    break;
            }
            if (parse != null)
            {
                parse.Element.BindWidgetProperties(node, root);
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Comment)
                        continue;
                    var nextElement = Serialize(child, root);
                    if (parse.Element is IAttachable)
                        ((IAttachable)parse.Element).AttachProperties(nextElement, child);
                    if (nextElement != null)
                        parse.AddToElement(nextElement);
                }
                return parse.Element;
            }
            return null;
        }
    }
}
