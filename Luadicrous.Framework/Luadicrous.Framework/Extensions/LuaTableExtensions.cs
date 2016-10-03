using NLua;
using System.Dynamic;
using System.Collections.Generic;

namespace Luadicrous.Framework.Extensions
{
    public static class LuaTableExtensions
	{
		public static LuaTable Clone(this LuaTable table)
		{
			using (Lua scope = new Lua())
			{
				scope["a"] = table;
				return (LuaTable)scope.PCall(@"
					function copy(obj, seen)
						if type(obj) ~= 'table' then 
							return obj 
						end
					  	if seen and seen[obj] then 
							return seen[obj] 
						end
					  	local s = seen or {}
					  	local res = setmetatable({}, getmetatable(obj))
					  	s[obj] = res
					  	for k, v in pairs(obj) do 
							res[copy(k, s)] = copy(v, s) 
						end
					  	return res
					end
					return copy(a)")[0];
				
			}
		}

		public static object ValueOfKey(this LuaTable table, string key)
		{
			var keys = table.Keys.GetEnumerator();
			var values = table.Values.GetEnumerator();
			while (keys.MoveNext())
			{
				values.MoveNext();
				if (keys.Current.ToString().Equals(key))
				{
					return values.Current;
				}
			}
			return null;
		}

        public static dynamic ToDynamic(this LuaTable table)
        {
            var value = new ExpandoObject() as IDictionary<string, object>;
            var keys = table.Keys.GetEnumerator();
            var values = table.Values.GetEnumerator();
            while (keys.MoveNext())
            {
                values.MoveNext();
                value.Add((string)keys.Current, values.Current);
            }
            return (dynamic)value;
        }
	}
}

