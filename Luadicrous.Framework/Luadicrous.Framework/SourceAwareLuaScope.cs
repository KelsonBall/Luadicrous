using NLua;
using System;

namespace Luadicrous.Framework
{
    public class SourceAwareLuaScope
    {
        public Lua LuaScope { get; private set; }
        public string[] Source { get; private set; }
        public Action Execute { get; private set; }

        public SourceAwareLuaScope(string source)
        {
            LuaScope = new Lua();
            LuaScope.LoadCLRPackage();
            LuaScope.DoString("import 'Luadicrous.Framework'");
            string[] Source = source.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            LuaScope.DebugHook += (sender, args) =>
            {
                LuadicrousApplication.DebugHook?.Invoke(Source, sender, args);
            };
            LuaScope.SetDebugHook(NLua.Event.EventMasks.LUA_MASKALL, 0);
            Execute = () =>
            {
                LuaScope.DoString(source);
                Execute = null;
            };
        }
    }
}
