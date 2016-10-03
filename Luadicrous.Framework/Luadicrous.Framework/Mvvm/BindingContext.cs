using Luadicrous.Framework.Extensions;
using Luadicrous.Framework.Mvvm;
using NLua;
using System;
using System.Collections.Generic;

namespace Luadicrous.Framework
{
	public class BindingContext
	{
		private SourceAwareLuaScope scope;
		private IDictionary<string, object> viewModel;
		private string[] currentScipt;

		public BindingContext(SourceAwareLuaScope source)
		{
            scope = source;
		}

        public BindingContext(SourceAwareLuaScope source, string key, dynamic model)
        {
            scope = source;
        }

		public void LoadContext(string key, dynamic model)
		{
            if (model is LuaTable)
            {
                model = ((LuaTable)model).ToDynamic();
            }
            scope?.Execute();
            LuaFunction func = scope.LuaScope["ViewModel"] as LuaFunction;
            viewModel = ((LuaTable)func.Call(new object[] { key, ((object)model).Copy() })[0]).ToDynamic();
		}

        public void LoadContext()
        {
            scope?.Execute();
            LuaFunction func = scope.LuaScope["ViewModel"] as LuaFunction;
            viewModel = ((LuaTable)func.Call()[0]).ToDynamic();
        }

		public void BindProperty(Action<Action> subscribe, Func<dynamic> getView, Action<dynamic> setView, string property, string bindingExpression, BindingMode mode = BindingMode.TwoWay)
		{
			var bindableProperty = (BindableProperty)viewModel[bindingExpression];
            if (bindableProperty.Get() != null)
            {
                setView(bindableProperty.Get());
            }
            else
            {
                bindableProperty.Set(getView());
            }
			subscribe( () => bindableProperty.SetSilent( getView() ) );
			bindableProperty.OnPropertyChanged += setView;
		}

        public void BindCollection(Action<string, dynamic> removeFromView, Action<string, dynamic> addToView, BindingExpression bindingExpression)
        {
            var bindableCollection = (BindableCollection)viewModel[bindingExpression.Target];
            foreach (string key in bindableCollection.Contents.Keys)
            {
                LuaTable table = (LuaTable)bindableCollection.Contents[key];
                addToView(key, table.ToDynamic());
            }
            bindableCollection.OnAdded += addToView;
            bindableCollection.OnRemoved += removeFromView;
        }

        public void BindCommand(Action<Action> subscribe, string command, string bindingExpression)
		{
			var bindableCommand = (LuaFunction)viewModel[bindingExpression];
			subscribe( () => bindableCommand.Call() );
		}
	}
}