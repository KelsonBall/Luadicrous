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

		internal static Tuple<Control, Func<VisualTreeElement, Control>> Parse(XmlNode node)
		{
			Control element = new Control();
			XmlAttribute bindingAttribute = (XmlAttribute)node.Attributes.GetNamedItem("BindingContext");
			if (bindingAttribute != null)
				element.BindingContext = new BindingContext(bindingAttribute.Value);
			return new Tuple<Control, Func<VisualTreeElement, Control>>(
				element,
				e => (Control)element.AddChild(e)
			);
		}

		private static VisualTreeElement Serialize(XmlNode node, Control root)
		{
			VisualTreeElement element = null;
			Func<VisualTreeElement, VisualTreeElement> addChild = null;
			switch (node.Name)
			{
			case "Control":
				element = Control.LoadFromSource(node.Attributes.GetNamedItem("Source").Value);
				addChild = e => element;
				break;
			case "VerticalPanel":
				element = new VerticalPanel ();
				addChild = e => ((VerticalPanel)element).AddChildren (e);
				break;
			case "HorizontalPanel":
				element = new HorizontalPanel ();
				addChild = e => ((HorizontalPanel)element).AddChildren (e);
				break;
			case "Button":
				var parsedButton = Button.Parse(node, root);
				element = parsedButton.Item1;
				addChild = parsedButton.Item2;
				break;
			case "Label":
				var parsedLabel = Label.Parse (node, root);
				element = parsedLabel.Item1;
				addChild = parsedLabel.Item2;
				break;
			case "Text":
				var parsedTextbox = Textbox.Parse(node, root);
				element = parsedTextbox.Item1;
				addChild = parsedTextbox.Item2;
				break;
			default:
				break;
			}
			if (element != null)
			{
				foreach (XmlNode child in node.ChildNodes)
				{
					var nextElement = Serialize(child, root);
					if (nextElement != null)
						addChild (nextElement);
				}
			}
			return element;
		}
	}
}

