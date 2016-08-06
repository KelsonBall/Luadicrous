using System;
using System.Xml;
using wButton = System.Windows.Forms.Button;

namespace Luadicrous.Framework
{
	public class Button : LeafElement
	{
		private wButton button;

		internal override object Widget
		{
			get { return button; }
			set { button = (wButton)value; }
		}

		public Button()
		{
			button = new wButton
            {
                AutoSize = true               
            };                                   
        }

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Component root)
		{
			Button element = new Button();
			BindClick(element, node, root);
            BindText(element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
				element,
				e => element
			);
		}

        private static void BindText(Button element, XmlNode node, Component root)
        {
            XmlAttribute attribute = (XmlAttribute)node.Attributes.GetNamedItem("Text");
            element.button.Text = attribute?.Value;            
        }

		private static void BindClick(Button element, XmlNode node, Component root)
		{
			XmlAttribute attribute = (XmlAttribute)node.Attributes.GetNamedItem("Click");
			if (attribute?.Value.StartsWith("{Binding ") ?? false)
			{
				Action<EventHandler> subscribe = func => element.button.MouseClick += (sender, args) => func(sender, args);
				root.BindingContext.BindCommand(subscribe, "Click", attribute.Value);
			}
		}
	}
}

