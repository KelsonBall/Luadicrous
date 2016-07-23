using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Luadicrous.Framework
{
	public class BindableProperty
	{
		private dynamic value;

		public BindableProperty()
		{
		}

		public Action<dynamic> OnGot;

		public dynamic Get()
		{
			OnGot?.Invoke(value);
			return value; 
		}

		public Action<dynamic> OnSet;

		public BindableProperty Set(dynamic newValue)
		{
			OnSet?.Invoke(newValue);
			value = newValue;
		}
	}
}

