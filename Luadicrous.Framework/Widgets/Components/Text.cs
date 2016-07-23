namespace Luadicrous.Framework
{
	public class Text : LeafElement
	{
		private Gtk.Entry entry;

		internal override Gtk.Widget Widget
		{
			get { return entry; }
			set { entry = (Gtk.Entry)value; }
		}

		public Text(string text = null)
		{
			if (text != null)
				entry = new Gtk.Entry(text);
			else
				entry = new Gtk.Entry();
		}
	}
}

