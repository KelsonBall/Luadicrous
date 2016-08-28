using Luadicrous.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Luadicrous.Framework.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> VisualElements(this Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.Is<VisualTreeElement>())
                {
                    continue;
                }

                if (type.GetCustomAttribute<VisualElementAttribute>() != null)
                {
                    yield return type;
                }
            }
        }
    }
}
