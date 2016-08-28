using System.Collections.Generic;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
    public abstract class VisualTreeElement : VisualTree<VisualTreeElement>
	{
		public abstract Control Control { get; }

        public Dictionary<string, object> AttachedProperties { get; set; } = new Dictionary<string, object>();

		public BindingContext BindingContext { get; set; }
	}
}

