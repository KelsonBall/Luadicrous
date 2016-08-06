using System;
using System.Xml;
using Luadicrous.Framework.Serialization;
using Table = System.Windows.Forms.TableLayoutPanel;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
    public class Grid : MultipleItemContainer, IAttachable
    {
        private Table _table;

        internal override object Widget
        {
            get { return _table; }
            set { _table = (Table)value; }
        }

        public Grid()
        {
            _table = new Table();            
        }

        internal VisualTreeElement AddItem(VisualTreeElement element)
        {
            uint row = (uint)(element.AttachedProperties.ContainsKey("Row") ? (int)element.AttachedProperties["Row"] : 0);
            uint column = (uint)(element.AttachedProperties.ContainsKey("Column") ? (int)element.AttachedProperties["Column"] : 0);
            uint rowSpan = (uint)(element.AttachedProperties.ContainsKey("RowSpan") ? (int)element.AttachedProperties["RowSpan"] : 1);
            uint columnSpan = (uint)(element.AttachedProperties.ContainsKey("columnSpan") ? (int)element.AttachedProperties["columnSpan"] : 1);            
            _table.SetColumn((Control)element.Widget, (int)column);
            _table.SetColumnSpan((Control)element.Widget, (int)columnSpan);
            _table.SetRow((Control)element.Widget, (int)row);
            _table.SetRowSpan((Control)element.Widget, (int)rowSpan);
            return this.AddChildren(element);
        }

        internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, Component root)
        {
            Grid grid = new Grid();            
            var rows = uint.Parse(node.Attributes.GetNamedItem("Rows").Value);
            grid._table.RowCount = (int)rows;
            var columns = uint.Parse(node.Attributes.GetNamedItem("Columns").Value);
            grid._table.ColumnCount = (int)columns;
            return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
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
