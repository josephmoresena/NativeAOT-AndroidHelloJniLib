using System.Text;

using HelloJniLib.Jni.Pointers;

namespace HelloJniLib.Jni;

public static class JNIExt {
    public static CCharSequence ToCCharSequece(this string str) {
        return  (CCharSequence)Encoding.UTF8.GetBytes(str).AsSpan();
    }
}
