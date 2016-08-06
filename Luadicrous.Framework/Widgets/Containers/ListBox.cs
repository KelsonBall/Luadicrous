using System;
using System.Xml;
using System.Collections.Generic;
using wList = System.Windows.Forms.FlowLayoutPanel;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
    public class ListBox : MultipleItemContainer
    {
        private Dictionary<string, Component> items = new Dictionary<string, Component>();

        private wList listbox;

        internal override object Widget
        {
            get { return listbox; }
            set { listbox = (wList)value; }
        }        

        public ListBox()
        {
            listbox = new wList
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
            ListBox element = new ListBox();
            BindItemsSource(element, node, root);
            return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
                element,
                e => element
            );
        }

        private static void BindItemsSource(ListBox element, XmlNode node, Component root)
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
            Component listItem = Component.LoadFromSource(template, key, item);
            items[key] = listItem;
            this.AddChildren(listItem);
        }

        private void RemoveListItem(string key, dynamic item)
        {
            this.RemoveChildren(items[key]);
        }
    }
}

