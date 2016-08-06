namespace Luadicrous.Framework
{
	public abstract class MultipleItemContainer : VisualTreeElement
	{
		public new VisualTreeElement AddChildren(params VisualTreeElement[] children)
		{
			foreach (var child in children)
				(( System.Windows.Forms.Control)Widget).Controls.Add((System.Windows.Forms.Control)child.Widget);            
            return base.AddChildren(children);
		}

		public new VisualTreeElement RemoveChildren(params VisualTreeElement[] children)
		{
			foreach (var child in children)
				(( System.Windows.Forms.Control)Widget).Controls.Remove(( System.Windows.Forms.Control)child.Widget);            
            return base.RemoveChildren(children);
		}
	}
}

