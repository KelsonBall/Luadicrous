using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using Luadicrous.Framework;
using Luadicrous.Framework.Attributes;
using Luadicrous.Framework.VisualTree;

namespace Luadicrous.Controls
{
    /// <summary>
    /// Arranges child elements horizontally or vertically.
    /// Can display lists of items.
    /// </summary>
    [VisualElement]
    [BindableCollection(source: "ItemsSource", template: nameof(Template), adder: nameof(AddChild), remover: nameof(RemoveChild))]
    public class List : MultipleItemContainer
    {
        private TableLayoutPanel table = new TableLayoutPanel();

        public override Control Control
        {
            get { return table; }
        }

        public List() : base()
        {

        }

        private Dictionary<string, Framework.View> views = new Dictionary<string, Framework.View>();

        public string Template { get; set; }

        public void AddChild(string key, dynamic model)
        {
            Framework.View control = LuadicrousApplication.ViewFactory.CreateView(LuadicrousApplication.SourceManager.GetView(LuadicrousApplication.GetFileInfo(Template)), key, model);
            views.Add(key, control);
            AddSegment(control.Control);
            base.AddChildren(control);
        }

        public void RemoveChild(string key, dynamic item)
        {
            base.RemoveChildren(views[key]);
            RemoveSegment(views[key].Control);
            views.Remove(key);
        }

        public void AddSegment(Control item)
        {
            switch (Orientation)
            {
                case Orientation.Vertical:
                    AddToTable(item, () => ++table.RowCount, table.SetRow);
                    break;
                case Orientation.Horizontal:
                    AddToTable(item, () => ++table.ColumnCount, table.SetColumn);
                    break;
            }
        }

        public void AddToTable(Control item, Func<int> increment, Action<Control, int> set)
        {
            set(item, increment());
        }

        public void RemoveSegment(Control item)
        {
            switch (Orientation)
            {
                case Orientation.Vertical:
                    RemoveFromTable(item, () => --table.RowCount, table.GetRow, table.SetRow);
                    break;
                case Orientation.Horizontal:
                    RemoveFromTable(item, () => --table.ColumnCount, table.GetColumn, table.SetColumn);
                    break;
            }
        }

        public void RemoveFromTable(Control item, Func<int> decrement, Func<Control, int> get, Action<Control, int> set)
        {
            int row = get(item);
            foreach (Control itemToShift in views.Where(kvPair => get(kvPair.Value.Control) >= row)
                                                 .Select(kvPair => kvPair.Value.Control))
            {
                set(itemToShift, get(itemToShift) - 1);
            }
            decrement();
        }

        public override void AddControl(VisualTreeElement element)
        {
            AddSegment(element.Control);
            table.Controls.Add(element.Control);
            base.AddChildren(element);
        }

        [BindableProperty]
        public Orientation Orientation { get; set; } = Orientation.Vertical;

        [BindableProperty]
        public Alignment ContentAlignment { get; set; } = Alignment.Top;
    }
}
