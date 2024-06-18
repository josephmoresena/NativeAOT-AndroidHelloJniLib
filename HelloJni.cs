using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;

namespace HelloJniLib;

internal sealed class HelloJni : IDisposable
{
	private readonly DateTime? _load;
	private readonly JGlobal _looperClass;
	private readonly JGlobal _toastClass;
	private Task _backgroundThread = Task.CompletedTask;
	private Int32 _count;
	private JGlobalBase? _currentContext;

	private Boolean _disposed;

	public IVirtualMachine VirtualMachine { get; }

	public HelloJni(IVirtualMachine vm, DateTime? load = default)
	{
		this.VirtualMachine = vm;
		this._load = load;

		using IThread
			thread = this.VirtualMachine.InitializeThread(new(() => "OnLoad"u8)); // Attaches current thread to VM.
		using JClassObject androidLooperClass = JClassObject.GetClass<AndroidLooper>(thread); // Loads android.os.Looper
		using JClassObject
			androidToastClass = JClassObject.GetClass<AndroidToast>(thread); // Loads android.widget.Toast

		// We need to hold global classes references to avoid .NET GC collect them.
		this._looperClass = androidLooperClass.Global; // Creates a global instance for android.os.Looper
		this._toastClass = androidToastClass.Global; // Creates a global instance for android.widget.Toast
	}

	public void Dispose()
	{
		this.ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	public JStringObject GetString(DateTime call, IEnvironment env, AndroidContext context)
	{
		env.LocalCapacity = 20;
		if (this._backgroundThread.IsCompleted) // Background task is running?
		{
			this._currentContext ??= context.Weak; // Initialize a global reference for current activity.
			this._backgroundThread = // Creates background task.
				Task.Factory.StartNew(HelloJni.ToastBackground, this, TaskCreationOptions.LongRunning);
		}
		this._count++;
		String result = "Hello from JNI! Compiled with NativeAOT." + Environment.NewLine +
			ExportedMethods.GetRuntimeInformation(call, this._load, this._count);
		return JStringObject.Create(env, result);
	}

	~HelloJni() { this.ReleaseUnmanagedResources(); }

	private void ReleaseUnmanagedResources()
	{
		if (this._disposed) return;
		this._disposed = true;
		this._currentContext?.Dispose(); // Removes weak-global instance.
		this._toastClass.Dispose(); // Removes global class instance.
		this._looperClass.Dispose(); // Removes global class instance.
	}

	private static void ToastBackground(Object? obj)
	{
		if (obj is not HelloJni instance) return; // Weak-global activity reference not exists
		if (instance._currentContext is null) return; // Weak-global activity reference not exists
		using IThread thread = // Attaches current thread to VM.
			instance._currentContext.VirtualMachine.InitializeDaemon(new(() => "ToastBackground"u8));
		ExportedMethods.LogAndroid($"Daemon {Environment.CurrentManagedThreadId}, {thread.Reference}, {thread.Name}");
		try
		{
			if (!instance._currentContext.IsValid(thread)) // Weak-global activity is invalid?
			{
				instance._currentContext.Dispose(); // Unloads weak-global reference.
				instance._currentContext = default; // Sets weak-global reference to null.
				return;
			}

			Int32 i = 0;
			AndroidLooper.Prepare(thread); // Prepares current thread to show Toast.
			using AndroidContext context = // Creates context instance (not local reference) from weak-global reference
				instance._currentContext.AsLocal<AndroidContext>(thread);
			while (instance._count - i > 0)
			{
				using AndroidToast toast = // Invokes Toast.makeText
					AndroidToast.MakeText(context, $"Random {i}: {Random.Shared.Next()}", AndroidToast.Length.Short);
				toast.Show(); // Invokes Toast.show
				Thread.Sleep(1000);
				i++;
			}
		}
		catch (Exception e)
		{
			ExportedMethods.LogAndroid($"Exception: {e}");
			if (e is not ThrowableException throwableException) return;
			String throwableToString = throwableException.WithSafeInvoke(t => t.ToString());
			ExportedMethods.LogAndroid(throwableToString);
			// Clear JNI exception.
			thread.PendingException = default;
		}
	}
}