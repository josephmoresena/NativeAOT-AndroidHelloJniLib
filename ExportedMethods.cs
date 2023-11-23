using System.Runtime.InteropServices;

using HelloJniLib.Jni;
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

        private static JavaVMRef? vmRef = default;
        private static DateTime? load = default;
        private static Int32 count = 0;

        [UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
        internal static Int32 LoadLibrary(JavaVMRef vm, IntPtr unknown)
        {
            load = DateTime.Now;
            vmRef = vm;
            return 0x00010006; //JNI_VERSION_1_6
        }

        [UnmanagedCallersOnly(EntryPoint = "Java_com_example_hellojni_HelloJni_stringFromJNI")]
        internal static JStringLocalRef Hello(JEnvRef jEnv, JObjectLocalRef jObj)
        {
            DateTime call = DateTime.Now;
            count++;

            String result =
                "Hello from JNI! Compiled with NativeAOT." + Environment.NewLine
                + GetRuntimeInformation(call, GetJavaVersion(jEnv), jEnv);

            return result.AsSpan().WithSafeFixed(jEnv, CreateString);
        }

        private static Int32 GetJavaVersion(JEnvRef jEnv)
        {
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            IntPtr getVersionPtr = jInterface.GetVersionPointer;
            GetVersionDelegate getVersion = getVersionPtr.GetUnsafeDelegate<GetVersionDelegate>()!;
            return getVersion(jEnv);
        }

        private static String GetJavaVersion2Env(JEnvRef jEnv)
        {
            if (!ExportedMethods.vmRef.HasValue)
                return "No VM";
            JavaVMValue value = ExportedMethods.vmRef.Value.VirtualMachine;
            ref JInvokeInterface jInvoke = ref value.Functions;
            IntPtr getEnvPtr = jInvoke.GetEnvPointer;
            
            GetEnvDelegate getEnv = getEnvPtr.GetUnsafeDelegate<GetEnvDelegate>();
            JEnvRef envRef2 = default;
            getEnv(ExportedMethods.vmRef.Value, ref envRef2, 0x00010002);
            Int32 newVersion = GetJavaVersion(envRef2);

            return $"Equal: {jEnv == envRef2} Version2: 0x{newVersion:x8}";
        }
        private static String GetJavaVersion2Env()
        {
            if (!ExportedMethods.vmRef.HasValue)
                return "No VM";
            JavaVMValue value = ExportedMethods.vmRef.Value.VirtualMachine;
            ref JInvokeInterface jInvoke = ref value.Functions;
            IntPtr attachCurrentThread = jInvoke.AttachCurrentThreadPointer;
            IntPtr detachCurrentThread = jInvoke.DetachCurrentThreadPointer;
            
            AttachCurrentThreadDelegate getEnv = attachCurrentThread.GetUnsafeDelegate<AttachCurrentThreadDelegate>();
            DetachCurrentThreadDelegate detach = detachCurrentThread.GetUnsafeDelegate<DetachCurrentThreadDelegate>();
            JEnvRef envRef2 = default;
            JavaVMAttachArgs args = new()
            {
                Version = 0x00010002,
            };
            getEnv(ExportedMethods.vmRef.Value, ref envRef2, in args);
            Int32 newVersion = GetJavaVersion(envRef2);
            detach(ExportedMethods.vmRef.Value);
            
            return $"Thread: {Environment.CurrentManagedThreadId} Version2: 0x{newVersion:x8}";
        }

        private static JStringLocalRef CreateString(in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)
        {
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            IntPtr newStringPtr = jInterface.NewStringPointer;
            NewStringDelegate newString = newStringPtr.GetUnsafeDelegate<NewStringDelegate>();

            return newString(jEnv, ctx.Pointer, ctx.Values.Length);
        }

        private static String GetRuntimeInformation(DateTime call, Int32 javaVersion, JEnvRef envRef)
            => $"Load: {load.GetString()}" + Environment.NewLine
            + $"Call: {call.GetString()}" + Environment.NewLine
            + $"Count: {count}" + Environment.NewLine 
            + $"Java Version: 0x{javaVersion:x8}" + Environment.NewLine 
            + $"Is Linux: {RuntimeInformation.IsOSPlatform(OSPlatform.Linux)}" + Environment.NewLine
            + $"{Environment.CurrentManagedThreadId}" + Environment.NewLine 
            + GetJavaVersion2Env(envRef) + Environment.NewLine 
            + Task.Run(GetJavaVersion2Env).Result 
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