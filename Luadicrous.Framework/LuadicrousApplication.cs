namespace Luadicrous.Framework
{
	public class LuadicrousApplication
	{		
		public LuadicrousApplication()
		{
			Gtk.Application.Init();
		}

		public void Run()
		{
			Gtk.Application.Run();
		}

		public static void Quit()
		{
			Gtk.Application.Quit();
		}
	}
}

