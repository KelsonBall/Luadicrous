using Luadicrous.Framework.Attributes;
using Luadicrous.Framework.VisualTree;
using System.Windows.Forms;
using System;

namespace Luadicrous.Framework
{
    [VisualElement]
    public class View : SingleItemContainer
    {
        private UserControl form;

        public override Control Control
        {
            get { return form; }
        }

        public new BindingContext BindingContext;

        public View(BindingContext context)
        {
            form = new UserControl();
            form.Dock = DockStyle.Fill;
            BindingContext = context;
        }

        public override void AddControl(VisualTreeElement element)
        {
            form.Controls.Add(element.Control);
            AddChild(element);
        }
    }
}

