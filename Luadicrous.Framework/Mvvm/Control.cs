using Luadicrous.Framework.Serialization;
using System;
using System.Xml;

namespace Luadicrous.Framework
{
	public class Control : SingleItemContainer
	{
		private Gtk.Frame box;

		internal override Gtk.Widget Widget
		{
			get { return box; }
			set { box = (Gtk.Frame)value; }
		}

		public BindingContext BindingContext;

		public Control()
		{
			box = new Gtk.Frame ();
			box.BorderWidth = 0;
			box.ShadowType = Gtk.ShadowType.None;
		}

		public static Control LoadFromSource(string source)
		{
			XmlDocument document = new XmlDocument ();
			document.Load (LuadicrousApplication.GetApplicationDirectoryRelativeTo(source));
			var controlNode = document.DocumentElement;
			Control root = (Control)Control.Parse(controlNode).Element;
			root.AddChild(XmlSerializer.Serialize(controlNode.FirstChild, root));
			return root;
		}

        public static Control LoadFromSource(string source, string key, dynamic model)
        {
            XmlDocument document = new XmlDocument();
            document.Load(LuadicrousApplication.GetApplicationDirectoryRelativeTo(source));
            var controlNode = document.DocumentElement;
            Control root = (Control)Control.Parse(controlNode, key, model).Element;
            root.AddChild(XmlSerializer.Serialize(controlNode.FirstChild, root));
            return root;
        }

        internal static ElementPair Parse(XmlNode node)
		{
			Control element = new Control();
			XmlAttribute bindingAttribute = node.Attribute("BindingContext");
			if (bindingAttribute != null)
            {                
                element.BindingContext = new BindingContext(bindingAttribute.Value);
            }
				
			return new ElementPair(
				element,
				e => (Control)element.AddChild(e)
			);
		}

        internal static ElementPair Parse(XmlNode node, string key, dynamic model)
        {
            Control element = new Control();
            XmlAttribute bindingAttribute = node.Attribute("BindingContext");
            if (bindingAttribute != null)
            {
                element.BindingContext = new BindingContext(bindingAttribute.Value, key, model);
            }

            return new ElementPair(
                element,
                e => (Control)element.AddChild(e)
            );
        }

        internal static ElementPair ParseNested(XmlNode node, Control root)
		{
			Control element = Control.LoadFromSource ( node.Attributes.GetNamedItem ("Source").Value );
			return new ElementPair (
				element,
				e => ((Control)element).AddChild(e)
			);
		}

		
	}
}

