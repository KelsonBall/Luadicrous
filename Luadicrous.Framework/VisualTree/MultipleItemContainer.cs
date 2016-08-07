using System;

namespace Luadicrous.Framework
{
	public abstract class MultipleItemContainer : VisualTreeElement
	{
		internal new VisualTreeElement AddChildren(params VisualTreeElement[] children)
		{
			foreach (var child in children)
				((Gtk.Container)Widget).Add(child.Widget);
            ((Gtk.Container)Widget).ShowAll();
            return base.AddChildren(children);
		}

        internal VisualTreeElement PackChildren(Action<Gtk.Widget> pack, params VisualTreeElement[] children)
        {
            foreach (var child in children)
                pack(child.Widget);
            ((Gtk.Container)Widget).ShowAll();
            return base.AddChildren(children);
        }

		internal new VisualTreeElement RemoveChildren(params VisualTreeElement[] children)
		{
			foreach (var child in children)
				((Gtk.Container)Widget).Remove(child.Widget);
            ((Gtk.Container)Widget).ShowAll();
            return base.RemoveChildren(children);
		}
	}
}

