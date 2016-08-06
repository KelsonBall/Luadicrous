using System;
using System.Xml;
using Gtk;

namespace Luadicrous.Framework.Serialization
{
    public static class BindWidget
    {
        public static void BindWidgetProperties(this VisualTreeElement element, XmlNode node, Control root)
        {
            element.BindMargin(node, root);
            element.BindAlignment(node, root);
            element.BindExpand(node, root);
            element.BindSize(node, root);

        }

        public static void BindMargin(this VisualTreeElement element, XmlNode node, Control root)
        {
            XmlAttribute attribute = node.Attribute("Margin");
            if (attribute != null)
            {
                if (attribute.IsBinding())
                {
                    Action<EventHandler> subscribe = func => { };
                    root.BindingContext.BindProperty(
                        subscribe,
                        () => element.Widget.MarginString(),
                        value => element.Widget.SetMargin((string)value),
                        "Margin",
                        attribute.Value);
                }
                else
                {
                    element.Widget.SetMargin(attribute.Value);
                }
            }
        }

        public static void BindAlignment(this VisualTreeElement element, XmlNode node, Control root)
        {
            XmlAttribute halign = node.Attribute("HorizontalAlignment");
            if (halign != null)
            {
                if (halign.IsBinding())
                {
                    throw new NotImplementedException();
                }
                else
                {
                    element.Widget.Halign = (Align)Enum.Parse(typeof(Align), halign.Value);
                }
            }
            XmlAttribute valign = node.Attribute("VerticalAlignment");
            if (valign != null)
            {
                if (valign.IsBinding())
                {
                    throw new NotImplementedException();
                }
                else
                {
                    element.Widget.Valign = (Align)Enum.Parse(typeof(Align), valign.Value);
                }
            }
        }

        public static void BindExpand(this VisualTreeElement element, XmlNode node, Control root)
        {
            XmlAttribute hexpand = node.Attribute("HorizontalExpansion");
            if (hexpand != null)
            {
                if (hexpand.IsBinding())
                {
                    throw new NotImplementedException();                    
                }
                else
                {
                    element.Widget.Hexpand = bool.Parse(hexpand.Value);
                }
            }
            XmlAttribute vexpand = node.Attribute("VerticalExpansion");
            if (vexpand != null)
            {
                if (vexpand.IsBinding())
                {
                    throw new NotImplementedException();
                }
                else
                {
                    element.Widget.Vexpand = bool.Parse(vexpand.Value);
                }
            }
        }

        public static void BindSize(this VisualTreeElement element, XmlNode node, Control root)
        {
            XmlAttribute height = node.Attribute("Height");
            if (height != null)
            {
                if (height.IsBinding())
                {
                    throw new NotImplementedException();
                }
                else
                {                    
                    element.Widget.HeightRequest = int.Parse(height.Value);
                }
            }

            XmlAttribute width = node.Attribute("Widght");
            if (width != null)
            {
                if (width.IsBinding())
                {
                    throw new NotImplementedException();
                }
                else
                {                    
                    element.Widget.WidthRequest = int.Parse(width.Value);
                }
            }

        }
    }
}
