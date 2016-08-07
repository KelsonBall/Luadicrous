using System;

namespace Luadicrous.Framework
{
	public class LuadicrousApplication
	{		
		public LuadicrousApplication()
		{
			Gtk.Application.Init();
			GLib.ExceptionManager.UnhandledException += (GLib.UnhandledExceptionArgs args) => {
				throw (Exception)args.ExceptionObject;
			};
		}

		public void Run()
		{
			Gtk.Application.Run();
		}

		public static void Quit()
		{
			Gtk.Application.Quit();
		}

		public static string ApplicationDirectory { get; set; }
		public static string GetApplicationDirectoryRelativeTo(string source)
		{
			return ApplicationDirectory + "/" + source;
		}
	}
}

