using System.Numerics;

namespace Polokus.Core.Helpers
{
    public static class TypeHelper
    {
        public enum TypeId
        {
            None = 0,
            Byte = 1,
            Int16 = 2,
            Int32 = 3,
            Int64 = 4,
            SByte = 5,
            UInt16 = 6,
            UInt32 = 7,
            UInt64 = 8,
            BigInteger = 9,
            Decimal = 10,
            Double = 11,
            Single = 12,
            Bool = 13,
            Char = 14,
            String = 15,
        }

        public static TypeId GetTypeId(Type type)
        {
            if (type == typeof(Byte)) { return TypeId.Byte; }
            if (type == typeof(Int16)) { return TypeId.Int16; }
            if (type == typeof(Int32)) { return TypeId.Int32; }
            if (type == typeof(Int64)) { return TypeId.Int64; }
            if (type == typeof(SByte)) { return TypeId.SByte; }
            if (type == typeof(UInt16)) { return TypeId.UInt16; }
            if (type == typeof(UInt32)) { return TypeId.UInt32; }
            if (type == typeof(UInt64)) { return TypeId.UInt64; }
            if (type == typeof(BigInteger)) { return TypeId.BigInteger; }
            if (type == typeof(Decimal)) { return TypeId.Decimal; }
            if (type == typeof(Double)) { return TypeId.Double; }
            if (type == typeof(Single)) { return TypeId.Single; }
            if (type == typeof(bool)) { return TypeId.Bool; }
            if (type == typeof(char)) { return TypeId.Char; }
            if (type == typeof(string)) { return TypeId.String; }

            throw new Exception("Not supported conversion.");
        }

        public static object Convert(string value, TypeId type)
        {
            Func<string, object> converter = type switch
            {
                TypeId.Byte => (str) => System.Convert.ToByte(str),
                TypeId.Int16 => (str) => System.Convert.ToInt16(str),
                TypeId.Int32 => (str) => System.Convert.ToInt32(str),
                TypeId.Int64 => (str) => System.Convert.ToInt64(str),
                TypeId.SByte => (str) => System.Convert.ToSByte(str),
                TypeId.UInt16 => (str) => System.Convert.ToUInt16(str),
                TypeId.UInt32 => (str) => System.Convert.ToUInt32(str),
                TypeId.UInt64 => (str) => System.Convert.ToUInt64(str),
                TypeId.Decimal => (str) => System.Convert.ToDecimal(str),
                TypeId.Double => (str) => System.Convert.ToDouble(str),
                TypeId.Single => (str) => System.Convert.ToSingle(str),

                TypeId.Bool => (str) => System.Convert.ToBoolean(str),
                TypeId.Char => (str) => System.Convert.ToChar(str),
                TypeId.String => (str) => str,

                _ => throw new Exception("Not supported conversion.")
            };


            return converter(value);
        }

        public static bool IsNumeric(Type type)
        {
            return type == typeof(Byte)
                || type == typeof(Int16)
                || type == typeof(Int32)
                || type == typeof(Int64)
                || type == typeof(SByte)
                || type == typeof(UInt16)
                || type == typeof(UInt32)
                || type == typeof(UInt64)
                //|| type == typeof(BigInteger)
                || type == typeof(Decimal)
                || type == typeof(Double)
                || type == typeof(Single);
        }

        public static bool IsSerializableType(Type type)
        {
            return type == typeof(string)
                || type == typeof(char)
                || type == typeof(bool)
                || IsNumeric(type);
        }
    }
}
