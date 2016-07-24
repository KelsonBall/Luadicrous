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

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
		{
			HorizontalPanel element = new HorizontalPanel ();
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => ((HorizontalPanel)element).AddChildren(e)
			);
		}
	}
}

