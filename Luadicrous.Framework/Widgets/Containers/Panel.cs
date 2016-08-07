using System;
using Gtk;
using System.Xml;
using Luadicrous.Framework.Serialization;

namespace Luadicrous.Framework
{
	internal class Panel : MultipleItemContainer, IAttachable
	{
		private Gtk.Fixed _frame;

		internal override Gtk.Widget Widget 
		{
			get { return _frame; }
			set { _frame = (Gtk.Fixed)value; }
		}

		internal Panel ()
		{
			_frame = new Gtk.Fixed ();
		}

		internal VisualTreeElement AddItem(VisualTreeElement element)
		{
			int x = BindPanelX (element);
			int y = BindPanelY (element);
			_frame.Put (element.Widget, x, y);
			this.PlaceItem (element, x, y);
			return this.AddChildren (element);
		}

		private int BindPanelX(VisualTreeElement element)
		{
			object x = element.AttachedProperties ["X"];
			if (x is string)
			{
				element.AttachedProperties ["X"] = 0;
				Action<EventHandler> subscribe = func => { };
				element.BindingContext.BindProperty (
					subscribe,
					() => (int)element.AttachedProperties ["X"],
					value => this.PlaceItem (element, (int)value, element.AttachedProperties ["Y"] as int?),
					"Panel.X",
					(string)x);
			}
			return (int)element.AttachedProperties ["X"];
		}

		private int BindPanelY(VisualTreeElement element)
		{
			object y = element.AttachedProperties ["Y"];
			if (y is string)
			{
				element.AttachedProperties ["Y"] = 0;
				Action<EventHandler> subscribe = func => { };
				this.BindingContext.BindProperty (
					subscribe,
					() => (int)element.AttachedProperties ["Y"],
					value => this.PlaceItem (element, element.AttachedProperties["X"] as int?, (int)value),
					"Panel.Y",
					(string)y);
			}
			return (int)element.AttachedProperties ["Y"];
		}

		internal VisualTreeElement PlaceItem(VisualTreeElement element, int? xIn, int? yIn)
		{
			int x = xIn ?? 0;
			int y = yIn ?? 0;
			if (xIn != null)
			{
				element.AttachedProperties ["X"] = x;
			}
			if (yIn != null)
			{
				element.AttachedProperties ["Y"] = y;
			}
			_frame.Put (element.Widget, x, y);
			_frame.Move (element.Widget, x, y);
			return this;
		}

		void IAttachable.AttachProperties(VisualTreeElement element, XmlNode node)
		{
			this.AttachX (element, node);
			this.AttachY (element, node);
		}

		internal void AttachX(VisualTreeElement element, XmlNode node)
		{
			var posX = node.Attribute ("Panel.X");
			if (posX != null)
			{
				int position;
				if (int.TryParse (posX.Value, out position))
				{
					element.AttachedProperties.Add ("X", position);
				} else
				{
					element.AttachedProperties.Add ("X", posX.Value);
				}
			} 
			else
			{
				element.AttachedProperties.Add ("X", 0);
			}
		}

		internal void AttachY(VisualTreeElement element, XmlNode node)
		{
			var posY = node.Attribute ("Panel.Y");
			if (posY != null)
			{
				int position;
				if (int.TryParse (posY.Value, out position))
				{
					element.AttachedProperties.Add ("Y", position);
				} else
				{
					element.AttachedProperties.Add ("Y", posY.Value);
				}
			} 
			else
			{
				element.AttachedProperties.Add ("Y", 0);
			}
		}

		internal static ElementPair Parse(XmlNode node, Control root)
		{
			Panel panel = new Panel ();
			panel.BindingContext = root.BindingContext;

			return new ElementPair (
				panel,
				e => panel.AddItem (e));
		}
	}
}

