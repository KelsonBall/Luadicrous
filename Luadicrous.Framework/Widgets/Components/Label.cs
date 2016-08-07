using System;
using System.Xml;
using Gtk;
using Luadicrous.Framework.Serialization;

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

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			Label element = new Label ();
			element.BindingContext = root.BindingContext;
			BindText(element, node, root);
			return new ElementPair (
				element,
				e => element
			);
		}

		internal static void BindText(Label element, XmlNode node, Control root)
		{
			XmlAttribute attribute = node.Attribute("Text");
			if (attribute.IsBinding())
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

