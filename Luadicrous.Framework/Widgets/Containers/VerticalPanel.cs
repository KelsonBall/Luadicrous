using System;
using System.Xml;
using wVBox = System.Windows.Forms.FlowLayoutPanel;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
	public class VerticalPanel : MultipleItemContainer
	{
		private wVBox box;

		internal override object Widget
		{
			get { return box; }
			set { box = (wVBox)value; }
		}

		internal VerticalPanel()
		{
            box = new wVBox()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Margin = new Padding(2)
            };
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Component root)
		{
			VerticalPanel element = new VerticalPanel ();
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => ((VerticalPanel)element).AddChildren(e)
			);
		}
	}
}

