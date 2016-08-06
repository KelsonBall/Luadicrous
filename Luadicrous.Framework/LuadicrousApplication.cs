namespace Luadicrous.Framework
{
	public class LuadicrousApplication
	{		
		public static string ApplicationDirectory { get; set; }
		public static string GetApplicationDirectoryRelativeTo(string source)
		{
			return ApplicationDirectory + "/" + source;
		}
	}
}

