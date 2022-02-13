using System.Runtime.CompilerServices;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib.Jni.Values
{
    internal readonly struct JValue
    {
        private delegate Boolean IsDefaultDelegate(in JValue value);

        public static readonly Int32 Size = NativeUtilities.SizeOf<JValue>();

        private static readonly IsDefaultDelegate isDefault = GetIsDefault();

#pragma warning disable 0649
#pragma warning disable 0169
        private readonly Byte _value1;
        private readonly Byte _value2;
        private readonly Int16 _value3;
        private readonly Int32 _value4;
#pragma warning restore 0169
#pragma warning restore 0649

        public Boolean IsDefault => isDefault(this);

        public static JValue Create(ReadOnlySpan<Byte> source)
        {
            Byte[] result = new Byte[Size];
            for (Int32 i = 0; i < source.Length; i++)
                result[i] = source[i];
            return result.AsValue<JValue>();
        }

        private static IsDefaultDelegate GetIsDefault() => Environment.Is64BitProcess ? DefaultLong : Default;

        private static Boolean Default(in JValue jValue)
            => (jValue._value1 + jValue._value2 + jValue._value3) == default
            && jValue._value4 == default;

        private static Boolean DefaultLong(in JValue jValue) =>
            Unsafe.AsRef(jValue).AsReferenceOf<JValue, Int64>() == default;
    }
}
