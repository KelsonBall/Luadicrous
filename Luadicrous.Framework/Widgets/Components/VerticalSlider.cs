using System;
using Gtk;
using System.Xml;
using Luadicrous.Framework.Serialization;

namespace Luadicrous.Framework
{
	public class VerticalSlider : LeafElement
	{
		private Gtk.VScale _scale;

		internal override Widget Widget
		{
			get { return _scale; }
			set { _scale = (Gtk.VScale)value; }
		}

		public VerticalSlider ()
		{
			_scale = new VScale (0, 1, 0.1);
		}

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			VerticalSlider slider = new VerticalSlider ();
			slider.BindingContext = root.BindingContext;
			slider.BindValue (node);
			return new ElementPair (
				slider,
				e => slider);
		}

		internal void BindValue(XmlNode node)
		{
			XmlAttribute valueAttribute = node.Attribute ("Value");
			if (valueAttribute != null)
			{
				if (valueAttribute.IsBinding ())
				{
					Gtk.Range range = (Gtk.Range)Widget;
					Action<EventHandler> subscribe = func => range.ValueChanged += func;
					BindingContext.BindProperty (
						subscribe,
						() => range.Value,
						value => range.Value = value,
						"Value",
						valueAttribute.Value);
				} 
				else
				{
					throw new NotImplementedException ("What possible reason could you have for doing this?");
				}
			}
		}
	}
}

