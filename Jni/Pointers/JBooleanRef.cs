using System.Runtime.CompilerServices;

using HelloJniLib.Jni.Primitives;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib.Jni.Pointers
{
    public readonly struct JBooleanRef
    {
        private const Int32 JBooleanResultFalse = 0;
        private const Int32 JBooleanResultTrue = 1;

#pragma warning disable IDE0052
        private readonly IntPtr _value;
#pragma warning restore IDE0052

        public JBooleanRef(JBoolean? jBoolean)
            => this._value = jBoolean.HasValue ? GetJBooleanRef(jBoolean.Value) : IntPtr.Zero;

        private static IntPtr GetJBooleanRef(Boolean value)
            => value ? Unsafe.AsRef(JBooleanResultTrue).AsIntPtr() : Unsafe.AsRef(JBooleanResultFalse).AsIntPtr();
    }
}
