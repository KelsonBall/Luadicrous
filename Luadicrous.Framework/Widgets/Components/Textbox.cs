using System.Xml;
using System;
using Gtk;

namespace Luadicrous.Framework
{
	public class Textbox : LeafElement
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

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
		{
			Textbox element = new Textbox();
			BindText(element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
				element,
				e => element
			);
		}

		private static void BindText(Textbox element, XmlNode node, Control root)
		{
			XmlAttribute attribute = (XmlAttribute)node.Attributes.GetNamedItem("Text");
			if (attribute?.Value.StartsWith("{Binding ") ?? false)
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

