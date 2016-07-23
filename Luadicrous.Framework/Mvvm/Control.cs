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
			return (Control)Serialize(document.DocumentElement);
		}

		private static VisualTreeElement Serialize(XmlNode node)
		{
			VisualTreeElement element = null;
			Func<VisualTreeElement, VisualTreeElement> addChild = null;
			switch (node.Name)
			{
			case "Control":
				element = new Control ();
				addChild = e => ((Control)element).AddChild (e);
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
				element = new Button ();
				addChild = e => ((Button)element).AddChild (e);
				break;
			case "Label":
				var parsed = Label.Parse (node);
				element = parsed.Item1;
				addChild = parsed.Item2;
				break;
			case "Text":
				element = new Text ();
				addChild = e => element;
				break;
			default:
				break;
			}
			if (element != null)
			{
				foreach (XmlNode child in node.ChildNodes)
				{
					var nextElement = Serialize(child);
					if (nextElement != null)
						addChild (nextElement);
				}
			}
			return element;
		}
	}
}

