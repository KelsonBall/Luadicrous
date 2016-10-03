using Luadicrous.Framework;
using Luadicrous.Framework.Attributes;
using System;
using System.Windows.Forms;

namespace Luadicrous.Controls.Controls
{
    [VisualElement]
    public class Slider : LeafElement
    {
        private TrackBar slider = new TrackBar();
        public override Control Control
        {
            get { return slider; }
        }

        public Slider() : base()
        {
            slider.ValueChanged += (sender, args) => ValueChanged?.Invoke();
        }

        [BindableProperty(nameof(ValueChanged))]
        public int Value
        {
            get { return slider.Value; }
            set { slider.Value = value; }
        }

        [BindableEvent]
        public Action ValueChanged { get; set; }

        [BindableProperty]
        public int Max
        {
            get { return slider.Maximum; }
            set { slider.Maximum = value; }
        }

        [BindableProperty]
        public int Min
        {
            get { return slider.Minimum; }
            set { slider.Minimum = value; }
        }
    }
}
