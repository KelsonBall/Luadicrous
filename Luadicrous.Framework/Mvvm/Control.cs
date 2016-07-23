using System;
namespace Luadicrous.Framework
{
	public class Control : SingleItemContainer
	{
		private Gtk.Bin bin;

		internal override Gtk.Widget Widget
		{
			get { return bin; }
			set { bin = (Gtk.Bin)value; }
		}

		public BindingContext BindingContext;

		public Control()
		{
		}
	}
}

