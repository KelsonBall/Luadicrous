namespace Luadicrous.Framework.VisualTree
{
    public abstract class MultipleItemContainer : VisualTreeElement, IContainer
	{
		public override VisualTreeElement AddChildren(params VisualTreeElement[] children)
		{
            return base.AddChildren(children);
		}

		public override VisualTreeElement RemoveChildren(params VisualTreeElement[] children)
		{
            return base.RemoveChildren(children);
		}

        public abstract void AddControl(VisualTreeElement element);
    }
}

