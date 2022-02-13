using HelloJniLib.Jni.Pointers;

namespace HelloJniLib.Jni.Values
{
    public readonly struct JNativeMethod
    {
        internal CCharSequence Name { get; init; }
        internal CCharSequence Signature { get; init; }
        internal IntPtr Pointer { get; init; }
    }
}
