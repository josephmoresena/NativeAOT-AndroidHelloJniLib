using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;

namespace HelloJniLib;

public partial class AndroidToast
{
	private sealed class ShowMethod() : JMethodDefinition("show"u8)
	{
		public static readonly ShowMethod Instance = new();

		public void Invoke(AndroidToast toast)
			=> base.Invoke(toast, JClassObject.GetClass<AndroidToast>(toast.Environment));
	}
}