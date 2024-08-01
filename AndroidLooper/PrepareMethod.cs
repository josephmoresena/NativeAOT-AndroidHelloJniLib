using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;

namespace HelloJniLib;

public sealed partial class AndroidLooper
{
	private sealed class PrepareMethod() : JMethodDefinition("prepare"u8)
	{
		public static readonly PrepareMethod Instance = new();

		public void Invoke(IEnvironment env)
		{
			JClassObject looperClass = JClassObject.GetClass<AndroidLooper>(env);
			this.StaticInvoke(looperClass, []);
		}
	}
}