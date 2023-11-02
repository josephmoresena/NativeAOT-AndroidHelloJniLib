using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni.Internal.Pointers
{
    internal readonly struct JValueSequence : IEquatable<JValueSequence>
    {
        private readonly IntPtr _value;

        private JValueSequence(IntPtr value) => this._value = value;
        internal JValueSequence(ReadOnlySpan<JValue> readonlySpan) : this(readonlySpan.GetUnsafeIntPtr()) { }

        #region Operators
        public static implicit operator JValueSequence(IntPtr value) => new(value);

        public static JValueSequence operator ++(JValueSequence a) => new(a._value + JValue.Size);
        public static JValueSequence operator --(JValueSequence a) => new(a._value - JValue.Size);
        public static Boolean operator ==(JValueSequence a, JValueSequence b) => a._value.Equals(b._value);
        public static Boolean operator !=(JValueSequence a, JValueSequence b) => !a._value.Equals(b._value);
        #endregion

        #region Public Methods
        public Boolean Equals(JValueSequence other) => this._value.Equals(other._value);
        #endregion

        #region Overrided Methods
        public override Boolean Equals(Object obj) => obj is JValueSequence other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion
    }
}
