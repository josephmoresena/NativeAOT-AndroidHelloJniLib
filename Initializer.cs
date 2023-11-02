using System.Runtime.CompilerServices;

using HelloJniLib.Jni.Pointers;

using Rxmxnx.PInvoke;

namespace HelloJniLib
{
    internal class Initializer
    {
        private delegate Int32 JNILoadLibraryDelagate(JavaVMRef vm, IntPtr unknown);

#if ANDROID
        [System.Runtime.InteropServices.DllImport("System.Security.Cryptography.Native.Android")]
        private static extern Int32 SetJniOnLoad(IntPtr intPtr);
#endif

#pragma warning disable CA2255
        [ModuleInitializer]
#pragma warning restore CA2255
        internal static void Init()
        {
            try
            {
                JNILoadLibraryDelagate del = ExportedMethods.LoadLibrary;
                IntPtr ptr = del.GetUnsafeIntPtr();
#if ANDROID
                Console.WriteLine("Use System.Security.Cryptography.Native.Android method");
                SetJniOnLoad(ptr);
                Console.WriteLine($"JNI_onLoad registered {ptr}");
#else
                Console.WriteLine("Use NativeAOT exported method");
#endif
            }
            catch (Exception ex)
            {
                if (!ExportedMethods.DisabledReflection)
                    Console.Write(ex);
                else
                    Console.WriteLine(ex.Message);
            }
        }
    }
}
