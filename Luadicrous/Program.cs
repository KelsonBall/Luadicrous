namespace Luadicrous
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			NLua.Lua state = new NLua.Lua();
			state.LoadCLRPackage();
			var app = new Luadicrous.Framework.LuadicrousApplication();
			state["Application"] = app;
			Luadicrous.Framework.LuadicrousApplication.ApplicationDirectory = "Demo";
			string text = System.IO.File.ReadAllText ("Demo/app.lua");
			if (args.Length == 0)
				state.DoString(text);
			else
				state.DoFile(args[0]);
			
			app.Run();
			return;
		}
	}
}

