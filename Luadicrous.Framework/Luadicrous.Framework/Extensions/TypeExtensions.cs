using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Luadicrous.Framework.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetCustomAttribute<TAttribute>() != null;
        }

        public static IEnumerable<MemberInfo> MembersWithAttribute<TAttribute>(this Type type, BindingFlags flags) where TAttribute : Attribute
        {
            MemberInfo[] members = type.GetMembers(flags);
            foreach (MemberInfo member in members)
            {
                if (member.GetCustomAttribute<TAttribute>() != null)
                {
                    yield return member;
                }
            }
        }

        public static bool Is<TType>(this Type type)
        {
            Type compareType = typeof(TType);
            Type baseType = type;

            Func<Type, Type, bool> compare;
            if (compareType.IsInterface)
            {
                compare = (tBase, tCompare) => tBase.GetInterfaces().Contains<Type>(tCompare);
            }
            else
            {
                compare = (tBase, tCompare) => tBase.Equals(tCompare);

            }

            do
            {
                if (compare(baseType, compareType))
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            while (baseType != null);

            return false;
        }
    }
}
