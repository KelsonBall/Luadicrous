using System.Reflection;
using System;

namespace Luadicrous.Framework.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static Func<T> CreateGetDelegate<T>(this PropertyInfo info, object instance)
        {
            return (Func<T>)info.GetGetMethod().CreateDelegate(typeof(Func<T>), instance);
        }

        public static Action<T> CreateSetDelegate<T>(this PropertyInfo info, object instance)
        {
            return (Action<T>)info.GetSetMethod().CreateDelegate(typeof(Action<T>), instance);
        }
    }
}
