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

		public BindableProperty(dynamic start)
		{
			value = start;
		}

		public Action<dynamic> OnGot;

		public dynamic Get()
		{
			OnGot?.Invoke(value);
			return value; 
		}

		public Action<dynamic> OnSet;

		internal Action<dynamic> OnPropertyChanged;

		public BindableProperty Set(dynamic newValue)
		{
			if (value == newValue)
				return this;			
			value = newValue;
			OnSet?.Invoke(newValue);
			OnPropertyChanged?.Invoke(newValue);
			return this;
		}

		internal BindableProperty SetSilent(dynamic newValue)
		{
			if (value == newValue)
				return this;
			value = newValue;
			OnSet?.Invoke(newValue);
			return this;
		}

		public static string ParseBindingExpression(string bindingExpression)
		{
			string exp = bindingExpression.Split(' ')[1];
			return exp.Substring(0, exp.Length - 1);
		}
	}
}

