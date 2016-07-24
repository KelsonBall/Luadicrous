using Luadicrous.Framework;

namespace Luadicrous
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			NLua.Lua scope = new NLua.Lua();
			scope.LoadCLRPackage();
			var app = new Luadicrous.Framework.LuadicrousApplication();
			scope["Application"] = app;
			string script = "";
#if DEBUG
			if (args.Length == 0)
			{
				script = System.IO.File.ReadAllText("../../Demo/app.lua");
				LuadicrousApplication.ApplicationDirectory = "../../Demo";
			}
#else
			if (args.Length == 0)
			{
				script = System.IO.File.ReadAllText("../app.lua");
				LuadicrousApplication.ApplicationDirectory = "../";
			}
#endif
			else
			{
				var file = new System.IO.FileInfo(args[0]);
				script = System.IO.File.ReadAllText(file.FullName);
				LuadicrousApplication.ApplicationDirectory = file.Directory.FullName;
			}
			scope.PCall(script);
			app.Run();
			return;
		}
	}
}

