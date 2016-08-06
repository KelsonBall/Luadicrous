using System.Collections.Generic;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
	public abstract class VisualTreeElement : VisualTree<VisualTreeElement>
	{
		internal abstract object Widget { get; set; }

        internal Dictionary<string, object> AttachedProperties { get; set; } = new Dictionary<string, object>();        
	}
}

