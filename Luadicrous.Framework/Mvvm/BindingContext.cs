using NLua;
using System;
using System.IO;

namespace Luadicrous.Framework
{
	public class BindingContext
	{
		private Lua scope;
		private LuaTable viewModel;

		public BindingContext()
		{
			scope = new Lua();
			scope.LoadCLRPackage();
		}

		public BindingContext(string source) : this()
		{			
			LoadContext(
				new FileInfo(
					LuadicrousApplication
						.GetApplicationDirectoryRelativeTo(source)
					)
			);
		}

		public void LoadContext(FileInfo file)
		{
			string text = File.ReadAllText(file.FullName);
			viewModel = (LuaTable)scope.PCall(text)[0];
		}

		public void BindProperty(Action<EventHandler> subscribe, Func<dynamic> getView, Action<dynamic> setView, string property, string bindingExpression, BindingMode mode = BindingMode.TwoWay)
		{
			string bindingPath = BindableProperty.ParseBindingExpression(bindingExpression);
			var bindableProperty = (BindableProperty)viewModel.ValueOfKey(bindingPath);
			if (bindableProperty.Get() != null)
				setView(bindableProperty.Get());
			subscribe( (sender, args) => bindableProperty.SetSilent(getView()) );
			bindableProperty.OnPropertyChanged += setView;
		}

		public void BindCommand(Action<EventHandler> subscribe, string command, string bindingExpression)
		{
			string bindingPath = BindableProperty.ParseBindingExpression(bindingExpression);
			var bindableCommand = (LuaFunction)viewModel.ValueOfKey(bindingPath);
			subscribe( (sender, e) => bindableCommand.Call() );
		}
	}
}

