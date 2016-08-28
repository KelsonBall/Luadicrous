using System;

namespace Luadicrous.Framework.Converters
{
    public enum TypeType
    {
        String ,
        Integer,
        Decimal,
        Enum
    }

    public static class BindingConverter
    {
        public static dynamic ConvertBindingValue(Type to, string value)
        {
            switch (getTypeType(to))
            {
                case TypeType.Integer:
                    return Convert.ChangeType(int.Parse(value), to);
                case TypeType.Decimal:
                    return Convert.ChangeType(decimal.Parse(value), to);
                case TypeType.Enum:
                    return Enum.Parse(to, value);
                default:
                    return value;
            }
        }

        private static TypeType getTypeType(Type to)
        {
            if (to == typeof(int) || to == typeof(byte) || to == typeof(short) || to == typeof(long))
            {
                return TypeType.Integer;
            }
            if (to == typeof(float) || to == typeof(double) || to == typeof(decimal))
            {
                return TypeType.Decimal;
            }
            if (to.IsEnum)
            {
                return TypeType.Enum;
            }
            return TypeType.String;
        }
    }
}
