using Luadicrous.Framework.Extensions;
using Luadicrous.Framework.Interfaces;
using Luadicrous.Framework.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public class ControlFactory : IControlFactory
    {
        private IDictionary<string, Type> types;

        public ControlFactory(IEnumerable<Assembly> assemblies)
        {
            types = assemblies.SelectMany(asm => asm.VisualElements()).ToDictionary(t => t.Name);
        }

        public VisualTreeElement CreateControl(View view, XmlNode node)
        {
            VisualTreeElement element;
            if (node.Name.Equals(nameof(View)))
            {
                element = LuadicrousApplication.ViewFactory.CreateView(node);
            }
            else
            {
                element = (VisualTreeElement)Activator.CreateInstance(types[node.Name]);
                element.BindingContext = view.BindingContext;
                element.Bind(node);
                if (element is IContainer)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (types.ContainsKey(child.Name))
                        {
                            ((IContainer)element).AddControl(CreateControl(view, child));
                        }
                    }
                }
            }
            return element;
        }
    }
}
