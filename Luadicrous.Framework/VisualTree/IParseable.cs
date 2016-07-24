using System;
using System.Xml;

namespace Luadicrous.Framework
{
	internal interface IParseable<T>
	{
		Tuple<T, Func<VisualTreeElement, T>> Parse(XmlNode node, Control root);
	}
}

