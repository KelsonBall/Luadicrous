namespace Luadicrous.Framework
{
	public class Window : SingleItemContainer
	{
		private Gtk.Window window;

		internal override Gtk.Widget Widget
		{
			get { return window; }
			set { window = (Gtk.Window)value; }
		}

		public Window(string title = null)
		{
			window = new Gtk.Window(title);
			window.Resize (400, 300);
			window.DeleteEvent += (i, j) => LuadicrousApplication.Quit();
		}

		public Window Render()
		{
			window.ShowAll();
			return this;
		}
	}
}

