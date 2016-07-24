using System;
using System.Xml;
using Gtk;

namespace Luadicrous.Framework
{
	public class Label : LeafElement
	{
		private Gtk.Label label;

		internal override Gtk.Widget Widget
		{
			get { return label; }
			set { label = (Gtk.Label)value; }
		}

		public Label(string text = null)
		{
			if (text != null)
				label = new Gtk.Label(text);
			else
				label = new Gtk.Label();
		}

		public string Text 
		{ 
			get { return label.Text; } 
			set { label.Text = value; } 
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
		{
			Label element = new Label ();
			BindText(element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => element
			);
		}

		private static void BindText(Label element, XmlNode node, Control root)
		{
			XmlAttribute attribute = (XmlAttribute)node.Attributes.GetNamedItem("Text");
			if (attribute?.Value.StartsWith("{Binding ") ?? false)
			{
				Action<EventHandler> subscribe = func => { };
				root.BindingContext.BindProperty(subscribe, 
				                         () => element.Text, 
				                         text => element.Text = text, 
				                         "Text", 
				                         attribute.Value);
			}
			else
				element.Text = attribute?.Value;
		}
	}
}

