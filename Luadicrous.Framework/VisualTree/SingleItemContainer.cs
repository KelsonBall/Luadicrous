using System.Linq;
using Gtk;
namespace Luadicrous.Framework
{
	public abstract class SingleItemContainer : VisualTreeElement
	{
		private VisualTreeElement child;

		internal VisualTreeElement AddChild(params VisualTreeElement[] children)
		{
			var childElement = children.Single();
			if (child != null)
				RemoveChild(children);
			child = childElement;
			((Gtk.Bin)Widget).Child = childElement.Widget;
			return base.AddChildren(children);
		}

		internal VisualTreeElement RemoveChild(params VisualTreeElement[] children)
		{			
			child = null;
			((Gtk.Bin)Widget).Child = null;
			return base.RemoveChildren(children);
		}
	}
}

