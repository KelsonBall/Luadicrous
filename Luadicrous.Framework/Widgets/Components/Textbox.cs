using System.Xml;
using System;
using Gtk;
using Luadicrous.Framework.Serialization;

namespace Luadicrous.Framework
{
	internal class Textbox : LeafElement
	{
		private Gtk.Entry entry;

		internal override Gtk.Widget Widget
		{
			get { return entry; }
			set { entry = (Gtk.Entry)value; }
		}

		public Textbox(string text = null)
		{
			if (text != null)
				entry = new Gtk.Entry(text);
			else
				entry = new Gtk.Entry();
		}

		public string Text
		{
			get { return entry.Text; }
			set { entry.Text = value; }
		}

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			Textbox element = new Textbox();
			element.BindingContext = root.BindingContext;
			BindText(element, node, root);
			return new ElementPair(
				element,
				e => element
			);
		}

		private static void BindText(Textbox element, XmlNode node, Control root)
		{
			XmlAttribute attribute = node.Attribute("Text");
			if (attribute.IsBinding())
			{
				Action<EventHandler> subscribe = 
					func => ((IEditable)element.Widget).Changed += func;
				root.BindingContext.BindProperty(subscribe, () => element.Text, text => element.Text = text, "Text", attribute.Value);
			}
			else
				element.Text = attribute?.Value;
		}
	}
}

