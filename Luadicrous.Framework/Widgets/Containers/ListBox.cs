using NLua;
using System;
using System.Xml;
using System.Collections.Generic;

namespace Luadicrous.Framework
{
	public class ListBox : MultipleItemContainer
	{
		private Gtk.VBox listbox;

		internal override Gtk.Widget Widget
		{
			get { return listbox; }
			set { listbox = (Gtk.VBox)value; }
		}

		public ListBox ()
		{
			listbox = new Gtk.VBox (true, 0);
		}

		internal Control _header;
		internal Control Header { 
			get { return _header; }
			set 
			{ 
				RemoveChildren (_header);
				_header = value;
				AddChildren (_header);
				// Keep the header at the top of the vbox!
				Gtk.Widget temp = listbox.Children [0];
				listbox.Children [0] = _header.Widget;
				listbox.Children [listbox.Children.Length - 1] = temp;
			}
		}

		internal LuaTable _items;
		internal event Action<LuaTable> ItemsSourceChanged;
		internal LuaTable Items 
		{ 
			get { return _items; }
			set 
			{
				_items = value;
				ItemsSourceChanged?.Invoke (value);
			}
		}

		internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
		{
			ListBox element = new ListBox ();
			BindHeaderTemplate (element, node, root);
			BindItemsSource (element, node, root);
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => element
			);
		}

		private static void BindHeaderTemplate(ListBox element, XmlNode node, Control root)
		{
			var attribute = (XmlAttribute)node.Attributes.GetNamedItem ("HeaderTemplate");
			element.Header = Control.LoadFromSource (attribute.Value);
		}

		private static void BindItemsSource(ListBox element, XmlNode node, Control root)
		{
			var attribute = (XmlAttribute)node.Attributes.GetNamedItem ("ItemsSource");
			if (attribute?.Value.StartsWith ("{Binding ") ?? false)
			{
			//	root.BindingContext.BindProperty (
			//		func => element.ItemsSourceChanged += func,
			//		() => element.Items,
			//		items => element.Items = items,
			//		"ItemsSource",
			//		attribute.Value);
			} 
			else
				throw new Exception ("Items source must be bound to a table in the view model.");
		}
	}
}

