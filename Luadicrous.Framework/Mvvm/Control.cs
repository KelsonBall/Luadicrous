using System;
using System.Xml;

namespace Luadicrous.Framework
{
	public class Control : SingleItemContainer
	{
		private Gtk.Frame box;

		internal override Gtk.Widget Widget
		{
			get { return box; }
			set { box = (Gtk.Frame)value; }
		}

		public BindingContext BindingContext;

		public Control()
		{
			box = new Gtk.Frame ();
			box.BorderWidth = 0;
			box.ShadowType = Gtk.ShadowType.None;
		}

		public static Control LoadFromSource(string source)
		{
			XmlDocument document = new XmlDocument ();
			document.Load (LuadicrousApplication.GetApplicationDirectoryRelativeTo(source));
			var controlNode = document.DocumentElement;
			Control root = (Control)Control.Parse(controlNode).Item1;
			root.AddChild(Serialize(controlNode.FirstChild, root));
			return root;
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node)
		{
			Control element = new Control();
			XmlAttribute bindingAttribute = (XmlAttribute)node.Attributes.GetNamedItem("BindingContext");
			if (bindingAttribute != null)
				element.BindingContext = new BindingContext(bindingAttribute.Value);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
				element,
				e => (Control)element.AddChild(e)
			);
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> ParseNested(XmlNode node, Control root)
		{
			Control element = Control.LoadFromSource ( node.Attributes.GetNamedItem ("Source").Value );
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => ((Control)element).AddChild(e)
			);
		}

		private static VisualTreeElement Serialize(XmlNode node, Control root)
		{			
			Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> parse = null;
			switch (node.Name)
			{
			case "Control":
				parse = Control.ParseNested (node, root);
				break;
			case "VerticalPanel":
				parse = VerticalPanel.Parse (node, root);
				break;
			case "HorizontalPanel":
				parse = HorizontalPanel.Parse (node, root);
				break;
			case "Button":
				parse = Button.Parse(node, root);
				break;
			case "Label":
				parse = Label.Parse (node, root);
				break;
			case "Text":
				parse = Textbox.Parse (node, root);
				break;
			case "DrawingArea":
				parse = DrawingArea.Parse (node, root);

				break;
			default:
				break;
			}
			if (parse != null)
			{
				foreach (XmlNode child in node.ChildNodes)
				{
					if (child.NodeType == XmlNodeType.Comment)
						continue;
					var nextElement = Serialize(child, root);
					if (nextElement != null)
						parse.Item2 (nextElement);
				}
				return parse.Item1;
			}
			return null;
		}
	}
}

