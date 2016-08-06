using System;
using System.Xml;
using wLabel = System.Windows.Forms.Label;

namespace Luadicrous.Framework
{
	public class Label : LeafElement
	{
		private wLabel label;

		internal override object Widget
		{
			get { return label; }
			set { label = (wLabel)value; }
		}

        public Label(string text = null)
        {
            label = new wLabel
            {
                Text = text ?? string.Empty,
                AutoSize = true
            };
		}

		public string Text 
		{ 
			get { return label.Text; } 
			set { label.Text = value; } 
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Component root)
		{
			Label element = new Label ();
			BindText(element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => element
			);
		}

		private static void BindText(Label element, XmlNode node, Component root)
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

