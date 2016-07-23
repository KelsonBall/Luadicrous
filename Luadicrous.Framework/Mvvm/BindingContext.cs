using System.IO;

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

		public void LoadContext(FileInfo file)
		{
			scope.DoFile(file.FullName);
		}
	}
}

