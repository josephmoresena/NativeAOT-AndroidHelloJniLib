using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni.Pointers
{
    public readonly struct JCharSequence : IEquatable<JCharSequence>
    {
        private readonly IntPtr _value;

        private JCharSequence(IntPtr value) => this._value = value;

        #region Operators
        public static implicit operator JCharSequence(IntPtr value) => new(value);
        public static implicit operator JCharSequence(Span<Char> span) => new(span.GetUnsafeIntPtr());
        public static implicit operator JCharSequence(ReadOnlySpan<Char> readonlySpan) => new(readonlySpan.GetUnsafeIntPtr());

        public static JCharSequence operator ++(JCharSequence a) => new(a._value + sizeof(Char));
        public static JCharSequence operator --(JCharSequence a) => new(a._value - sizeof(Char));
        public static Boolean operator ==(JCharSequence a, JCharSequence b) => a._value.Equals(b._value);
        public static Boolean operator !=(JCharSequence a, JCharSequence b) => !a._value.Equals(b._value);
        #endregion

        #region Public Methods
        public Boolean Equals(JCharSequence other) => this._value.Equals(other._value);
        public String AsString(Int32 length = 0) => this._value.GetUnsafeString(length);
        #endregion

        #region Overrided Methods
        public override Boolean Equals(Object obj) => obj is JCharSequence other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion
    }
}
