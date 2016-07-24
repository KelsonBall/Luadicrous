﻿using System;
using NLua;

namespace Luadicrous.Framework
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
			var keyEnum = table.Keys.GetEnumerator();
			var valEnum = table.Values.GetEnumerator();
			while (keyEnum.MoveNext())
			{
				valEnum.MoveNext();
				if (keyEnum.Current.ToString().Equals(key))
				{
					return valEnum.Current;
				}
			}
			return null;
		}
	}
}

