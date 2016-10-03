using Luadicrous.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Luadicrous.Framework
{
    public abstract class VisualTreeElement : VisualTree<VisualTreeElement>
	{
		public abstract Control Control { get; }

        public Dictionary<string, object> AttachedProperties { get; set; } = new Dictionary<string, object>();

		public BindingContext BindingContext { get; set; }        

        [BindableProperty]
        public string Dock
        {
            get { return Control.Dock.ToString(); }
            set { Control.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), value, true); }
        }

        [BindableProperty]
        public string Anchor
        {
            get { return Control.Anchor.ToString(); }
            set { Control.Anchor = (AnchorStyles)Enum.Parse(typeof(AnchorStyles), value, true); }
        }

        [BindableProperty]
        public bool Visible
        {
            get { return Control.Visible; }
            set { Control.Visible = value; }
        }

        [BindableProperty]
        public bool Enabled
        {
            get { return Control.Enabled; }
            set { Control.Enabled = value; }
        }

        [BindableProperty]
        public string Background
        {
            get { return Control.BackColor.ToString(); }
            set
            {
                int n;
                if (value.StartsWith("#"))
                {
                    Control.BackColor = ColorTranslator.FromHtml(value);
                }                
                else if (int.TryParse(value.Substring(0,1), out n))
                {
                    int[] values = value.Split(',').Select(s => int.Parse(s)).ToArray();
                    if (values.Length == 3)
                    {
                        Control.BackColor = Color.FromArgb(255, values[0], values[1], values[2]);
                    }
                    else if (values.Length == 4)
                    {
                        Control.BackColor = Color.FromArgb(values[0], values[1], values[2], values[3]);
                    }
                    else
                    {
                        throw new FormatException("Invalid color format: " + value);
                    }
                }
                else
                {
                    Control.BackColor = Color.FromName(value);
                }
            }
        }

        [BindableProperty]
        public string Foreground
        {
            get { return Control.ForeColor.ToString(); }
            set
            {
                int n;
                if (value.StartsWith("#"))
                {
                    Control.ForeColor = ColorTranslator.FromHtml(value);
                }
                else if (int.TryParse(value.Substring(0, 1), out n))
                {
                    int[] values = value.Split(',').Select(s => int.Parse(s)).ToArray();
                    if (values.Length == 3)
                    {
                        Control.ForeColor = Color.FromArgb(255, values[0], values[1], values[2]);
                    }
                    else if (values.Length == 4)
                    {
                        Control.ForeColor = Color.FromArgb(values[0], values[1], values[2], values[3]);
                    }
                    else
                    {
                        throw new FormatException("Invalid color format: " + value);
                    }
                }
                else
                {
                    Control.ForeColor = Color.FromName(value);                    
                }
            }            
        }

        [BindableProperty]
        public string Margin
        {
            get { return Control.Margin.ToString(); }
            set
            {
                int[] data = value.Split(',').Select(s => int.Parse(s)).ToArray();
                switch (data.Length)
                {
                    case 1:
                        Control.Margin = new Padding(data[0]);
                        break;
                    case 2:
                        Control.Margin = new Padding(data[0], data[1], data[0], data[1]);
                        break;
                    case 4:                        
                        Control.Margin = new Padding(data[0], data[1], data[2], data[3]);
                        break;
                    default:
                        throw new FormatException("Invalid margin format: " + value);
                }
            }
        }

        [BindableProperty]
        public string Padding
        {
            get { return Control.Margin.ToString(); }
            set
            {
                int[] data = value.Split(',').Select(s => int.Parse(s)).ToArray();
                switch (data.Length)
                {
                    case 1:
                        Control.Padding = new Padding(data[0]);
                        break;
                    case 2:
                        Control.Padding = new Padding(data[0], data[1], data[0], data[1]);
                        break;
                    case 4:
                        Control.Padding = new Padding(data[0], data[1], data[2], data[3]);
                        break;
                    default:
                        throw new FormatException("Invalid margin format: " + value);
                }
            }
        }
    }
}

