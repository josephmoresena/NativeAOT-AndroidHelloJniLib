using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace HelloJniLib;

public partial class AndroidToast
{
	private sealed class MakeTextMethod() : JFunctionDefinition<AndroidToast>("makeText"u8,
	                                                                          JArgumentMetadata.Get<AndroidContext>(),
	                                                                          JArgumentMetadata
		                                                                          .Get<JCharSequenceObject>(),
	                                                                          JArgumentMetadata.Get<JInt>())
	{
		public static readonly MakeTextMethod Instance = new();

		public AndroidToast Invoke(AndroidContext context, IInterfaceObject<JCharSequenceObject> text, Length duration)
		{
			JClassObject toastClass = JClassObject.GetClass<AndroidToast>(context.Environment);
			IObject?[] args = this.CreateArgumentsArray();
			args[0] = context;
			args[1] = text;
			args[2] = (JInt)(Int32)duration;
			return this.StaticInvoke(toastClass, args)!;
		}
	}
}