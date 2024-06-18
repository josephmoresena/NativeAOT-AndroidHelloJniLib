using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace HelloJniLib;

public partial class AndroidToast : JLocalObject, IClassType<AndroidToast>
{
	public enum Length
	{
		Short = 0,
		Long = 1,
	}

	private static readonly JClassTypeMetadata<AndroidToast> typeMetadata =
		TypeMetadataBuilder<AndroidToast>.Create("android/widget/Toast"u8).Build();

	static JClassTypeMetadata<AndroidToast> IClassType<AndroidToast>.Metadata => AndroidToast.typeMetadata;

	protected AndroidToast(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected AndroidToast(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected AndroidToast(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void Show() => ShowMethod.Instance.Invoke(this);

	public static AndroidToast MakeText(AndroidContext context, String text, Length duration)
	{
		using JStringObject jText = JStringObject.Create(context.Environment, text);
		AndroidToast result = MakeTextMethod.Instance.Invoke(context, jText, duration);
		return result;
	}

	static AndroidToast IClassType<AndroidToast>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static AndroidToast IClassType<AndroidToast>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static AndroidToast IClassType<AndroidToast>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}