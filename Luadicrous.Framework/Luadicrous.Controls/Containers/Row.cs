using Luadicrous.Framework;
using Luadicrous.Framework.Attributes;
using Luadicrous.Framework.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace Luadicrous.Controls.Containers
{
    [VisualElement]
    [BindableCollection(source: "ItemsSource", template: nameof(Template), adder: nameof(AddChild), remover: nameof(RemoveChild))]
    public class Row : MultipleItemContainer
    {
        private TableLayoutPanel table = new TableLayoutPanel();

        public override Control Control
        {
            get { return table; }
        }

        public Row() : base()
        {
            table.Dock = DockStyle.Fill;
            //table.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            table.AutoSize = true;
            table.AutoSizeMode = AutoSizeMode.GrowAndShrink;                        
            table.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }

        private Dictionary<string, Framework.View> views = new Dictionary<string, Framework.View>();

        public string Template { get; set; }

        public void AddChild(string key, dynamic model)
        {
            //table.SuspendLayout();
            Framework.View control = LuadicrousApplication.ViewFactory.CreateView(LuadicrousApplication.SourceManager.GetView(LuadicrousApplication.GetFileInfo(Template)), key, model);
            views.Add(key, control);
            AddToTable(control.Control);
            base.AddChildren(control);
            //table.ResumeLayout();
            table.PerformLayout();
        }

        public void RemoveChild(string key, dynamic item)
        {
            //table.SuspendLayout();
            base.RemoveChildren(views[key]);
            RemoveFromTable(views[key].Control, () => --table.ColumnCount, table.GetColumn, table.SetColumn);            
            views.Remove(key);
            //table.ResumeLayout();
            table.PerformLayout();
        }

        public void AddToTable(Control item)
        {
            table.ColumnCount++;
            table.SetColumn(item, table.ColumnCount);
        }

        public void RemoveFromTable(Control item, Func<int> decrement, Func<Control, int> get, Action<Control, int> set)
        {
            int column = get(item);
            foreach (Control itemToShift in views.Where(kvPair => get(kvPair.Value.Control) >= column)
                                                 .Select(kvPair => kvPair.Value.Control))
            {
                set(itemToShift, get(itemToShift) - 1);
            }
            decrement();
        }

        public override void AddControl(VisualTreeElement element)
        {
            table.Controls.Add(element.Control);
            AddToTable(element.Control);            
            base.AddChildren(element);
        }
    }
}
