using System;
using System.Xml;

namespace Luadicrous.Framework
{
	public class HorizontalPanel : MultipleItemContainer
	{
		private Gtk.HBox box;

		internal override Gtk.Widget Widget
		{
			get { return box; }
			set { box = (Gtk.HBox)value; }
		}

		public HorizontalPanel()
		{
			box = new Gtk.HBox();
		}

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			HorizontalPanel element = new HorizontalPanel ();
			return new ElementPair (
				element,
				e => ((HorizontalPanel)element).AddChildren(e)
			);
		}
	}
}

