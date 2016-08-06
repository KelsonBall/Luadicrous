using System;
using System.Xml;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
	public class HorizontalPanel : MultipleItemContainer
	{
		private FlowLayoutPanel box;

		internal override object Widget
		{
			get { return box; }
			set { box = (FlowLayoutPanel)value; }
		}

		public HorizontalPanel()
		{
            box = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true,
                Margin = new Padding(2)
            };
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Component root)
		{
			HorizontalPanel element = new HorizontalPanel ();
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => ((HorizontalPanel)element).AddChildren(e)
			);
		}
	}
}

