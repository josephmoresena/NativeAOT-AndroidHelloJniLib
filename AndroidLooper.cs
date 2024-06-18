using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace HelloJniLib;

public sealed partial class AndroidLooper : JLocalObject, IClassType<AndroidLooper>
{
	private static readonly JClassTypeMetadata<AndroidLooper> typeMetadata =
		TypeMetadataBuilder<AndroidLooper>.Create("android/os/Looper"u8, JTypeModifier.Final).Build();

	static JClassTypeMetadata<AndroidLooper> IClassType<AndroidLooper>.Metadata => AndroidLooper.typeMetadata;

	private AndroidLooper(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private AndroidLooper(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private AndroidLooper(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static void Prepare(IEnvironment env) => PrepareMethod.Instance.Invoke(env);

	static AndroidLooper IClassType<AndroidLooper>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static AndroidLooper IClassType<AndroidLooper>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static AndroidLooper IClassType<AndroidLooper>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}