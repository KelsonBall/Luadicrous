using Gtk;
using System;
using System.Xml;
using System.Collections.Generic;
using Luadicrous.Framework.Serialization;

namespace Luadicrous.Framework
{
    internal class ListBox : MultipleItemContainer
    {
        private Dictionary<string, Control> items = new Dictionary<string, Control>();

        private VBox listbox;

        internal override Widget Widget
        {
            get { return listbox; }
            set { listbox = (VBox)value; }
        }        

        internal ListBox()
        {
            listbox = new VBox();                       
        }

        internal static ElementPair Parse(XmlNode node, Control root)
        {
            ListBox element = new ListBox();
			element.BindingContext = root.BindingContext;
            BindItemsSource(element, node, root);
            return new ElementPair(
                element,
                e => element
            );
        }

        private static void BindItemsSource(ListBox element, XmlNode node, Control root)
        {
            var attribute = node.Attribute("ItemsSource");
            var itemTemplate = node.Attribute("Template");
            if (attribute.IsBinding())
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
            Action<Widget> pack = widget => listbox.PackStart(widget, false, false, 0);
            this.PackChildren(pack, listItem);            
        }

        private void RemoveListItem(string key, dynamic item)
        {
            this.RemoveChildren(items[key]);
        }
    }
}

