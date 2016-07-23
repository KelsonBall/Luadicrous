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
	}
}

