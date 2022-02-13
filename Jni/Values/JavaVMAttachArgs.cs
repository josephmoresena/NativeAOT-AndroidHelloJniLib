using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.References;

namespace HelloJniLib.Jni.Values
{
    public readonly struct JavaVMAttachArgs
    {
        internal Int32 Version { get; init; }
        internal CCharSequence Name { get; init; }
        internal JObjectLocalRef Group { get; init; }
    }
}
