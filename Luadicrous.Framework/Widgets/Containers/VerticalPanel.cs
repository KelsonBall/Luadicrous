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

		public VerticalPanel()
		{
			box = new Gtk.VBox();
		}
	}
}

