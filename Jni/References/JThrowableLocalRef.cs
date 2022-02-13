using System.Diagnostics.CodeAnalysis;

namespace HelloJniLib.Jni.References
{
    public readonly struct JThrowableLocalRef : IEquatable<JThrowableLocalRef>
    {
#pragma warning disable 0649
        private readonly IntPtr _value;
#pragma warning restore 0649

        #region Public Methods
        public Boolean Equals(JThrowableLocalRef other)
            => this._value.Equals(other._value);
        #endregion

        #region Override Methods
        public override Boolean Equals([NotNullWhen(true)] Object obj)
            => obj is JThrowableLocalRef other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion

        #region Operators
        public static Boolean operator ==(JThrowableLocalRef a, JThrowableLocalRef b) => a.Equals(b);
        public static Boolean operator !=(JThrowableLocalRef a, JThrowableLocalRef b) => !a.Equals(b);
        #endregion
    }
}
