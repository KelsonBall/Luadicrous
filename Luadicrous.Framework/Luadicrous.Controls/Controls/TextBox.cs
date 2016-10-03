using System;
using Luadicrous.Framework.Attributes;
using System.Drawing;
using System.Windows.Forms;
using Luadicrous.Framework;
using FormsTextBox = System.Windows.Forms.TextBox;

namespace Luadicrous.Controls
{
    [VisualElement]
    public class TextBox : LeafElement
    {
        private FormsTextBox text = new FormsTextBox();

        public override Control Control { get { return text; } }

        public TextBox() : base()
        {
            text.TextChanged += (sender, args) => TextChanged?.Invoke();
        }

        [BindableProperty(nameof(TextChanged))]
        public string Text
        {
            get { return text.Text; }
            set { text.Text = value; }
        }

        [BindableEvent]
        public Action TextChanged { get; set; }

        [BindableProperty]
        public bool Multiline
        {
            get { return text.Multiline; }
            set { text.Multiline = value; }
        }

        [BindableProperty]
        public int MaxLength
        {
            get { return text.MaxLength; }
            set { text.MaxLength = value; }
        }

        [BindableProperty]
        public Size MinimumSize
        {
            get { return text.MinimumSize; }
            set { text.MinimumSize = value; }
        }

        [BindableProperty]
        public Size MaximumSize
        {
            get { return text.MaximumSize; }
            set { text.MaximumSize = value; }
        }

        [BindableProperty(nameof(TextChanged))]
        public int SelectionLength
        {
            get { return text.SelectionLength; }
            set { text.SelectionLength = value; }
        }

        [BindableProperty(nameof(TextChanged))]
        public int SelectionStart
        {
            get { return text.SelectionStart; }
            set { text.SelectionStart = value; }
        }

        [BindableProperty(nameof(TextChanged))]
        public string SelectedText
        {
            get { return text.SelectedText; }
        }

        [BindableProperty]
        public bool Visible
        {
            get { return text.Visible; }
            set { text.Visible = value; }
        }
    }
}
