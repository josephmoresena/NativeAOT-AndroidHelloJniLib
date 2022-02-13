using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib.Jni.Pointers
{
    public readonly struct JNativeMethodSequence : IEquatable<JNativeMethodSequence>
    {
        private readonly IntPtr _value;

        private JNativeMethodSequence(IntPtr value) => this._value = value;

        #region Operators
        public static implicit operator JNativeMethodSequence(IntPtr value) => new(value);
        public static implicit operator JNativeMethodSequence(ReadOnlySpan<JNativeMethod> readonlySpan) => new(readonlySpan.AsIntPtr());

        public static JNativeMethodSequence operator ++(JNativeMethodSequence a) => new(a._value + JValue.Size);
        public static JNativeMethodSequence operator --(JNativeMethodSequence a) => new(a._value - JValue.Size);
        public static Boolean operator ==(JNativeMethodSequence a, JNativeMethodSequence b) => a._value.Equals(b._value);
        public static Boolean operator !=(JNativeMethodSequence a, JNativeMethodSequence b) => !a._value.Equals(b._value);
        #endregion

        #region Public Methods
        public Boolean Equals(JNativeMethodSequence other) => this._value.Equals(other._value);
        #endregion

        #region Overrided Methods
        public override Boolean Equals(Object obj) => obj is JNativeMethodSequence other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion
    }
}
