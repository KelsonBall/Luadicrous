namespace Luadicrous.Framework.VisualTree
{
    public abstract class SingleItemContainer : VisualTreeElement, IContainer
	{
		private VisualTreeElement child;

		public VisualTreeElement AddChild(params VisualTreeElement[] children)
		{
            return base.AddChildren(children);
		}

        public VisualTreeElement RemoveChild(params VisualTreeElement[] children)
		{
            return base.RemoveChildren(children);
		}

        public abstract void AddControl(VisualTreeElement element);
    }
}

