using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni.Values;

public readonly struct JavaVMOption
{
	internal ReadOnlyValPtr<Byte> Name { get; init; }
	internal IntPtr ExtraInfo { get; init; }
}