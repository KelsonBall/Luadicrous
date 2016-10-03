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
        public static dynamic ConvertBindingValue(Type to, object value)
        {
            if (value == null)
                return null;

            if (value.GetType() == typeof(string))
            {
                string data = (string)value;
                switch (getTypeType(to))
                {
                    case TypeType.Integer:
                        return Convert.ChangeType(int.Parse(data), to);
                    case TypeType.Decimal:
                        return Convert.ChangeType(decimal.Parse(data), to);
                    case TypeType.Enum:
                        return Enum.Parse(to, data);
                    default:
                        return value;
                }
            }
            else if (to == typeof(string))
            {
                return value.ToString();
            }
            return Convert.ChangeType(value, to);
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
