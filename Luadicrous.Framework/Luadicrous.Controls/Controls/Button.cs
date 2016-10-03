using Luadicrous.Framework.Attributes;
using System;
using System.Windows.Forms;
using Luadicrous.Framework;
using FormsButton = System.Windows.Forms.Button;
namespace Luadicrous.Controls
{
    [VisualElement]
    public class Button : LeafElement
    {
        private FormsButton button = new FormsButton();
        public override Control Control
        {
            get { return button; }
        }

        public Button() : base()
        {
            button.Click += (sender, args) => Click?.Invoke();
        }

        [BindableProperty]
        public string Text
        {
            get { return button.Text; }
            set { button.Text = value; }
        }

        [BindableEvent]
        public Action Click { get; set; }
    }
}
