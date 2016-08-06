using System;
using System.Xml;

namespace Luadicrous.Framework
{
	public class VerticalPanel : MultipleItemContainer
	{
		private Gtk.VBox box;

		internal override Gtk.Widget Widget
		{
			get { return box; }
			set { box = (Gtk.VBox)value; }
		}

		internal VerticalPanel()
		{
			box = new Gtk.VBox();
		}

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			VerticalPanel element = new VerticalPanel ();
			return new ElementPair (
				element,
				e => ((VerticalPanel)element).AddChildren(e)
			);
		}
	}
}

