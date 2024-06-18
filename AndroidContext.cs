using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace HelloJniLib;

public class AndroidContext : JLocalObject, IClassType<AndroidContext>
{
	private static readonly JClassTypeMetadata<AndroidContext> typeMetadata = TypeMetadataBuilder<AndroidContext>
	                                                                          .Create("android/content/Context"u8,
		                                                                          JTypeModifier.Abstract).Build();

	static JClassTypeMetadata<AndroidContext> IClassType<AndroidContext>.Metadata => AndroidContext.typeMetadata;

	protected AndroidContext(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected AndroidContext(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected AndroidContext(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static AndroidContext IClassType<AndroidContext>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static AndroidContext IClassType<AndroidContext>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static AndroidContext IClassType<AndroidContext>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}