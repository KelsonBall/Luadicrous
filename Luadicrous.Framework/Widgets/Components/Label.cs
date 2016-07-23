using System;
using System.Xml;

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

		public static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node)
		{
			Label element = new Label ();
			XmlAttribute textAttribute = (XmlAttribute)node.Attributes.GetNamedItem ("Text");
			if (textAttribute != null)
				element.Text = textAttribute.Value;
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => element
			);
		}
	}
}

