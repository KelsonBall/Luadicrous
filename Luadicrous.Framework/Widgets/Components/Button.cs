using Luadicrous.Framework.Serialization;
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

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			Button element = new Button();
			BindClick(element, node, root);
			return new ElementPair(
				element,
				e => (Button)element.AddChild(e)
			);
		}

		private static void BindClick(Button element, XmlNode node, Control root)
		{
			XmlAttribute attribute = node.Attribute("Click");
			if (attribute.IsBinding())
			{
				Action<EventHandler> subscribe = func => element.button.Clicked += func;
				root.BindingContext.BindCommand(subscribe, "Click", attribute.Value);
			}
		}
	}
}

