namespace Luadicrous.Framework
{
	public class Label : LeafElement
	{
		private Gtk.Label label;

		internal override Gtk.Widget Widget
		{
			get { return label; }
			set { label = (Gtk.Label)value; }
		}

		public Label(string text = null)
		{
			if (text != null)
				label = new Gtk.Label(text);
			else
				label = new Gtk.Label();
		}
	}
}

