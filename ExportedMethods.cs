using System.Runtime.InteropServices;
using System.Text;

using HelloJniLib.Jni;
using HelloJniLib.Jni.Identifiers;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke;

namespace HelloJniLib
{
#pragma warning disable IDE0060
    public static class ExportedMethods
    {
        private static readonly Boolean disabledReflection = !typeof(String).ToString().Contains(nameof(String));

        private static DateTime? load = default;
        private static Int32 count = 0;


        
        [UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
        internal static Int32 LoadLibrary(JavaVMRef vm, IntPtr unknown)
        {
            load = DateTime.Now;
            return 0x00010006; //JNI_VERSION_1_6
        }
        
        [UnmanagedCallersOnly(EntryPoint = "Java_com_csharp_interop_HelloJNI_stringFromJNI")]
        internal static JStringLocalRef Hello(JEnvRef jEnv, JObjectLocalRef jObj,JStringLocalRef str)
        {
            // jString = this.CreateInitialObject<JStringObject>(str.);
            DateTime call = DateTime.Now;
            count++;
            
            
            var javaClazz = JNIHelper.findClass(jEnv,"com/csharp/interop/HelloJNI");
            var javaMethod= JNIHelper.findStaticMethod(jEnv,javaClazz,"callByCSharp","()V");
            JNIHelper.callStaticMethodV_V(jEnv,javaClazz,javaMethod);
            string javastr = " ";
            var arg = "greet from csharp";
            Log.d($"reflect callStaticMethodStr_Str args:{arg}");
            javastr= JNIHelper.callStaticMethodStr_Str(jEnv,"com/csharp/interop/HelloJNI",
                                                       "callByCSharp","(Ljava/lang/String;)Ljava/lang/String;",
                                                       arg);
            
            Log.d($"reflect callStaticMethodStr_Str return :{javastr}");
            
            string result =
                $"Hello from JNI!  {count} Compiled with NativeAOT." 
                + Environment.NewLine
                + GetRuntimeInformation(call)
                + Environment.NewLine
                + "Call JNI find class:"
                + Environment.NewLine
                + javaClazz
                + Environment.NewLine
                + javaMethod
                + Environment.NewLine
                + javastr
                + Environment.NewLine
                + "par:"+str
                + Environment.NewLine;
            return result.toJavaStringRef(jEnv);
        }

        private static JStringLocalRef CreateString(in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)
        {
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            IntPtr newStringPtr = jInterface.NewStringPointer;
            NewStringDelegate newString = newStringPtr.GetUnsafeDelegate<NewStringDelegate>();

            return newString(jEnv, ctx.Pointer, ctx.Values.Length);
        }
       

        public static String GetRuntimeInformation(DateTime call)
            => $"Load: {load.GetString()}" + Environment.NewLine
            + $"Call: {call.GetString()}" + Environment.NewLine
            + $"Count: {count}"
            + Environment.NewLine + Environment.NewLine
            + $"Number of Cores: {Environment.ProcessorCount}" + Environment.NewLine
            + $"OS: {RuntimeInformation.OSDescription}" + Environment.NewLine
            + $"OS Arch: {RuntimeInformation.OSArchitecture.GetName()}" + Environment.NewLine
            + $"OS Version: {Environment.OSVersion}" + Environment.NewLine
            + $"Computer: {Environment.MachineName}" + Environment.NewLine
            + $"User: {Environment.UserName}" + Environment.NewLine
            + $"System Path: {Environment.SystemDirectory}" + Environment.NewLine
            + $"Current Path: {Environment.CurrentDirectory}" + Environment.NewLine
            + $"Process Arch: {RuntimeInformation.ProcessArchitecture.GetName()}"
            + Environment.NewLine + Environment.NewLine
            + GetRuntimeInformation();
        private static String GetRuntimeInformation()
            => !disabledReflection ? GetRuntimeReflectionInformation() : "REFLECTION DISABLED";
        private static String GetRuntimeReflectionInformation()
        {
            return $"Framework Version: {Environment.Version}" + Environment.NewLine
                + $"Runtime Name: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine
                + $"Runtime Path: {RuntimeEnvironment.GetRuntimeDirectory()}" + Environment.NewLine
                + $"Runtime Version: {RuntimeEnvironment.GetSystemVersion()}" + Environment.NewLine
                + Environment.NewLine;
        }
        private static String GetName(this Architecture architecture)
            => architecture switch
            {
                Architecture.S390x => nameof(Architecture.S390x),
                Architecture.Arm64 => nameof(Architecture.Arm64),
                Architecture.Arm => nameof(Architecture.Arm),
                Architecture.Wasm => nameof(Architecture.Wasm),
                Architecture.X64 => nameof(Architecture.X64),
                Architecture.X86 => nameof(Architecture.X86),
                _ => architecture.ToString(),
            };
        private static String GetString(this DateTime date)
            => GetString((DateTime?)date);
        private static String GetString(this DateTime? date)
            => date != default ? date.Value.ToString("yyyy-MM-dd HH:mm:ss.fffffff") : "null";
    }
#pragma warning restore IDE0060
    
    
    
}