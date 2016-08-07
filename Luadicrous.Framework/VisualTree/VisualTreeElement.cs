using System.Collections.Generic;
using Gtk;

namespace Luadicrous.Framework
{
	public abstract class VisualTreeElement : VisualTree<VisualTreeElement>
	{
		internal abstract Widget Widget { get; set; }

        internal Dictionary<string, object> AttachedProperties { get; set; } = new Dictionary<string, object>();

		internal BindingContext BindingContext { get; set; }
	}
}

