using Luadicrous.Framework.Attributes;
using System;
using MetroFramework.Controls;
using System.Windows.Forms;
using Luadicrous.Framework;

namespace Luadicrous.Controls
{
    [VisualElement]
    public class Button : LeafElement
    {
        private MetroButton button = new MetroButton();
        public override Control Control
        {
            get { return button; }
        }

        public Button() : base()
        {
            button.Click += (sender, args) => Click?.Invoke();
        }

        [BindableEvent]
        public Action Click { get; set; }
    }
}
