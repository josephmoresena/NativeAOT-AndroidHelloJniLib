using System.Runtime.InteropServices;

using HelloJniLib.Jni;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib
{
#pragma warning disable IDE0060
    public static class ExportedMethods
    {
        internal static readonly Boolean DisabledReflection = !typeof(String).ToString().Contains(nameof(String));

        private static DateTime? load = default;
        private static Int32 count = 0;

        /*
            [UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
            internal static Int32 LoadLibrary(JavaVMRef vm, IntPtr unknown)
            {
                load = DateTime.Now;
                count = 0;
                return 0x00010006; //JNI_VERSION_1_6
            }
        */

        [UnmanagedCallersOnly(EntryPoint = "Java_com_example_hellojni_HelloJni_stringFromJNI")]
        internal static JStringLocalRef Hello(JEnvRef jEnv, JObjectLocalRef jObj)
        {
            DateTime call = DateTime.Now;
            count++;

            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            IntPtr newStringPtr = jInterface.NewStringPointer;
            NewStringDelegate newString = newStringPtr.AsDelegate<NewStringDelegate>();

            String result =
                "Hello from JNI! Compiled with NativeAOT." + Environment.NewLine
                + GetRuntimeInformation(call);
            return CreateJString(result, jEnv, newString);
        }

        [UnmanagedCallersOnly(EntryPoint = "Java_com_example_hellojni_SqlServerConnection_getConnectionString")]
        internal static JStringLocalRef GetConnectionString(JEnvRef jEnv, JObjectLocalRef jObj, JStringLocalRef jServer, JStringLocalRef jUser, JStringLocalRef jPassword)
        {
            JavaVMRef vm = default;
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            GetStringLengthDelegate getStringLength =
                jInterface.GetStringLengthPointer.AsDelegate<GetStringLengthDelegate>();
            GetStringRegionDelegate getStringRegion =
                jInterface.GetStringRegionPointer.AsDelegate<GetStringRegionDelegate>();
            NewStringDelegate newString =
                jInterface.NewStringPointer.AsDelegate<NewStringDelegate>();
            NewWeakGlobalRefDelegate newWeakGlobalRef =
                jInterface.NewWeakGlobalRefPointer.AsDelegate<NewWeakGlobalRefDelegate>();

            GetJavaVMDelegate getJavaVM =
                jInterface.GetJavaVMPointer.AsDelegate<GetJavaVMDelegate>();

            String server = new String(default, getStringLength(jEnv, jServer));
            String user = new String(default, getStringLength(jEnv, jUser));
            String password = new String(default, getStringLength(jEnv, jPassword));

            getStringRegion(jEnv, jServer, 0, server.Length, server.AsSpan());
            getStringRegion(jEnv, jUser, 0, user.Length, user.AsSpan());
            getStringRegion(jEnv, jPassword, 0, password.Length, password.AsSpan());
            getJavaVM(jEnv, ref vm);

            String strConn =
                $"Server=tcp:{server};Database=master;" +
                "Trusted_Connection=false;MultipleActiveResultSets=true;" +
                $"User ID={user};Password={password};";
            Console.WriteLine($"Connection string created. {strConn}");

            JWeakRef jWeak = newWeakGlobalRef(jEnv, jObj);
            Console.WriteLine($"Weak object created. {jWeak.Value}");

            _ = SqlExperiment.ConnectAsync(vm, jWeak, strConn);
            Console.WriteLine("Invoked connection async.");

            return CreateJString(strConn, jEnv, newString);
        }

        internal static JStringLocalRef CreateJString(String result, JEnvRef jEnv, NewStringDelegate newString)
        {
            ReadOnlySpan<Char> chars = result;
            JStringLocalRef jString = newString(jEnv, chars, chars.Length);
            return jString;
        }
        private static String GetRuntimeInformation(DateTime call)
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
            => !DisabledReflection ? GetRuntimeReflectionInformation() : "REFLECTION DISABLED";
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