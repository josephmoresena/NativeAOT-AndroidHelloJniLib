using System.Runtime.CompilerServices;

using HelloJniLib.Jni.Primitives;

using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni.Pointers
{
    public readonly struct JBooleanRef
    {   
        private static readonly Int32 JBooleanResultFalse = 0;
        private static readonly Int32 JBooleanResultTrue = 1;

#pragma warning disable IDE0052
        private readonly IntPtr _value;
#pragma warning restore IDE0052

        public JBooleanRef(JBoolean? jBoolean)
            => this._value = jBoolean.HasValue ? GetJBooleanRef(jBoolean.Value) : IntPtr.Zero;

        private static IntPtr GetJBooleanRef(Boolean value)
            => value ? Unsafe.AsRef(in JBooleanResultTrue).GetUnsafeIntPtr() : Unsafe.AsRef(in JBooleanResultFalse).GetUnsafeIntPtr();
    }
}
