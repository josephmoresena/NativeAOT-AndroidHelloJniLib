using HelloJniLib.Jni.References;

using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni.Values;

public readonly struct JavaVMAttachArgs
{
	internal Int32 Version { get; init; }
	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal JObjectLocalRef Group { get; init; }
}