using System.Diagnostics;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.PInvoke;

namespace HelloJniLib;

internal static class ExportedMethods
{
	private const String helloJniName = "com_example_hellojni_HelloJni";

	private static HelloJni? instance;

	static ExportedMethods()
	{
		// Android datatypes registration
		JVirtualMachine.Register<AndroidLooper>();
		JVirtualMachine.Register<AndroidToast>();
		JVirtualMachine.Register<AndroidContext>();
	}

	public static String GetRuntimeInformation(DateTime call, DateTime? load, Int32 count)
		=> $"Load: {load.GetString()}" + Environment.NewLine + $"Call: {call.GetString()}" + Environment.NewLine +
			$"Count: {count}" + Environment.NewLine + Environment.NewLine +
			$"Number of Cores: {Environment.ProcessorCount}" + Environment.NewLine +
			$"OS: {RuntimeInformation.OSDescription}" + Environment.NewLine +
			$"OS Arch: {RuntimeInformation.OSArchitecture.GetName()}" + Environment.NewLine +
			$"OS Version: {Environment.OSVersion}" + Environment.NewLine + $"Computer: {Environment.MachineName}" +
			Environment.NewLine + $"User: {Environment.UserName}" + Environment.NewLine +
			$"System Path: {Environment.SystemDirectory}" + Environment.NewLine +
			$"Current Path: {Environment.CurrentDirectory}" + Environment.NewLine +
			$"Process Arch: {RuntimeInformation.ProcessArchitecture.GetName()}" + Environment.NewLine +
			$"Pubic Ip: {ExportedMethods.GetIp()}" + Environment.NewLine + Environment.NewLine +
			ExportedMethods.GetRuntimeInformation();
	public static void LogAndroid(String text)
	{
		// To write the text as complete as possible in logcat, it must be divided into lines.
		foreach (String tLine in text.Split(Environment.NewLine))
			Trace.WriteLine(tLine);
	}

	[UnmanagedCallersOnly(EntryPoint = "JNI_OnLoad")]
	private static Int32 LoadLibrary(JVirtualMachineRef vmRef, IntPtr _)
	{
		DateTime call = DateTime.Now;
		Trace.AutoFlush = true;
		IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(vmRef); // Creates a managed VM instance.
		ExportedMethods.instance = new(vm, call);
		// JNetInterface requires at least JNI_VERSION_1_6 
		return IVirtualMachine.MinimalVersion;
	}
	[UnmanagedCallersOnly(EntryPoint = "JNI_OnUnload")]
	private static void UnloadLibrary(JVirtualMachineRef vmRef, IntPtr _)
	{
		// Android doesn't call JNI_OnUnload but it would be an implementation
		if (ExportedMethods.instance?.VirtualMachine.Reference != vmRef)
			return;
		ExportedMethods.instance.Dispose();
		JVirtualMachine.RemoveVirtualMachine(vmRef); // Removes all resources uses by current VM.
	}
	[UnmanagedCallersOnly(EntryPoint = "Java_" + ExportedMethods.helloJniName + "_stringFromJNI")]
	private static JStringLocalRef Hello(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		ExportedMethods.LogAndroid($"Instance call, {envRef}, {localRef}");
		DateTime call = DateTime.Now;
		JNativeCallAdapter adapter = // Creates call adapter initializing context object. 
			(ExportedMethods.instance is not null ?
				JNativeCallAdapter.Create(ExportedMethods.instance.VirtualMachine, envRef, localRef,
				                          out AndroidContext context) :
				JNativeCallAdapter.Create(envRef, localRef, out context)).Build();
		ExportedMethods.instance ??= // Initializes the instance if JNI_OnLoad was not called.
			new(adapter.Environment.VirtualMachine);
		JStringObject jString = ExportedMethods.instance.GetString(call, adapter.Environment, context);
		return adapter.FinalizeCall(jString); // Finalize adapter with a JString result.
	}

	private static String GetRuntimeInformation()
		=> !AotInfo.IsReflectionDisabled ? ExportedMethods.GetRuntimeReflectionInformation() : "REFLECTION DISABLED";
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
	private static String GetString(this DateTime date) => ((DateTime?)date).GetString();
	private static String GetString(this DateTime? date)
		=> date is not null ? date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") : "null";
	private static String GetIp()
	{
		try
		{
			using HttpClient httpClient = new();
			using HttpRequestMessage request = new(HttpMethod.Get, "https://api64.ipify.org/?format=text");
			using HttpResponseMessage response = httpClient.Send(request);
			return response.Content.ReadAsStringAsync().Result;
		}
		catch (Exception e)
		{
			return e.Message;
		}
	}
}