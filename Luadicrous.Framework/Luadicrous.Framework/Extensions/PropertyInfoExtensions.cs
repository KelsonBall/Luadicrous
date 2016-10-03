using System.Reflection;
using System;
using Luadicrous.Framework.Converters;

namespace Luadicrous.Framework.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static Func<T> CreateGetDelegate<T>(this PropertyInfo info, object instance)
        {
            Type propertyType = info.PropertyType;
            var generatedDelegate = info.GetGetMethod().CreateDelegate(typeof(Func<>).MakeGenericType(propertyType), instance);
            return () => (T)generatedDelegate.DynamicInvoke();
        }

        public static Action<T> CreateSetDelegate<T>(this PropertyInfo info, object instance)
        {
            Type propertyType = info.PropertyType;
            var generatedDelegate = info.GetSetMethod().CreateDelegate(typeof(Action<>).MakeGenericType(propertyType), instance);
            return data => generatedDelegate.DynamicInvoke(BindingConverter.ConvertBindingValue(propertyType, data));
        }
    }
}
