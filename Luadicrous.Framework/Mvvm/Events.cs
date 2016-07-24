using System.Collections.Generic;
using NLua;

namespace Luadicrous.Framework
{
	public class Events
	{
		private static Dictionary<string, Events> events = new Dictionary<string, Events>();

		public static Events GetChannel(string channelId)
		{
			if (!events.ContainsKey(channelId))
				events[channelId] = new Events();
			return events[channelId];
		}


		internal Events()
		{
		}

		private List<LuaFunction> subscribers = new List<LuaFunction>();

		public Events Subscribe(LuaFunction function)
		{
			subscribers.Add(function);
			return this;
		}

		public Events Publish(params dynamic[] args)
		{
			foreach (LuaFunction func in subscribers)
				func.Call(args);
			return this;
		}
	}
}

