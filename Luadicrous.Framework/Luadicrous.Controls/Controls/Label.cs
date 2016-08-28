using Luadicrous.Framework;
using Luadicrous.Framework.Attributes;
using MetroFramework.Controls;
using System.Windows.Forms;

namespace Luadicrous.Controls
{
    [VisualElement]
    public class Label : LeafElement
    {
        private MetroLabel label = new MetroLabel();
        public override Control Control
        {
            get { return label; }
        }

        [BindableProperty]
        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }
    }
}
