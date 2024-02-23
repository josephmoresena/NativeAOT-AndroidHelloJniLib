using System.Text;

using HelloJniLib.Jni.Identifiers;
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

public class JNIHelper {
    // 获取JVM实例的函数指针
    public static T getJNIFunc<T>(JEnvRef env, IntPtr ptr) where T: Delegate{
        return ptr.GetUnsafeDelegate<T>();
    }
    
    // 找到一个JVM已经加载的类
    public static JClassLocalRef findClass(JEnvRef env, String sign) {
        var dlg = getJNIFunc<FindClassDelegate>(env,env.Environment.Functions.FindClassPointer);
        return dlg(env, sign.ToCCharSequece());
    }
    // 找到静态方法的MethodID
    public static JMethodId findStaticMethod(JEnvRef env, JClassLocalRef clazz,String metodName,String parSign) {
        var dlg = getJNIFunc<GetStaticMethodIdDelegate>(env,env.Environment.Functions.GetStaticMethodIdPointer);
        return dlg(env,clazz, metodName.ToCCharSequece(),parSign.ToCCharSequece());
    }

    //new a java string
    public static JStringLocalRef newJaveString(JEnvRef env, String str) {
       return str.AsSpan().WithSafeFixed(env, (in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)=> {
           var dlg = getJNIFunc<NewStringDelegate>(env,env.Environment.Functions.NewStringPointer);
           return dlg(jEnv, ctx.Pointer, ctx.Values.Length);
        });
    }
    
    public static JObjectLocalRef callStaticVMethod(JEnvRef jEnv , JClassLocalRef clazz,JMethodId jMethod, params object[] pars)
    {
        JEnvValue value = jEnv.Environment;
        ref JNativeInterface jInterface = ref value.Functions;

        IntPtr callPoint = jInterface.CallStaticVoidMethodPointer;
        CallStaticVoidMethodDelegate method = callPoint.GetUnsafeDelegate<CallStaticVoidMethodDelegate>();

        IntPtr args = default;//TODO 参数转为指针
        method(jEnv, clazz, jMethod, args);
        return default;
    }
}
