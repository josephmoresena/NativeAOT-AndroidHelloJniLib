using System.Runtime.InteropServices;

using HelloJniLib.Jni;
using HelloJniLib.Jni.Identifiers;
using HelloJniLib.Jni.Pointers;
using HelloJniLib.Jni.Primitives;
using HelloJniLib.Jni.References;
using HelloJniLib.Jni.Values;

using Rxmxnx.PInvoke;

namespace HelloJniLib;

public static class ExportedMethods
{
	private static readonly Boolean disabledReflection = !typeof(String).ToString().Contains(nameof(String));

	private static DateTime? load;
	private static JavaVMRef? loadedJavaVm;
	private static JGlobalRef? looperGlobalClass;
	private static JMethodId? prepareMethodId;
	private static JGlobalRef? toastGlobalClass;
	private static JMethodId? makeTextMethodId;
	private static JMethodId? showMethodId;
	private static JWeakRef? context;
	private static Int32 count;
	private static Task backgroundTask = Task.CompletedTask;

	[UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
	internal static Int32 LoadLibrary(JavaVMRef javaVm, IntPtr unknown)
	{
		ExportedMethods.load = DateTime.Now;
		ExportedMethods.loadedJavaVm = javaVm;

		"OnLoad"u8.WithSafeFixed(new AttachFuncion(javaVm), out AttachResult attach);
		if (!attach.JEnv.HasValue) return JniHelper.JniVersion;
		try
		{
			"android/os/Looper"u8.WithSafeFixed(new GlobalClassFunction(attach.JEnv.Value),
			                                    out ExportedMethods.looperGlobalClass);
			"android/widget/Toast"u8.WithSafeFixed(new GlobalClassFunction(attach.JEnv.Value),
			                                       out ExportedMethods.toastGlobalClass);
		}
		finally
		{
			if (ExportedMethods.looperGlobalClass.HasValue && !ExportedMethods.toastGlobalClass.HasValue)
				JniHelper.RemoveGlobal(attach.JEnv.Value, ExportedMethods.looperGlobalClass.Value);
			if (attach.NewAttach)
				JniHelper.Detach(javaVm);
		}
		return JniHelper.JniVersion;
	}

	[UnmanagedCallersOnly(EntryPoint = "JNI_OnUnload")]
	internal static void UnloadLibrary(JavaVMRef javaVm, IntPtr unknown)
	{
		if (ExportedMethods.loadedJavaVm != javaVm) return;
		"OnUnload"u8.WithSafeFixed(new AttachFuncion(javaVm), out AttachResult attach);
		if (attach.JEnv.HasValue)
		{
			ExportedMethods.backgroundTask.Wait();
			if (ExportedMethods.context.HasValue)
				JniHelper.RemoveWeakGlobal(attach.JEnv.Value, ExportedMethods.context.Value);
			if (ExportedMethods.toastGlobalClass.HasValue)
			{
				JniHelper.RemoveGlobal(attach.JEnv.Value, ExportedMethods.toastGlobalClass.Value);
				ExportedMethods.toastGlobalClass = default;
				ExportedMethods.showMethodId = default;
				ExportedMethods.makeTextMethodId = default;
			}
			if (ExportedMethods.looperGlobalClass.HasValue)
			{
				JniHelper.RemoveGlobal(attach.JEnv.Value, ExportedMethods.looperGlobalClass.Value);
				ExportedMethods.looperGlobalClass = default;
				ExportedMethods.prepareMethodId = default;
			}
		}
		if (attach.NewAttach)
			JniHelper.Detach(javaVm);
		ExportedMethods.loadedJavaVm = default;
	}

	[UnmanagedCallersOnly(EntryPoint = "Java_com_example_hellojni_HelloJni_stringFromJNI")]
	internal static JStringLocalRef Hello(JEnvRef jEnv, JObjectLocalRef jObj)
	{
		DateTime call = DateTime.Now;
		ExportedMethods.count++;

		if (ExportedMethods.backgroundTask.IsCompleted)
		{
			ExportedMethods.loadedJavaVm ??= JniHelper.GetVirtualMachine(jEnv);
			ExportedMethods.context ??= JniHelper.CreateWeakGlobal(jEnv, jObj);
			if (ExportedMethods.context.HasValue)
				ExportedMethods.backgroundTask =
					Task.Factory.StartNew(ExportedMethods.ToastBackground, TaskCreationOptions.LongRunning);
		}

		String result = "Hello from JNI! Compiled with NativeAOT (main)." + Environment.NewLine +
			ExportedMethods.GetRuntimeInformation(call);
		return JniHelper.CreateString(jEnv, result).GetValueOrDefault();
	}
	private static void ToastBackground()
	{
		if (!ExportedMethods.context.HasValue) return;
		if (!ExportedMethods.toastGlobalClass.HasValue) return;
		if (!ExportedMethods.looperGlobalClass.HasValue) return;
		if (!ExportedMethods.loadedJavaVm.HasValue) return;

		"ToastBackground"u8.WithSafeFixed(new AttachDaemonFunction(ExportedMethods.loadedJavaVm.Value),
		                                  out JEnvRef? jEnv);
		if (!jEnv.HasValue) return;
		try
		{
			JBoolean? isValid = JniHelper.IsValidGlobalWeak(jEnv.Value, ExportedMethods.context.Value);
			if (!isValid.GetValueOrDefault())
			{
				if (isValid.HasValue && !(Boolean)isValid.Value)
					JniHelper.RemoveWeakGlobal(jEnv.Value, ExportedMethods.context.Value);
				ExportedMethods.context = default;
				return;
			}

			Int32 i = 0;
			ExportedMethods.prepareMethodId ??= ExportedMethods.GetPrepareMethodId(jEnv.Value);
			ExportedMethods.makeTextMethodId ??= ExportedMethods.GetMakeTextMethodId(jEnv.Value);
			ExportedMethods.showMethodId ??= ExportedMethods.GetShowMethodId(jEnv.Value);

			if (!ExportedMethods.prepareMethodId.HasValue) return;
			if (!ExportedMethods.makeTextMethodId.HasValue) return;
			if (!ExportedMethods.showMethodId.HasValue) return;

			JClassLocalRef looperClass = (JClassLocalRef)(JObjectLocalRef)ExportedMethods.looperGlobalClass;
			JClassLocalRef toastClass = (JClassLocalRef)(JObjectLocalRef)ExportedMethods.toastGlobalClass;

			JniHelper.CallStaticVoidMethod(jEnv.Value, looperClass, ExportedMethods.prepareMethodId.Value);

			Span<Byte> toastLength = stackalloc Byte[sizeof(Int32)];
			toastLength.AsValue<Int32>() = 0;
			while (ExportedMethods.count - i > 0)
			{
				JStringLocalRef? jString = JniHelper.CreateString(jEnv.Value, $"Random {i}: {Random.Shared.Next()}");
				if (!jString.HasValue) return;
				JObjectLocalRef? jObject = JniHelper.CallStaticObjectMethod(
					jEnv.Value, toastClass, ExportedMethods.makeTextMethodId.Value,
					(JValue)(JObjectLocalRef)ExportedMethods.context.Value, (JValue)(JObjectLocalRef)jString.Value,
					JValue.Create(toastLength.AsBytes()));
				try
				{
					if (!jObject.HasValue) return;
					JniHelper.CallVoidMethod(jEnv.Value, jObject.Value, ExportedMethods.showMethodId.Value);
				}
				finally
				{
					if (jObject.HasValue)
						JniHelper.RemoveLocal(jEnv.Value, jObject.Value);
					JniHelper.RemoveLocal(jEnv.Value, (JObjectLocalRef)jString.Value);
					Thread.Sleep(1000);
					i++;
				}
			}
		}
		finally
		{
			JniHelper.Detach(ExportedMethods.loadedJavaVm.Value);
		}
	}

	private static JMethodId? GetPrepareMethodId(JEnvRef jEnv)
	{
		ReadOnlySpan<Byte> name = "prepare"u8;
		ReadOnlySpan<Byte> descriptor = "()V"u8;
		JClassLocalRef jClass = (JClassLocalRef)(JObjectLocalRef)ExportedMethods.looperGlobalClass.GetValueOrDefault();
		using IDisposable _ = name.GetUnsafeValPtr()
		                          .GetUnsafeFixedContext(name.Length, FixedPointerValue.UnsafeDisposable,
		                                                 out ReadOnlyFixedContextValue<Byte> fName);
		descriptor.GetUnsafeValPtr()
		          .GetUnsafeFixedContext(name.Length, _, out ReadOnlyFixedContextValue<Byte> fDescriptor);
		return JniHelper.GetStaticMethodId(jEnv, jClass, fName, fDescriptor);
	}
	private static JMethodId? GetMakeTextMethodId(JEnvRef jEnv)
	{
		ReadOnlySpan<Byte> name = "makeText"u8;
		ReadOnlySpan<Byte> descriptor = "(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;"u8;
		JClassLocalRef jClass = (JClassLocalRef)(JObjectLocalRef)ExportedMethods.toastGlobalClass.GetValueOrDefault();
		using IDisposable _ = name.GetUnsafeValPtr()
		                          .GetUnsafeFixedContext(name.Length, FixedPointerValue.UnsafeDisposable,
		                                                 out ReadOnlyFixedContextValue<Byte> fName);
		descriptor.GetUnsafeValPtr()
		          .GetUnsafeFixedContext(name.Length, _, out ReadOnlyFixedContextValue<Byte> fDescriptor);
		return JniHelper.GetStaticMethodId(jEnv, jClass, fName, fDescriptor);
	}
	private static JMethodId? GetShowMethodId(JEnvRef jEnv)
	{
		ReadOnlySpan<Byte> name = "show"u8;
		ReadOnlySpan<Byte> descriptor = "()V"u8;
		JClassLocalRef jClass = (JClassLocalRef)(JObjectLocalRef)ExportedMethods.toastGlobalClass.GetValueOrDefault();
		using IDisposable _ = name.GetUnsafeValPtr()
		                          .GetUnsafeFixedContext(name.Length, FixedPointerValue.UnsafeDisposable,
		                                                 out ReadOnlyFixedContextValue<Byte> fName);
		descriptor.GetUnsafeValPtr()
		          .GetUnsafeFixedContext(name.Length, _, out ReadOnlyFixedContextValue<Byte> fDescriptor);
		return JniHelper.GetMethodId(jEnv, jClass, fName, fDescriptor);
	}

	private static String GetRuntimeInformation(DateTime call)
		=> $"Load: {ExportedMethods.load.GetString()}" + Environment.NewLine + $"Call: {call.GetString()}" +
			Environment.NewLine + $"Count: {ExportedMethods.count}" + Environment.NewLine + Environment.NewLine +
			$"Number of Cores: {Environment.ProcessorCount}" + Environment.NewLine +
			$"Little-Endian: {BitConverter.IsLittleEndian}" + Environment.NewLine +
			$"OS: {RuntimeInformation.OSDescription}" + Environment.NewLine +
			$"OS Arch: {RuntimeInformation.OSArchitecture.GetName()}" + Environment.NewLine +
			$"OS Version: {Environment.OSVersion}" + Environment.NewLine + $"Computer: {Environment.MachineName}" +
			Environment.NewLine + $"User: {Environment.UserName}" + Environment.NewLine +
			$"System Path: {Environment.SystemDirectory}" + Environment.NewLine +
			$"Current Path: {Environment.CurrentDirectory}" + Environment.NewLine +
			$"Process Arch: {RuntimeInformation.ProcessArchitecture.GetName()}" + Environment.NewLine +
			Environment.NewLine + ExportedMethods.GetRuntimeInformation();
	private static String GetRuntimeInformation()
		=> !ExportedMethods.disabledReflection ?
			ExportedMethods.GetRuntimeReflectionInformation() :
			"REFLECTION DISABLED";
	private static String GetRuntimeReflectionInformation()
		=> $"Framework Version: {Environment.Version}" + Environment.NewLine +
			$"Runtime Name: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine +
			$"Runtime Path: {RuntimeEnvironment.GetRuntimeDirectory()}" + Environment.NewLine +
			$"Runtime Version: {RuntimeEnvironment.GetSystemVersion()}" + Environment.NewLine + Environment.NewLine;
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
	{
		DateTime? datetime = date;
		return datetime.GetString();
	}
	private static String GetString(this DateTime? date)
		=> date != default ? date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") : "null";

	private readonly struct AttachResult
	{
		public JEnvRef? JEnv { get; init; }
		public Boolean NewAttach { get; init; }
	}

	#region FixedStructs
	private readonly struct AttachFuncion(JavaVMRef javaVm) : IReadOnlyFixedContextFunction<Byte, AttachResult>
	{
		public AttachResult Apply(scoped ReadOnlyFixedContextValue<Byte> threadName)
			=> new() { JEnv = JniHelper.Attach(javaVm, threadName, out Boolean newAttach), NewAttach = newAttach, };
	}

	private readonly struct AttachDaemonFunction(JavaVMRef javaVm) : IReadOnlyFixedContextFunction<Byte, JEnvRef?>
	{
		public JEnvRef? Apply(scoped ReadOnlyFixedContextValue<Byte> daemonName)
			=> JniHelper.AttachDaemon(javaVm, daemonName);
	}

	private readonly struct GlobalClassFunction(JEnvRef jEnv) : IReadOnlyFixedContextFunction<Byte, JGlobalRef?>
	{
		public JGlobalRef? Apply(scoped ReadOnlyFixedContextValue<Byte> fName) => JniHelper.GetGlobalClass(jEnv, fName);
	}
	#endregion
}