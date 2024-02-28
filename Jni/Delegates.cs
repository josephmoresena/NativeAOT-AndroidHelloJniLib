using HelloJniLib.Jni.Identifiers;
using HelloJniLib.Jni.Internal.Pointers;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.Primitives;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

namespace HelloJniLib.Jni
{
    internal delegate Int32 GetVersionDelegate(JEnvRef env);

    internal delegate JClassLocalRef DefineClassDelegate(JEnvRef env, CCharSequence name, JObjectLocalRef loader, IntPtr binaryData, Int32 len);
    internal delegate JClassLocalRef FindClassDelegate(JEnvRef env, CCharSequence name);

    internal delegate JMethodId FromReflectedMethodDelegate(JEnvRef env, JObjectLocalRef method);
    internal delegate JFieldId FromReflectedFieldIdDelegate(JEnvRef env, JObjectLocalRef field);

    internal delegate JObjectLocalRef ToReflectedMethodDelegate(JEnvRef env, JClassLocalRef jClass, JMethodId methodId, Boolean isStatic);

    internal delegate JClassLocalRef GetSuperclassDelegate(JEnvRef env, JClassLocalRef sub);
    internal delegate Boolean IsAssignableFromDelegate(JEnvRef env, JClassLocalRef sub, JClassLocalRef sup);

    internal delegate JObjectLocalRef ToReflectedFieldIdDelegate(JEnvRef env, JClassLocalRef jClass, JFieldId fieldId, Boolean isStatic);

    internal delegate JResult ThrowDelegate(JEnvRef env, JThrowableLocalRef obj);
    internal delegate JResult ThrowNewDelegate(JEnvRef env, JClassLocalRef jClass, CCharSequence msg);
    internal delegate JThrowableLocalRef ExceptionOccurredDelegate(JEnvRef env);
    internal delegate void ExceptionDescribeDelegate(JEnvRef env);
    internal delegate void ExceptionClearDelegate(JEnvRef env);
    internal delegate void FatalErrorDelegate(JEnvRef env, CCharSequence msg);

    internal delegate JResult PushLocalFrameDelegate(JEnvRef env, Int32 capacity);
    internal delegate JObjectLocalRef PopLocalFrameDelegate(JEnvRef env, JObjectLocalRef result);

    internal delegate JGlobalRef NewGlobalRefDelegate(JEnvRef env, JObjectLocalRef lref);
    internal delegate void DeleteGlobalRefDelegate(JEnvRef env, JGlobalRef gref);
    internal delegate void DeleteLocalRefDelegate(JEnvRef env, JObjectLocalRef lref);
    internal delegate Boolean IsSameObjectDelegate(JEnvRef env, JObjectLocalRef obj1, JObjectLocalRef obj2);
    internal delegate JObjectLocalRef NewLocalRefDelegate(JEnvRef env, JObjectLocalRef objRef);
    internal delegate JResult EnsureLocalCapacityDelegate(JEnvRef env, Int32 capacity);

    internal delegate JObjectLocalRef AllocObjectDelegate(JEnvRef env, JClassLocalRef jClass);
    internal delegate JObjectLocalRef NewObjectDelegate(JEnvRef env, JClassLocalRef jClass, JMethodId jMethod, IntPtr args);
    internal delegate JObjectLocalRef NewObjectVDelegate(JEnvRef env, JClassLocalRef jClass, JMethodId jMethod, ArgIterator args);
    internal delegate JObjectLocalRef NewObjectADelegate(JEnvRef env, JClassLocalRef jClass, JMethodId jMethod, JValueSequence args);

