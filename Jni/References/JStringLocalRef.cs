using System.Diagnostics.CodeAnalysis;

namespace HelloJniLib.Jni.References
{
    public readonly struct JStringLocalRef : IEquatable<JStringLocalRef>
    {
#pragma warning disable 0649
        private readonly JObjectLocalRef _value;
        
        /// <summary>
        /// JNI local reference.
        /// </summary>
        public JObjectLocalRef Value => this._value;
        /// <inheritdoc/>
        public IntPtr Pointer => this._value.Pointer;
        
#pragma warning restore 0649

        #region Public Methods
        public Boolean Equals(JStringLocalRef other)
            => this._value.Equals(other._value);
        #endregion

        #region Override Methods
        public override Boolean Equals([NotNullWhen(true)] Object obj)
            => obj is JStringLocalRef other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion

        #region Operators
        public static Boolean operator ==(JStringLocalRef a, JStringLocalRef b) => a.Equals(b);
        public static Boolean operator !=(JStringLocalRef a, JStringLocalRef b) => !a.Equals(b);
        #endregion
    }
}
