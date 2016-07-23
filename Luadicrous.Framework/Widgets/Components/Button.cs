namespace Luadicrous.Framework
{
	public class Button : SingleItemContainer
	{
		private Gtk.Button button;

		internal override Gtk.Widget Widget
		{
			get { return button; }
			set { button = (Gtk.Button)value; }
		}

		public Button()
		{
			button = new Gtk.Button();
		}
	}
}

