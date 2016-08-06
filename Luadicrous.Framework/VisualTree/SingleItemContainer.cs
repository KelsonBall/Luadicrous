using System.Linq;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
	public abstract class SingleItemContainer : VisualTreeElement
	{
		private VisualTreeElement child;

		public VisualTreeElement AddChild(params VisualTreeElement[] children)
		{
			var childElement = children.Single();
			if (child != null)
				RemoveChild(children);
			child = childElement;
            ((Control)Widget).Controls.Clear();
            ((Control)Widget).Controls.Add((Control)childElement.Widget);
			return base.AddChildren(children);
		}

		public VisualTreeElement RemoveChild(params VisualTreeElement[] children)
		{			
			child = null;
            ((Control)Widget).Controls.Clear();
			return base.RemoveChildren(children);
		}
	}
}

