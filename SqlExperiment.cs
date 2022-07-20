using HelloJniLib.Jni;
using HelloJniLib.Jni.Identifiers;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

using Microsoft.Data.SqlClient;

using Rxmxnx.PInvoke.Extensions;

namespace HelloJniLib
{
    internal static class SqlExperiment
    {
        private static readonly CString printSqlResultName = "printSqlResult";
        private static readonly CString printSqlResultSignature = "(Ljava/lang/String;)V";

        internal static async void ConnectAsync(JavaVMRef vm, JWeakRef jWeak, String strConn)
        {
            String result;
            try
            {
                using SqlConnection conn = new(strConn);
                Console.WriteLine("Connection created.");
                await conn.OpenAsync();
                Console.WriteLine("Open connection.");
                using SqlCommand comm = new("SELECT @@VERSION;", conn);
                Console.WriteLine("Command created.");
                result = comm.ExecuteScalar().ToString();
                Console.WriteLine("Command executed.");
            }
            catch (Exception ex)
            {
                if (!ExportedMethods.DisabledReflection)
                    result = ex.ToString();
                else
                    result = ex.Message;
            }

            await Task.Run(() => PrintResult(vm, jWeak, result));
        }

        private static void PrintResult(JavaVMRef vm, JWeakRef jWeak, String result)
        {
            Console.WriteLine(result);
            JEnvRef jEnv = default;
            JavaVMValue vmValue = vm.VirtualMachine;
            ref JInvokeInterface jVMInterface = ref vmValue.Functions;

            AttachCurrentThreadDelegate attachCurrentThread =
                jVMInterface.AttachCurrentThreadPointer.AsDelegate<AttachCurrentThreadDelegate>();
            DetachCurrentThreadDelegate detachCurrentThread =
                jVMInterface.DetachCurrentThreadPointer.AsDelegate<DetachCurrentThreadDelegate>();

            attachCurrentThread(vm, ref jEnv, new()
            {
                Version = 0x00010006
            });

            try
            {
                JEnvValue value = jEnv.Environment;
                ref JNativeInterface jInterface = ref value.Functions;

                GetObjectClassDelegate getObjectClass =
                    jInterface.GetObjectClassPointer.AsDelegate<GetObjectClassDelegate>();
                GetMethodIdDelegate getMethod =
                    jInterface.GetMethodIdPointer.AsDelegate<GetMethodIdDelegate>();
                CallVoidMethodADelegate callVoidMethodA =
                    jInterface.CallVoidMethodAPointer.AsDelegate<CallVoidMethodADelegate>();
                NewStringDelegate newString =
                    jInterface.NewStringPointer.AsDelegate<NewStringDelegate>();
                DeleteLocalRefDelegate deleteLocalRef =
                    jInterface.DeleteLocalRefPointer.AsDelegate<DeleteLocalRefDelegate>();

                JObjectLocalRef jObj = new(jWeak);
                JClassLocalRef jClass = getObjectClass(jEnv, jObj);
                JMethodId methodId = GetMethodId(jEnv, getMethod, jClass);
                JStringLocalRef jString = ExportedMethods.CreateJString(result, jEnv, newString);

                CallMethod(jEnv, callVoidMethodA, jObj, methodId, jString);

                deleteLocalRef(jEnv, jString.Value);
                deleteLocalRef(jEnv, jClass.Value);
            }
            finally
            {
                detachCurrentThread(vm);
            }
        }

        private static JMethodId GetMethodId(JEnvRef jEnv, GetMethodIdDelegate getMethod, JClassLocalRef jClass)
        {
            ReadOnlySpan<Byte> methodName = CString.GetBytes(printSqlResultName);
            ReadOnlySpan<Byte> methodSignature = CString.GetBytes(printSqlResultSignature);
            JMethodId methodId = getMethod(jEnv, jClass, methodName, methodSignature);
            return methodId;
        }
        private static void CallMethod(JEnvRef jEnv, CallVoidMethodADelegate callVoidMethodA, JObjectLocalRef jObj, JMethodId methodId, in JStringLocalRef jString)
        {
            Span<JValue> parameters = stackalloc JValue[1];
            parameters[0] = JValue.Create(jString.AsBytes());
            callVoidMethodA(jEnv, jObj, methodId, new(parameters));
        }
    }
}
