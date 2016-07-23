using System;
using System.Collections.Generic;
using Gtk;

namespace Luadicrous.Framework
{
	public abstract class VisualTreeElement : VisualTree<VisualTreeElement>
	{
		internal abstract Widget Widget { get; set; }
	}
}

