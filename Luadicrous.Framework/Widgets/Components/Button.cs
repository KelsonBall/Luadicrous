using System;
using System.Xml;

namespace Luadicrous.Framework
{
	public class Button : SingleItemContainer
	{
		private Gtk.Button button;

		internal override Gtk.Widget Widget
		{
			get { return button; }
			set { button = (Gtk.Button)value; }
		}

        public Button()
        {
            button = new Gtk.Button {
                Vexpand = false,
                Hexpand = false,
                Halign = Gtk.Align.Center,
                Valign = Gtk.Align.Center
            };                             
        }

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
		{
			Button element = new Button();
			BindClick(element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
				element,
				e => (Button)element.AddChild(e)
			);
		}

		private static void BindClick(Button element, XmlNode node, Control root)
		{
			XmlAttribute attribute = (XmlAttribute)node.Attributes.GetNamedItem("Click");
			if (attribute?.Value.StartsWith("{Binding ") ?? false)
			{
				Action<EventHandler> subscribe = func => element.button.Clicked += func;
				root.BindingContext.BindCommand(subscribe, "Click", attribute.Value);
			}
		}
	}
}

