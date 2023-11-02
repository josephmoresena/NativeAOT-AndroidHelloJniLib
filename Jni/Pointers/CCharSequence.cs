using System.Runtime.InteropServices;

using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni.Pointers
{
    public readonly struct CCharSequence : IEquatable<CCharSequence>
    {
        private readonly IntPtr _value;

        private CCharSequence(IntPtr value) => this._value = value;

        #region Operators
        public static implicit operator CCharSequence(IntPtr value) => new(value);
        public static implicit operator CCharSequence(Span<Byte> span) => new(span.GetUnsafeIntPtr());
        public static implicit operator CCharSequence(ReadOnlySpan<Byte> readonlySpan) => new(readonlySpan.GetUnsafeIntPtr());

        public static CCharSequence operator ++(CCharSequence a) => new(a._value + sizeof(Byte));
        public static CCharSequence operator --(CCharSequence a) => new(a._value - sizeof(Byte));
        public static Boolean operator ==(CCharSequence a, CCharSequence b) => a._value.Equals(b._value);
        public static Boolean operator !=(CCharSequence a, CCharSequence b) => !a._value.Equals(b._value);
        #endregion

        #region Public Methods
        public Boolean Equals(CCharSequence other) => this._value.Equals(other._value);
        public String AsString(Int32 length = 0)
            => length == 0 ? Marshal.PtrToStringUTF8(_value) : Marshal.PtrToStringUTF8(_value, length);
        #endregion

        #region Overrided Methods
        public override Boolean Equals(Object obj) => obj is CCharSequence other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion
    }
}
