using HelloJniLib.Jni.Pointers;

namespace HelloJniLib.Jni.Values
{
    public readonly struct JavaVMOption
    {
        internal CCharSequence Name { get; init; }
        internal IntPtr ExtraInfo { get; init; }
    }
}
