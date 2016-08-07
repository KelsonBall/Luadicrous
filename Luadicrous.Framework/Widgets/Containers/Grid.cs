using System;
using System.Xml;
using Gtk;
using Luadicrous.Framework.Serialization;

namespace Luadicrous.Framework
{
    public class Grid : MultipleItemContainer, IAttachable
    {
        private Table _table;

        internal override Widget Widget
        {
            get { return _table; }
            set { _table = (Table)value; }
        }

        internal VisualTreeElement AddItem(VisualTreeElement element)
        {
            uint row = (uint)(element.AttachedProperties.ContainsKey("Row") ? (int)element.AttachedProperties["Row"] : 0);
            uint column = (uint)(element.AttachedProperties.ContainsKey("Column") ? (int)element.AttachedProperties["Column"] : 0);
            uint rowSpan = (uint)(element.AttachedProperties.ContainsKey("RowSpan") ? (int)element.AttachedProperties["RowSpan"] : 1);
            uint columnSpan = (uint)(element.AttachedProperties.ContainsKey("columnSpan") ? (int)element.AttachedProperties["columnSpan"] : 1);
            _table.Attach(element.Widget, column, column + columnSpan, row, row + rowSpan);
            return AddChildren(element);
        }

        internal static ElementPair Parse(XmlNode node, Control root)
        {			
            Grid grid = new Grid();            
			grid.BindingContext = root.BindingContext;
            var rows = uint.Parse(node.Attributes.GetNamedItem("Rows").Value);
            var columns = uint.Parse(node.Attributes.GetNamedItem("Columns").Value);
            grid._table = new Table(rows, columns, true);
            return new ElementPair(
                    grid,
                    e => grid.AddItem(e)
                );
        }

        void IAttachable.AttachProperties(VisualTreeElement element, XmlNode node)
        {
            var gridRow = node.Attribute("Grid.Row");
            if (gridRow != null)
                element.AttachedProperties.Add("Row", int.Parse(gridRow.Value));
            var gridColumn = node.Attribute("Grid.Column");
            if (gridColumn != null)
                element.AttachedProperties.Add("Column", int.Parse(gridColumn.Value));
            var gridRowSpan = node.Attribute("Grid.RowSpan");
            if (gridRowSpan != null)
                element.AttachedProperties.Add("RowSpan", int.Parse(gridRowSpan.Value));
            var gridColumnSpan = node.Attribute("Grid.ColumnSpan");
            if (gridColumnSpan != null)
                element.AttachedProperties.Add("ColumnSpan", int.Parse(gridColumnSpan.Value));
            
        }
    }
}
