using Luadicrous.Framework.Serialization;
using System;
using System.Xml;
using wFrame = System.Windows.Forms.Panel;

namespace Luadicrous.Framework
{
	public class Component : SingleItemContainer
	{
		private wFrame box;

		internal override object Widget
		{
			get { return box; }
			set { box = (wFrame)value; }
		}

		public BindingContext BindingContext;

		public Component()
		{
			box = new wFrame ();
		}

		public static Component LoadFromSource(string source)
		{
			XmlDocument document = new XmlDocument ();
			document.Load (LuadicrousApplication.GetApplicationDirectoryRelativeTo(source));
			var controlNode = document.DocumentElement;
			Component root = (Component)Component.Parse(controlNode).Item1;
			root.AddChild(XmlSerializer.Serialize(controlNode.FirstChild, root));
			return root;
		}

        public static Component LoadFromSource(string source, string key, dynamic model)
        {
            XmlDocument document = new XmlDocument();
            document.Load(LuadicrousApplication.GetApplicationDirectoryRelativeTo(source));
            var controlNode = document.DocumentElement;
            Component root = (Component)Component.Parse(controlNode, key, model).Item1;
            root.AddChild(XmlSerializer.Serialize(controlNode.FirstChild, root));
            return root;
        }

        internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node)
		{
			Component element = new Component();
			XmlAttribute bindingAttribute = (XmlAttribute)node.Attributes.GetNamedItem("BindingContext");
			if (bindingAttribute != null)
            {                
                element.BindingContext = new BindingContext(bindingAttribute.Value);
            }
				
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
				element,
				e => (Component)element.AddChild(e)
			);
		}

        internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> Parse(XmlNode node, string key, dynamic model)
        {
            Component element = new Component();
            XmlAttribute bindingAttribute = (XmlAttribute)node.Attributes.GetNamedItem("BindingContext");
            if (bindingAttribute != null)
            {
                element.BindingContext = new BindingContext(bindingAttribute.Value, key, model);
            }

            return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>>(
                element,
                e => (Component)element.AddChild(e)
            );
        }

        internal static Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> ParseNested(XmlNode node, Component root)
		{
			Component element = Component.LoadFromSource ( node.Attributes.GetNamedItem ("Source").Value );
			return new Tuple<VisualTreeElement, Func<VisualTreeElement, VisualTreeElement>> (
				element,
				e => ((Component)element).AddChild(e)
			);
		}

		
	}
}

