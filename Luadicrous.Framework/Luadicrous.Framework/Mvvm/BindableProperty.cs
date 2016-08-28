using System;

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

		public Action<dynamic> OnPropertyChanged;

		public BindableProperty Set(dynamic newValue)
		{
			if (value == newValue)
				return this;			
			value = newValue;
			OnSet?.Invoke(newValue);
			OnPropertyChanged?.Invoke(newValue);
			return this;
		}

		public BindableProperty SetSilent(dynamic newValue)
		{
			if (value == newValue)
				return this;
			value = newValue;
			OnSet?.Invoke(newValue);
			return this;
		}
	}
}

