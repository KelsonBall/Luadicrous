using Luadicrous.Framework.Attributes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
    public abstract class LeafElement : VisualTreeElement
	{		
        protected LeafElement()
        {
            Control.SizeChanged += (sender, args) =>
            {
                SizeChanged?.Invoke();
                WidthChanged?.Invoke();
                HeightChanged?.Invoke();
                BottomChanged?.Invoke();
                RightChanged?.Invoke();
            };
        }

        [BindableProperty(nameof(WidthChanged))]
        public int Width
        {
            get { return Control.Width; }
            set { Control.Width = value; }
        }

        public Action WidthChanged;

        [BindableProperty(nameof(HeightChanged))]
        public int Height
        {
            get { return Control.Height; }
            set { Control.Height = value; }
        }

        public Action HeightChanged;

        [BindableProperty]
        public int Top
        {
            get { return Control.Top; }
            set { Control.Top = value; }
        }

        [BindableProperty]
        public int Left
        {
            get { return Control.Left; }
            set { Control.Left = value; }
        }

        [BindableProperty(nameof(BottomChanged))]
        public int Bottom
        {
            get { return Control.Bottom; }
        }

        public Action BottomChanged;

        [BindableProperty(nameof(RightChanged))]
        public int Right
        {
            get { return Control.Right; }
        }

        public Action RightChanged;

        [BindableProperty(nameof(SizeChanged))]
        public Size Size
        {
            get { return Control.Size; }
            set { Control.Size = value; }
        }

        public Action SizeChanged;

        public ContextMenu ContextMenu
        {
            get { return Control.ContextMenu; }
            set { Control.ContextMenu = value; }
        }
    }
}