    internal delegate JClassLocalRef GetObjectClassDelegate(JEnvRef env, JObjectLocalRef obj);
    internal delegate Boolean IsInstanceOfDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass);

    internal delegate JMethodId GetMethodIdDelegate(JEnvRef env, JClassLocalRef jClass, CCharSequence name, CCharSequence signature);

    internal delegate JObjectLocalRef CallObjectMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JObjectLocalRef CallObjectMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JObjectLocalRef CallObjectMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JBoolean CallBooleanMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JBoolean CallBooleanMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JBoolean CallBooleanMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JByte CallByteMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JByte CallByteMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JByte CallByteMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JChar CallCharMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JChar CallCharMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JChar CallCharMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JShort CallShortMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JShort CallShortMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JShort CallShortMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JInt CallIntMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JInt CallIntMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JInt CallIntMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JLong CallLongMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JLong CallLongMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JLong CallLongMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JFloat CallFloatMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JFloat CallFloatMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JFloat CallFloatMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate JDouble CallDoubleMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate JDouble CallDoubleMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate JDouble CallDoubleMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);
    internal delegate void CallVoidMethodDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, IntPtr args);
    internal delegate void CallVoidMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, ArgIterator args);
    internal delegate void CallVoidMethodADelegate(JEnvRef env, JObjectLocalRef obj, JMethodId jMethod, JValueSequence args);

    internal delegate JObjectLocalRef CallNonVirtualObjectMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JObjectLocalRef CallNonVirtualObjectMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JObjectLocalRef CallNonVirtualObjectMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JBoolean CallNonVirtualBooleanMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JBoolean CallNonVirtualBooleanMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JBoolean CallNonVirtualBooleanMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JByte CallNonVirtualByteMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JByte CallNonVirtualByteMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JByte CallNonVirtualByteMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JChar CallNonVirtualCharMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JChar CallNonVirtualCharMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JChar CallNonVirtualCharMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JShort CallNonVirtualShortMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JShort CallNonVirtualShortMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JShort CallNonVirtualShortMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JInt CallNonVirtualIntMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JInt CallNonVirtualIntMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JInt CallNonVirtualIntMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JLong CallNonVirtualLongMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JLong CallNonVirtualLongMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JLong CallNonVirtualLongMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JFloat CallNonVirtualFloatMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JFloat CallNonVirtualFloatMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JFloat CallNonVirtualFloatMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JDouble CallNonVirtualDoubleMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JDouble CallNonVirtualDoubleMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JDouble CallNonVirtualDoubleMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate void CallNonVirtualVoidMethodDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate void CallNonVirtualVoidMethodVDelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate void CallNonVirtualVoidMethodADelegate(JEnvRef env, JObjectLocalRef obj, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);

    internal delegate JFieldId GetFieldIdDelegate(JEnvRef env, JClassLocalRef jclass, CCharSequence name, CCharSequence signature);

    internal delegate JObjectLocalRef GetObjectFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JBoolean GetBooleanFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JByte GetByteFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JChar GetCharFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JShort GetShortFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JInt GetIntFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JLong GetLongFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JFloat GetFloatFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate JDouble GetDoubleFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField);
    internal delegate void SetObjectFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JObjectLocalRef value);
    internal delegate void SetBooleanFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JBoolean value);
    internal delegate void SetByteFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JByte value);
    internal delegate void SetCharFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JChar value);
    internal delegate void SetShortFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JShort value);
    internal delegate void SetIntFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JInt value);
    internal delegate void SetLongFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JLong value);
    internal delegate void SetFloatFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JFloat value);
    internal delegate void SetDoubleFieldDelegate(JEnvRef env, JObjectLocalRef obj, JFieldId jField, JDouble value);

    internal delegate JMethodId GetStaticMethodIdDelegate(JEnvRef env, JClassLocalRef jClass, CCharSequence name, CCharSequence signature);

    internal delegate JObjectLocalRef CallStaticObjectMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JObjectLocalRef CallStaticObjectMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JObjectLocalRef CallStaticObjectMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JBoolean CallStaticBooleanMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JBoolean CallStaticBooleanMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JBoolean CallStaticBooleanMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JByte CallStaticByteMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JByte CallStaticByteMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JByte CallStaticByteMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JChar CallStaticCharMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JChar CallStaticCharMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JChar CallStaticCharMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JShort CallStaticShortMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JShort CallStaticShortMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JShort CallStaticShortMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JInt CallStaticIntMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JInt CallStaticIntMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JInt CallStaticIntMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JLong CallStaticLongMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JLong CallStaticLongMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JLong CallStaticLongMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JFloat CallStaticFloatMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JFloat CallStaticFloatMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JFloat CallStaticFloatMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate JDouble CallStaticDoubleMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate JDouble CallStaticDoubleMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate JDouble CallStaticDoubleMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);
    internal delegate void CallStaticVoidMethodDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, IntPtr args);
    internal delegate void CallStaticVoidMethodVDelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, ArgIterator args);
    internal delegate void CallStaticVoidMethodADelegate(JEnvRef env, JClassLocalRef jclass, JMethodId jMethod, JValueSequence args);

    internal delegate JFieldId GetStaticFieldIdDelegate(JEnvRef env, JClassLocalRef jclass, CCharSequence name, CCharSequence signature);

    internal delegate JObjectLocalRef GetStaticObjectFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JBoolean GetStaticBooleanFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JByte GetStaticByteFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JChar GetStaticCharFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JShort GetStaticShortFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JInt GetStaticIntFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JLong GetStaticLongFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JFloat GetStaticFloatFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate JDouble GetStaticDoubleFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField);
    internal delegate void SetStaticObjectFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JObjectLocalRef value);
    internal delegate void SetStaticBooleanFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JBoolean value);
    internal delegate void SetStaticByteFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JByte value);
    internal delegate void SetStaticCharFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JChar value);
    internal delegate void SetStaticShortFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JShort value);
    internal delegate void SetStaticIntFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JInt value);
    internal delegate void SetStaticLongFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JLong value);
    internal delegate void SetStaticFloatFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JFloat value);
    internal delegate void SetStaticDoubleFieldDelegate(JEnvRef env, JClassLocalRef jclass, JFieldId jField, JDouble value);

    internal delegate JStringLocalRef NewStringDelegate(JEnvRef env, JCharSequence unicode, Int32 length);

    internal delegate Int32 GetStringLengthDelegate(JEnvRef env, JStringLocalRef jString);
    internal delegate JCharSequence GetStringCharsDelegate(JEnvRef env, JStringLocalRef jString, JBooleanRef isCopy);
    internal delegate void ReleaseStringCharsDelegate(JEnvRef env, JStringLocalRef jString, JCharSequence chars);

    internal delegate JStringLocalRef NewStringUtfDelegate(JEnvRef env, CCharSequence unicode);
    internal delegate Int32 GetStringUtfLengthDelegate(JEnvRef env, JStringLocalRef jString);
    internal delegate CCharSequence GetStringUtfCharsDelegate(JEnvRef env, JObjectLocalRef jString, JBooleanRef isCopy);
    internal delegate void ReleaseStringUtfCharsDelegate(JEnvRef env, JStringLocalRef jString, CCharSequence chars);

    internal delegate Int32 GetArrayLengthDelegate(JEnvRef env, JArrayLocalRef array);

    internal delegate JArrayLocalRef NewObjectArrayDelegate(JEnvRef env, Int32 length, JClassLocalRef jClass, JObjectLocalRef init);
    internal delegate JObjectLocalRef GetObjectArrayElementDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 index);
    internal delegate void SetObjectArrayElementDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 index, JObjectLocalRef obj);

    internal delegate JArrayLocalRef NewBooleanArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewByteArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewCharArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewShortArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewIntArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewLongArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewFloatArrayDelegate(JEnvRef env, Int32 length);
    internal delegate JArrayLocalRef NewDoubleArrayDelegate(JEnvRef env, Int32 length);

    internal delegate IntPtr GetBooleanArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetByteArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetCharArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetShortArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetIntArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetLongArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetFloatArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate IntPtr GetDoubleArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);

    internal delegate void ReleaseBooleanArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseByteArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseCharArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseShortArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseIntArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseLongArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseFloatArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);
    internal delegate void ReleaseDoubleArrayElementsDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);

    internal delegate void GetBooleanArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetByteArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetCharArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetShortArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetIntArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetLongArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetFloatArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void GetDoubleArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetBooleanArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetByteArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetCharArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetShortArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetIntArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetLongArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetFloatArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);
    internal delegate void SetDoubleArrayRegionDelegate(JEnvRef env, JArrayLocalRef jArray, Int32 startIndex, Int32 length, IntPtr buffer);

    internal delegate JResult RegisterNativesDelegate(JEnvRef env, JClassLocalRef jClass, JNativeMethodSequence methods);
    internal delegate JResult UnregisterNativesDelegate(JEnvRef env, JClassLocalRef jClass);

    internal delegate JResult MonitorEnterDelegate(JEnvRef env, JObjectLocalRef jClass);
    internal delegate JResult MonitorExitDelegate(JEnvRef env, JObjectLocalRef jClass);

    internal delegate JResult GetJavaVMDelegate(JEnvRef env, ref JavaVMRef jvm);

    internal delegate void GetStringRegionDelegate(JEnvRef env, JStringLocalRef jString, Int32 startIndex, Int32 length, JCharSequence buffer);
    internal delegate void GetStringUtfRegionDelegate(JEnvRef env, JStringLocalRef jString, Int32 startIndex, Int32 length, CCharSequence buffer);

    internal delegate IntPtr GetPrimitiveArrayCriticalDelegate(JEnvRef env, JArrayLocalRef jArray, JBooleanRef isCopy);
    internal delegate void ReleasePrimitiveArrayCriticalDelegate(JEnvRef env, JArrayLocalRef jArray, IntPtr elements, JReleaseMode mode);

    internal delegate JCharSequence GetStringCriticalDelegate(JEnvRef env, JStringLocalRef jString, JBooleanRef isCopy);
    internal delegate void ReleaseStringCriticalDelegate(JEnvRef env, JStringLocalRef jString, JCharSequence chars);

    internal delegate JWeakRef NewWeakGlobalRefDelegate(JEnvRef env, JObjectLocalRef obj);
    internal delegate void DeleteWeakGlobalRefDelegate(JEnvRef env, JWeakRef jWeak);

    internal delegate Boolean ExceptionCheckDelegate(JEnvRef env);

    internal delegate JObjectLocalRef NewDirectByteBufferDelegate(JEnvRef env, IntPtr address, Int64 capacity);
    internal delegate IntPtr GetDirectBufferAddressDelegate(JEnvRef env, JObjectLocalRef buffObj);
    internal delegate Int64 GetDirectBufferCapacityDelegate(JEnvRef env, JObjectLocalRef buffObj);

    internal delegate JReferenceType GetObjectRefTypeDelegate(JEnvRef env, JObjectLocalRef obj);

    internal delegate JResult DestroyJavaVMDelegate(JavaVMRef vm);
    internal delegate JResult AttachCurrentThreadDelegate(JavaVMRef vm, ref JEnvRef env, in JavaVMAttachArgs args);
    internal delegate JResult DetachCurrentThreadDelegate(JavaVMRef vm);

    internal delegate JResult GetEnvDelegate(JavaVMRef vm, ref JEnvRef env, JInt version);

    internal delegate JResult AttachCurrentThreadAsDaemonDelegate(JavaVMRef vm, ref JEnvRef env, in JavaVMAttachArgs args);
}