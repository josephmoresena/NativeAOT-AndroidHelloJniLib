using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib.Jni.Pointers
{
    public readonly struct JEnvRef : IEquatable<JEnvRef>
    {
#pragma warning disable 0649
        private readonly IntPtr _value;
#pragma warning restore 0649

        #region Operators
        public static Boolean operator ==(JEnvRef a, JEnvRef b) => a._value.Equals(b._value);
        public static Boolean operator !=(JEnvRef a, JEnvRef b) => !a._value.Equals(b._value);
        #endregion

        #region Public Properties
        internal readonly ref JEnvValue Environment => ref this._value.AsReference<JEnvValue>();
        #endregion

        #region Public Methods
        public Boolean Equals(JEnvRef other) => this._value.Equals(other._value);
        #endregion

        #region Overrided Methods
        public override Boolean Equals(Object obj) => obj is JEnvRef other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion
    }
}
