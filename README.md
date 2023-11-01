# HelloJniLib
 This repo is a sample for Android JNI libraries compilation with NativeAOT.
 For now we support two ways to compile this library: 
 1. Using BFlat tool https://github.com/bflattened/bflat. 
 2. Using .NET SDK 8.0
 
# BFlat
In order to optimize the build process we recommend the use of BFlat tool https://github.com/bflattened/bflat. 
This tool works on both Windows and Linux, so WSL envirionment is not needed for android compilation.

### Prerequisites
This library dependends of https://github.com/josephmoresena/Rxmxnx.PInvoke.Extensions so you need download the 
latest version of package https://www.nuget.org/api/v2/package/Rxmxnx.PInvoke.Extensions, extract dll file to 
BFlatSupport folder.

### Build
Use the following command to build library.

	    {PATH_TO_BFLAT}bflat build -r BFlatSupport/Rxmxnx.PInvoke.Extensions.dll --no-stacktrace-data --no-globalization --no-exception-messages -Os --no-pie --separate-symbols --os:linux --arch:arm64 --libc:bionic -o:libhello-jni.so

### How to build it
This process was tested for android-arm64 compilation but may works for android-x64 too. <br/>
The following commands assume:
 * ANDROID_NDK_ROOT envirionment variable: Full path to NDK. Used to preconfigure CppCompilerAndLinker, ObjCopyName and SysRoot.
 * Android NDK version is r26b.
 * Target architecture is linux-bionic-arm64 or linux-bionic-x64.
 * Host architecture is windows, linux or macOS x64.

	   dotnet publish -r linux-bionic-arm64 -p:DisableUnsupportedError=true -p:PublishAotUsingRuntimePack=true -p:AssemblyName=libhello-jni -p:RemoveSections=true \

#### Environment Parameters 
* ObjCopyName: Path of NDK ObjCopy. This is needed in order to use StripSymbols MSBuild parameter.
* RealCppCompilerAndLinker: NDK linker tool. This is needed to apply a hack for excluding the missing libraries at linking process.

#### MSBuild Parameters
* CppCompilerAndLinker: Linker. The fakeClang is just an script that invokes the real NDK Clang executable.
* SysRoot: Sysroot path from NDK. Needed for NDK compilation.
* RemoveSections: Hack to remove __init and __fini symbols from .exports file.
* AssemblyName: In order to produce a .so file with given name.
* UseLibCSections: In order to use __libc_init and __libc_fini as exported __init and __fini symbols.

### Dependencies
The native Android binary produced may has a dependency with libc++_shared.so. So you need include it along with the generated binary. <br/>
You can get it for arm64 from:

	/toolchains/llvm/prebuilt/{windows/linux/darwin}-x86_64/sysroot/usr/lib/{aarch64/x86_x64}-linux-android

## Considerations
* You can also build this in reflection-free mode, it will significantly reduce the size of the binary.

# How to test it?
This sample is inspired by https://github.com/android/ndk-samples/tree/main/hello-jni so with some changes you can load this library from that application. <br/>
## Considerations:
* The package of the calling class: com_example_hellojni
* The name of the calling class: HelloJni.
* The name of the native function in the calling class: stringFromJNI.
* In the above example the name of the JNI library: libhello-jni.so.
