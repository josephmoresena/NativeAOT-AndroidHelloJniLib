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
        private const String exportPrefix = "Java_com_example_hellojni_HelloJni_";
        internal static readonly Boolean DisabledReflection = !typeof(String).ToString().Contains(nameof(String));

        private static DateTime? load;
        private static Int32 count;

#if !ANDROID
        [UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
        private static Int32 LoadLibraryExported(JavaVMRef vm, IntPtr unknown) => ExportedMethods.LoadLibrary(vm, unknown);
#endif
        [UnmanagedCallersOnly(EntryPoint = ExportedMethods.exportPrefix + "stringFromJNI")]
        internal static JStringLocalRef Hello(JEnvRef jEnv, JObjectLocalRef jObj)
        {
            DateTime call = DateTime.Now;
            ExportedMethods.count++;
            String result =
                "Hello from JNI! Compiled with NativeAOT." + Environment.NewLine
                + ExportedMethods.GetRuntimeInformation(call);
            return result.AsSpan().WithSafeFixed(jEnv, ExportedMethods.CreateString);
        }

        [UnmanagedCallersOnly(EntryPoint = ExportedMethods.exportPrefix + "getConnectionString")]
        internal static JStringLocalRef GetConnectionString(JEnvRef jEnv, JObjectLocalRef jObj, JStringLocalRef jServer, JStringLocalRef jUser, JStringLocalRef jPassword)
        {
            JavaVMRef vm = default;
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            GetStringLengthDelegate getStringLength =
                jInterface.GetStringLengthPointer.GetUnsafeDelegate<GetStringLengthDelegate>()!;
            GetStringRegionDelegate getStringRegion =
                jInterface.GetStringRegionPointer.GetUnsafeDelegate<GetStringRegionDelegate>()!;
            NewWeakGlobalRefDelegate newWeakGlobalRef =
                jInterface.NewWeakGlobalRefPointer.GetUnsafeDelegate<NewWeakGlobalRefDelegate>()!;

            GetJavaVMDelegate getJavaVm =
                jInterface.GetJavaVMPointer.GetUnsafeDelegate<GetJavaVMDelegate>()!;

            String server = new(' ', getStringLength(jEnv, jServer));
            String user = new(' ', getStringLength(jEnv, jUser));
            String password = new(' ', getStringLength(jEnv, jPassword));

            getStringRegion(jEnv, jServer, 0, server.Length, server.AsSpan());
            getStringRegion(jEnv, jUser, 0, user.Length, user.AsSpan());
            getStringRegion(jEnv, jPassword, 0, password.Length, password.AsSpan());
            getJavaVm(jEnv, ref vm);

            String strConn =
                $"Server=tcp:{server.Trim()};Database=master;" +
                "Trusted_Connection=false;MultipleActiveResultSets=true;" +
                $"User ID={user.Trim()};Password={password.Trim()};";
            Console.WriteLine($"Connection string created. {strConn}");

            JWeakRef jWeak = newWeakGlobalRef(jEnv, jObj);
            Console.WriteLine($"Weak object created. {jWeak.Value}");

            SqlExperiment.ConnectAsync(vm, jWeak, strConn);
            Console.WriteLine("Invoked connection async.");

            return strConn.AsSpan().WithSafeFixed(jEnv, ExportedMethods.CreateString);
        }

        internal static JStringLocalRef CreateString(in IReadOnlyFixedContext<Char> ctx, JEnvRef jEnv)
        {
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            IntPtr newStringPtr = jInterface.NewStringPointer;
            NewStringDelegate newString = newStringPtr.GetUnsafeDelegate<NewStringDelegate>()!;

            return newString(jEnv, ctx.Pointer, ctx.Values.Length);
        }
        internal static String GetString(JStringLocalRef jStr, JEnvRef jEnv)
        {
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;
            GetStringLengthDelegate getStringLength =
                jInterface.GetStringLengthPointer.GetUnsafeDelegate<GetStringLengthDelegate>()!;

            return String.Create(getStringLength(jEnv, jStr), (jStr, jEnv), ExportedMethods.CreateString);
        }
        internal static Int32 LoadLibrary(JavaVMRef vm, IntPtr unknown)
        {
            ExportedMethods.load = DateTime.Now;
            ExportedMethods.count = 0;
            return 0x00010006; //JNI_VERSION_1_6
        }
        
        private static String GetRuntimeInformation(DateTime call)
            => $"Load: {ExportedMethods.load.GetString()}" + Environment.NewLine
            + $"Call: {call.GetString()}" + Environment.NewLine
            + $"Count: {ExportedMethods.count}"
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
            + ExportedMethods.GetRuntimeInformation();
        private static String GetRuntimeInformation()
            => !ExportedMethods.DisabledReflection ? ExportedMethods.GetRuntimeReflectionInformation() : "REFLECTION DISABLED";
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
            => ((DateTime?)date).GetString();
        private static String GetString(this DateTime? date)
            => date != default ? date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") : "null";
        private static void CreateString(Span<Char> buffer, (JStringLocalRef jStr, JEnvRef jEnv) args)
        {
            JEnvValue value = args.jEnv.Environment;
            Int32 length = buffer.Length;
            ref JNativeInterface jInterface = ref value.Functions;
            GetStringRegionDelegate getStringRegion =
                jInterface.GetStringRegionPointer.GetUnsafeDelegate<GetStringRegionDelegate>()!;
            getStringRegion( args.jEnv, args.jStr, 0, length, buffer);
        }
    }
#pragma warning restore IDE0060
}