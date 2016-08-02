using NLua;
using System;
using System.Xml;
using System.Collections.Generic;

namespace Luadicrous.Framework
{
    public class ListBox : MultipleItemContainer
    {
        private Dictionary<string, Control> items = new Dictionary<string, Control>();

        private Gtk.VBox listbox;

        internal override Gtk.Widget Widget
        {
            get { return listbox; }
            set { listbox = (Gtk.VBox)value; }
        }

        public ListBox()
        {
            listbox = new Gtk.VBox(false, 0);
        }

        internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Control root)
        {
            ListBox element = new ListBox();
            BindItemsSource(element, node, root);
            return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
                element,
                e => element
            );
        }

        private static void BindItemsSource(ListBox element, XmlNode node, Control root)
        {
            var attribute = (XmlAttribute)node.Attributes.GetNamedItem("ItemsSource");
            var itemTemplate = (XmlAttribute)node.Attributes.GetNamedItem("Template");
            if (attribute?.Value.StartsWith("{Binding ") ?? false)
            {
                root.BindingContext.BindCollection(
                    (k, e) => element.RemoveListItem(k, e),
                    (k, e) => element.AddListItem(itemTemplate.Value, k, e),
                    "ItemsSource",
                    attribute.Value);
            }
            else
            {
                throw new Exception("Items source must be bound to a table in the view model.");
            }
        }

        private void AddListItem(string template, string key, dynamic item)
        {
            Control listItem = Control.LoadFromSource(template, key, item);
            items[key] = listItem;
            this.AddChildren(listItem);
        }

        private void RemoveListItem(string key, dynamic item)
        {
            this.RemoveChildren(items[key]);
        }
    }
}

