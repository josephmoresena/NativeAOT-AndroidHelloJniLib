using System.Text;

using HelloJniLib.Jni.Identifiers;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke;

namespace HelloJniLib.Jni;

public class JNIHelper {
    // 获取JVM实例的函数指针
    public static T getJNIFunc<T>(JEnvRef env, IntPtr ptr) where T: Delegate{
        return ptr.GetUnsafeDelegate<T>();
    }
    
    // 找到一个JVM已经加载的类
    public static JClassLocalRef findClass(JEnvRef env, String sign) {
        var dlg = getJNIFunc<FindClassDelegate>(env,env.Environment.Functions.FindClassPointer);
        return dlg(env, (CCharSequence)Encoding.UTF8.GetBytes(sign).AsSpan());
    }

    //new a java string
    public static JStringLocalRef newJaveString(JEnvRef env, String str) {
       return str.AsSpan().WithSafeFixed(env, (in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)=> {
           var dlg = getJNIFunc<NewStringDelegate>(env,env.Environment.Functions.NewStringPointer);
           return dlg(jEnv, ctx.Pointer, ctx.Values.Length);
        });
    }
    
    private static JStringLocalRef CallStaticMethod(JEnvRef jEnv , String sign, params object[] pars)
    {
        JEnvValue value = jEnv.Environment;
        ref JNativeInterface jInterface = ref value.Functions;

        IntPtr callPoint = jInterface.CallStaticVoidMethodPointer;
        CallStaticVoidMethodDelegate method = callPoint.GetUnsafeDelegate<CallStaticVoidMethodDelegate>();

        //todo
        JClassLocalRef jclass = default; 
        JMethodId jMethod = default; 
        IntPtr args = default;
        method(jEnv, jclass, jMethod, args);
        return default;
    }
}
