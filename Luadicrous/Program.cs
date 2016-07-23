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

			if (args.Length == 0)
				state.DoFile(@"C:\Users\kelso\Desktop\form.lua");			
			else
				state.DoFile(args[0]);
			
			app.Run();
			return;
		}
	}
}

