namespace Luadicrous.Framework
{
	public class BindingContext
	{
		private NLua.Lua scope;

		public BindingContext()
		{
			scope = new NLua.Lua();
			scope.LoadCLRPackage();
		}

		public void LoadContext(System.IO.FileInfo file)
		{
			NLua.LuaTable table = (NLua.LuaTable)scope.DoFile(file.FullName)[0];
		}
	}
}

