using NLua;

namespace Luadicrous.Framework.Extensions
{
    public static class LuaExtensions
	{
		public static object[] PCall(this Lua scope, string script)
		{
			try
			{
				return scope.DoString(script);
			}
			catch (NLua.Exceptions.LuaException luaException)
			{				
				throw luaException;
			}
		}
	}
}

