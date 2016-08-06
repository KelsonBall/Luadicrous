using System.Xml;
using System;
using wEntry = System.Windows.Forms.TextBox;

namespace Luadicrous.Framework
{
	public class Textbox : LeafElement
	{
		private wEntry entry;

		internal override object Widget
		{
			get { return entry; }
			set { entry = (wEntry)value; }
		}

		public Textbox(string text = null)
		{
            entry = new wEntry
            {
                Text = text ?? string.Empty,                                
            };
		}

		public string Text
		{
			get { return entry.Text; }
			set { entry.Text = value; }
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Component root)
		{
			Textbox element = new Textbox();
			BindText(element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
				element,
				e => element
			);
		}

		private static void BindText(Textbox element, XmlNode node, Component root)
		{
			XmlAttribute attribute = (XmlAttribute)node.Attributes.GetNamedItem("Text");
			if (attribute?.Value.StartsWith("{Binding ") ?? false)
			{
				Action<EventHandler> subscribe = 
					func => ((wEntry)element.Widget).TextChanged += func;
				root.BindingContext.BindProperty(subscribe, () => element.Text, text => element.Text = text, "Text", attribute.Value);
			}
			else
				element.Text = attribute?.Value;
		}
	}
}

