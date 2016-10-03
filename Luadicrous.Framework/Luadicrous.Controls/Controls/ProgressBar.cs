using Luadicrous.Framework;
using Luadicrous.Framework.Attributes;
using System.Windows.Forms;
using Bar = System.Windows.Forms.ProgressBar;
namespace Luadicrous.Controls.Controls
{
    [VisualElement]
    public class ProgressBar : LeafElement
    {
        private Bar progress = new Bar();

        public override Control Control
        {
            get { return progress; }
        }    

        [BindableProperty]
        public int Value
        {
            get { return progress.Value; }
            set { progress.Value = value; }
        }

        [BindableProperty]
        public int Max
        {
            get { return progress.Maximum; }
            set { progress.Maximum = value; }
        }

        [BindableProperty]
        public int Min
        {
            get { return progress.Minimum; }
            set { progress.Minimum = value; }
        }
    }
}
