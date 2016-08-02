using NLua;
using System;
using System.Collections.Generic;
using System.IO;

namespace Luadicrous.Framework
{
	public class BindingContext
	{
		private Lua scope;
		private IDictionary<string, object> viewModel;

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

        public BindingContext(string source, string key, dynamic model) : this()
        {
            LoadContext(
                new FileInfo(
                    LuadicrousApplication
                        .GetApplicationDirectoryRelativeTo(source)
                ),
                key,
                model
            );
        }

		public void LoadContext(FileInfo file, string key, dynamic model)
		{
            if (model is LuaTable)
                model = ((LuaTable)model).ToDynamic();
			string text = File.ReadAllText(file.FullName);
            scope.PCall(text);
            LuaFunction func = scope["ViewModel"] as LuaFunction;            
            viewModel = ((LuaTable)func.Call(new object[] { key, ((object)model).Copy() })[0]).ToDynamic();
		}

        public void LoadContext(FileInfo file)
        {
            string text = File.ReadAllText(file.FullName);
            scope.PCall(text);
            LuaFunction func = scope["ViewModel"] as LuaFunction;
            viewModel = ((LuaTable)func.Call()[0]).ToDynamic();
        }

		public void BindProperty(Action<EventHandler> subscribe, Func<dynamic> getView, Action<dynamic> setView, string property, string bindingExpression, BindingMode mode = BindingMode.TwoWay)
		{
			string bindingPath = BindableProperty.ParseBindingExpression(bindingExpression);
			var bindableProperty = (BindableProperty)viewModel[bindingPath];
			if (bindableProperty.Get() != null)
				setView(bindableProperty.Get());
			subscribe( (sender, args) => bindableProperty.SetSilent( getView() ) );
			bindableProperty.OnPropertyChanged += setView;
		}

        public void BindCollection(Action<string, dynamic> removeFromView, Action<string, dynamic> addToView, string property, string bindingExpression, BindingMode mode = BindingMode.TwoWay)
        {
            string bindingPath = BindableProperty.ParseBindingExpression(bindingExpression);
            var bindableCollection = (BindableCollection)viewModel[bindingPath];
            foreach (string key in bindableCollection.Contents.Keys)
            {
                LuaTable table = (LuaTable)bindableCollection.Contents[key];
                addToView(key, table.ToDynamic());
            }
            bindableCollection.OnAdded += addToView;
            bindableCollection.OnRemoved += removeFromView;
        }

        public void BindCommand(Action<EventHandler> subscribe, string command, string bindingExpression)
		{
			string bindingPath = BindableProperty.ParseBindingExpression(bindingExpression);
			var bindableCommand = (LuaFunction)viewModel[bindingPath];
			subscribe( (sender, e) => bindableCommand.Call() );
		}
	}
}