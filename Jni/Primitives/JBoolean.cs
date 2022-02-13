using HelloJniLib.Jni.Pointers;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib.Jni.Primitives
{
    public readonly struct JBoolean : IComparable<Boolean>, IEquatable<Boolean>
    {
        internal static readonly Type Type = typeof(JBoolean);

        public static readonly CString Signature = "Z";

        private readonly Boolean _value;

        private JBoolean(Boolean value) => this._value = value;

        #region Operators
        public static implicit operator JBoolean(Boolean value) => new(value);
        public static implicit operator Boolean(JBoolean jValue) => jValue._value;
        public static implicit operator JBooleanRef(JBoolean? jValue) => new(jValue);
        public static JBoolean operator !(JBoolean a) => new(!a._value);
        public static JBoolean operator |(JBoolean a, JBoolean b) => new(a._value || b._value);
        public static JBoolean operator |(Boolean a, JBoolean b) => new(a || b._value);
        public static JBoolean operator |(JBoolean a, Boolean b) => new(a._value || b);
        public static JBoolean operator &(JBoolean a, JBoolean b) => new(a._value && b._value);
        public static JBoolean operator &(Boolean a, JBoolean b) => new(a && b._value);
        public static JBoolean operator &(JBoolean a, Boolean b) => new(a._value && b);
        public static Boolean operator ==(JBoolean a, JBoolean b) => a._value.Equals(b._value);
        public static Boolean operator ==(Boolean a, JBoolean b) => a.Equals(b._value);
        public static Boolean operator ==(JBoolean a, Boolean b) => a._value.Equals(b);
        public static Boolean operator !=(JBoolean a, JBoolean b) => !a._value.Equals(b._value);
        public static Boolean operator !=(Boolean a, JBoolean b) => !a.Equals(b._value);
        public static Boolean operator !=(JBoolean a, Boolean b) => !a._value.Equals(b);
        public static Boolean operator >(JBoolean a, JBoolean b) => a._value.CompareTo(b._value) > 0;
        public static Boolean operator >(Boolean a, JBoolean b) => a.CompareTo(b._value) > 0;
        public static Boolean operator >(JBoolean a, Boolean b) => a._value.CompareTo(b) > 0;
        public static Boolean operator <(JBoolean a, JBoolean b) => a._value.CompareTo(b._value) < 0;
        public static Boolean operator <(Boolean a, JBoolean b) => a.CompareTo(b._value) < 0;
        public static Boolean operator <(JBoolean a, Boolean b) => a._value.CompareTo(b) < 0;
        public static Boolean operator >=(JBoolean a, JBoolean b) => a._value.CompareTo(b._value) > 0 || a.Equals(b._value);
        public static Boolean operator >=(Boolean a, JBoolean b) => a.CompareTo(b._value) > 0 || a.Equals(b._value);
        public static Boolean operator >=(JBoolean a, Boolean b) => a._value.CompareTo(b) > 0 || a._value.Equals(b);
        public static Boolean operator <=(JBoolean a, JBoolean b) => a._value.CompareTo(b._value) < 0 || a.Equals(b._value);
        public static Boolean operator <=(Boolean a, JBoolean b) => a.CompareTo(b._value) < 0 || a.Equals(b._value);
        public static Boolean operator <=(JBoolean a, Boolean b) => a._value.CompareTo(b) < 0 || a._value.Equals(b);
        #endregion

        #region Public Methods
        public Int32 CompareTo(Boolean other) => this._value.CompareTo(other);
        public Int32 CompareTo(JBoolean other) => this._value.CompareTo(other._value);
        public Int32 CompareTo(Object obj) => obj is JBoolean jvalue ? this.CompareTo(jvalue) : obj is Boolean value ? this.CompareTo(value) : this._value.CompareTo(obj);
        public Boolean Equals(Boolean other) => this._value.Equals(other);
        public Boolean Equals(JBoolean other) => this._value.Equals(other._value);
        public String ToString(IFormatProvider formatProvider) => this._value.ToString(formatProvider);
        #endregion

        #region Overrided Methods
        public override String ToString() => this._value.ToString();
        public override Boolean Equals(Object obj) => obj is JBoolean jvalue ? this.Equals(jvalue) : obj is Boolean value ? this.Equals(value) : this._value.Equals(obj);
        public override Int32 GetHashCode() => this._value.GetHashCode();
        #endregion
    }
}
