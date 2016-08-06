using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luadicrous.Framework
{
    internal class ElementPair
    {
        public readonly VisualTreeElement Element;
        public readonly Func<VisualTreeElement, VisualTreeElement> AddToElement;

        public ElementPair(VisualTreeElement element, Func<VisualTreeElement, VisualTreeElement> add)
        {
            Element = element;
            AddToElement = add;
        }
    }
}
