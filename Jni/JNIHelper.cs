using System.Text;

using HelloJniLib.Jni.Identifiers;
using HelloJniLib.Jni.Internal.Pointers;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni;

// jclass jniInitializerClass = envPtr->FindClass( "android/app/ActivityThread");
//     if (jniInitializerClass == NULL) {return NULL;}
// jmethodID getApplicationMethod = envPtr->GetStaticMethodID(jniInitializerClass,"currentApplication","()Landroid/app/Application;");
//     if (getApplicationMethod == NULL) {return NULL;}
//
// jobject app = envPtr->CallStaticObjectMethod( jniInitializerClass,getApplicationMethod);
//     if (app == NULL) {return NULL;}
//
// jobject ctx = CallObjectMethod(app, "getBaseContext", "()Landroid/content/Context;").l;
//
// ctx = envPtr->NewGlobalRef(ctx);

public static class JNIHelper {
    // 获取JVM实例的函数指针
    public static T getJNIFunc<T>(JEnvRef env, IntPtr ptr) where T: Delegate{
        return ptr.GetUnsafeDelegate<T>();
    }
    
    // 找到一个JVM已经加载的类
    public static JClassLocalRef findClass(JEnvRef env, String classPath) {
        var dlg = getJNIFunc<FindClassDelegate>(env,env.Environment.Functions.FindClassPointer);
        return dlg(env, classPath.ToCCharSequece());
    }
    // 找到静态方法的MethodID
    public static JMethodId findStaticMethod(JEnvRef env, JClassLocalRef clazz,String metodName,String parSign) {
        var dlg = getJNIFunc<GetStaticMethodIdDelegate>(env,env.Environment.Functions.GetStaticMethodIdPointer);
        return dlg(env,clazz, metodName.ToCCharSequece(),parSign.ToCCharSequece());
    }

    //new a java string
    public static JStringLocalRef newJavaString(JEnvRef env, String str) {
       return str.AsSpan().WithSafeFixed(env, (in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)=> {
           var dlg = getJNIFunc<NewStringDelegate>(env,env.Environment.Functions.NewStringPointer);
           return dlg(jEnv, ctx.Pointer, ctx.Values.Length);
        });
    }
    
    public static JStringLocalRef toJavaString(this string str, JEnvRef env) {
        return str.AsSpan().WithSafeFixed(env, (in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)=> {
            var dlg = getJNIFunc<NewStringDelegate>(env,env.Environment.Functions.NewStringPointer);
            return dlg(jEnv, ctx.Pointer, ctx.Values.Length);
        });
    }
    public static void callStaticMethodV_V(JEnvRef jEnv , JClassLocalRef clazz,JMethodId jMethod)
    {
        JEnvValue value = jEnv.Environment;
        ref JNativeInterface jInterface = ref value.Functions;

        IntPtr callPoint = jInterface.CallStaticVoidMethodPointer;
        CallStaticVoidMethodDelegate method = callPoint.GetUnsafeDelegate<CallStaticVoidMethodDelegate>();
        // IntPtr args = default;
        method(jEnv, clazz, jMethod, default);
    }
    public static void callStaticMethodV_Str(JEnvRef jEnv , String classPath,String methodName,String methodSign, string pars) {
        var javaClazz = JNIHelper.findClass(jEnv,classPath);
        var javaMethod= JNIHelper.findStaticMethod(jEnv,javaClazz,methodName,methodSign);
        
        JEnvValue value = jEnv.Environment;
        ref JNativeInterface jInterface = ref value.Functions;

        IntPtr callPoint = jInterface.CallStaticVoidMethodPointer;
        CallStaticVoidMethodDelegate method = callPoint.GetUnsafeDelegate<CallStaticVoidMethodDelegate>();
        var stringLocalRef = newJavaString(jEnv,pars);
        method(jEnv, javaClazz, javaMethod, stringLocalRef.GetUnsafeIntPtr());
        
    }
    public static string callStaticMethodStr_Str(JEnvRef jEnv , String classPath,String methodName,String methodSign, string pars) {
        var javaClazz = JNIHelper.findClass(jEnv,classPath);
        var javaMethod= JNIHelper.findStaticMethod(jEnv,javaClazz,methodName,methodSign);
        
        JEnvValue envPtr = jEnv.Environment;
        ref JNativeInterface jni = ref envPtr.Functions;

        var invoke = jni.CallStaticObjectMethodPointer.GetUnsafeDelegate<CallStaticObjectMethodDelegate>();
        
        var strRef = pars.toJavaString(jEnv);
        // JStringObject jso = new JStringObject(strRef); 
        var result = invoke(jEnv,javaClazz,javaMethod,strRef.GetUnsafeValPtr());
        return Encoding.UTF8.GetString(result.ToBytes());
    }
}
