using Gtk;
using System;

namespace Luadicrous.Framework
{
    public static class GtkExtensions
    {
        public static void SetMargin(this Widget widget, string margin)
        {
            string[] margins = margin.Split(',');
            switch(margins.Length)
            {
                case 1:
                    int marginValue = int.Parse(margins[0]);
                    widget.Margin = marginValue;
                    widget.MarginBottom = marginValue;
                    widget.MarginLeft = marginValue;
                    widget.MarginRight = marginValue;
                    widget.MarginTop = marginValue;
                    break;
                case 2:
                    int marginH = int.Parse(margins[0]);
                    int marginV = int.Parse(margins[1]);                    
                    widget.MarginBottom = marginV;
                    widget.MarginLeft = marginH;
                    widget.MarginRight = marginH;
                    widget.MarginTop = marginV;
                    break;
                case 4:                                                            
                    widget.MarginLeft = int.Parse(margins[0]);                    
                    widget.MarginTop = int.Parse(margins[1]);
                    widget.MarginRight = int.Parse(margins[2]);
                    widget.MarginBottom = int.Parse(margins[3]);
                    break;
                default:
                    throw new ArgumentException("Invalid margin string: " + margin);
            }
        }

        public static string MarginString(this Widget widget)
        {
            return $"{widget.MarginLeft}, {widget.MarginTop}, {widget.MarginRight}, {widget.MarginBottom}";
        }
    }
}
