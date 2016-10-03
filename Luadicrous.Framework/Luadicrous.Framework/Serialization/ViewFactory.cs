using Luadicrous.Framework.Extensions;
using Luadicrous.Framework.Interfaces;
using System;
using System.Xml;

namespace Luadicrous.Framework.Serialization
{
    public class ViewFactory : IViewFactory
    {
        public IControlFactory ControlFactory { get; private set; }

        public ViewFactory(IControlFactory controlFactory)
        {
            ControlFactory = controlFactory;
        }

        /// <summary>
        /// Takes an xml node and returns a view / viewmodel pair.
        /// </summary>
        /// <param name="node"> Serialized view. </param>
        /// <returns> A view object with bound BindingContext. </returns>
        /// TODO: Unit Test
        public View CreateView(XmlNode node)
        {
            var source = LuadicrousApplication.SourceManager.GetScript(LuadicrousApplication.GetFileInfo(node.Attribute("ViewModel").Value));
            BindingContext context = new BindingContext(source);
            context.LoadContext();
            View view = new View(context);
            view.Control.SuspendLayout();
            foreach (XmlNode child in node.ChildNodes)
            {
                if (!child.Name.StartsWith("#"))
                {
                    view.AddControl(ControlFactory.CreateControl(view, child));
                }
            }
            view.Control.ResumeLayout();
            view.Control.PerformLayout();
            return view;
        }

        /// <summary>
        /// Takes an xml file and returns a view / viewmodel pair, witht he viewmodel initialized from the provided key and model.
        /// </summary>
        /// <param name="node"> Serialized view. </param>
        /// <param name="key"> Unique key in containing collection. </param>
        /// <param name="model"> Data from initializer. </param>
        /// <returns> A view object with bound BindingContext. </returns>
        /// TODO: Unit Test
        public View CreateView(XmlNode node, string key, dynamic model)
        {
            throw new NotImplementedException();
        }
    }
}
