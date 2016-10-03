using Luadicrous.Framework;
using Luadicrous.Framework.Attributes;
using System.Windows.Forms;
using FormsLabel = System.Windows.Forms.Label;

namespace Luadicrous.Controls
{
    [VisualElement]
    public class Label : LeafElement
    {
        private FormsLabel label = new FormsLabel();
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
