using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
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
			MakeTextArg args = new(context, text, duration);
			return this.StaticInvoke(toastClass, ref args)!;
		}

		private readonly struct MakeTextArg(
			AndroidContext context,
			IInterfaceObject<JCharSequenceObject> text,
			Length duration) : ICallArgument
		{
			public void Configure<TSlot>(TSlot slot) where TSlot : IParameterSlot
			{
				slot.SetParameterValue(0, context);
				slot.SetParameterValue(1, text);
				slot.SetParameterValue(2, (JInt)(Int32)duration);
			}
		}
	}
}