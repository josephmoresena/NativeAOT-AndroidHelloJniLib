﻿using System.Diagnostics.CodeAnalysis;

namespace HelloJniLib.Jni.References
{
    public readonly struct JObjectLocalRef : IEquatable<JObjectLocalRef>
    {
#pragma warning disable 0649
        private readonly IntPtr _value;
        public IntPtr Pointer => this._value;

#pragma warning restore 0649

        #region Public Methods
        public Boolean Equals(JObjectLocalRef other)
            => this._value.Equals(other._value);
        #endregion

        #region Override Methods
        public override Boolean Equals([NotNullWhen(true)] Object obj)
            => obj is JObjectLocalRef other && this.Equals(other);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion

        #region Operators
        public static Boolean operator ==(JObjectLocalRef a, JObjectLocalRef b) => a.Equals(b);
        public static Boolean operator !=(JObjectLocalRef a, JObjectLocalRef b) => !a.Equals(b);
        #endregion
    }
}
