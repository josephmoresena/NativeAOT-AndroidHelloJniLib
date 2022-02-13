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
        private static readonly Boolean DisabledReflection = !typeof(String).ToString().Contains(nameof(String));

        [UnmanagedCallersOnly(EntryPoint = "Java_com_example_hellojni_HelloJni_stringFromJNI")]
        internal static JStringLocalRef Hello(JEnvRef jEnv, JObjectLocalRef jObj)
        {
            JEnvValue value = jEnv.Environment;
            ref JNativeInterface jInterface = ref value.Functions;

            IntPtr newStringPtr = jInterface.NewStringPointer;
            NewStringDelegate newString = newStringPtr.AsDelegate<NewStringDelegate>();

            String result =
                "Hello from JNI! Compiled with NativeAOT." + Environment.NewLine
                + GetRuntimeInformation();
            return newString(jEnv, result.AsSpan().AsIntPtr(), result.Length);
        }

        private static String GetRuntimeInformation()
            => $"Number of Cores: {Environment.ProcessorCount}" + Environment.NewLine
            + $"OS: {RuntimeInformation.OSDescription}" + Environment.NewLine
            + $"OS Arch: {RuntimeInformation.OSArchitecture.GetName()}" + Environment.NewLine
            + $"OS Version: {Environment.OSVersion}" + Environment.NewLine
            + $"Computer: {Environment.MachineName}" + Environment.NewLine
            + $"User: {Environment.UserName}" + Environment.NewLine
            + $"System Path: {Environment.SystemDirectory}" + Environment.NewLine
            + $"Current Path: {Environment.CurrentDirectory}" + Environment.NewLine
            + $"Process Arch: {RuntimeInformation.ProcessArchitecture.GetName()}" + Environment.NewLine
            + GetRuntimeReflectionInformation()
            //+ $"Framework Version: {Environment.Version}" + Environment.NewLine //Error
            + "";

        private static String GetRuntimeReflectionInformation()
        {
            if (!DisabledReflection)
                return $"Process Arch: {RuntimeInformation.ProcessArchitecture}" + Environment.NewLine
                    + $"Runtime Name: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine
                    + $"Runtime Path: {RuntimeEnvironment.GetRuntimeDirectory()}" + Environment.NewLine
                    + $"Runtime Version: {RuntimeEnvironment.GetSystemVersion()}" + Environment.NewLine;
            return String.Empty;
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
    }
#pragma warning restore IDE0060
}