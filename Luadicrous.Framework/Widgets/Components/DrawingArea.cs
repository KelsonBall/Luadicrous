using System;
using System.Xml;

namespace Luadicrous.Framework
{
	public class DrawingArea : LeafElement
	{
		private Gtk.DrawingArea drawingArea;

		internal override Gtk.Widget Widget 
		{ 
			get { return (Gtk.Widget)drawingArea; } 
			set { drawingArea = (Gtk.DrawingArea)value; } 
		}

		public DrawingArea ()
		{
			
		}			

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
		{
			DrawingArea element = new DrawingArea ();
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => element
			);
		}
	}
}

