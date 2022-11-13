# HelloJniLib
 This repo is a sample for Android JNI libraries compilation with NativeAOT.
 For now we support two ways to compile this library: 
 1. Using BFlat tool https://github.com/bflattened/bflat. 
 2. Using Runtime repo with https://github.com/MichalStrehovsky/runtime/commit/0e1708375f93f66476179c54f8bb2123a88b901a
 
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
 
## Runtime repository
The following section applies only if you want compile by yourself runtime repo in Lunux/WSL envirionment.

### Prerequisites
This sample requires the NativeAOT Runtime and Libraries for Android Systems. <br/>
To get them by now you need to build them by your self using https://github.com/MichalStrehovsky/runtime/commit/0e1708375f93f66476179c54f8bb2123a88b901a

### How to build it
This process was tested for android-arm64 compilation but may works for android-x64 too. <br/>
The following commands assume:
 * The working directory is the same of this repo. 
 * ANDROID_NDK_ROOT envirionment variable: Full path to NDK. Used to preconfigure CppCompilerAndLinker, ObjCopyName and SysRoot.
 * RUNTIME_REPO envirionment variable: Full path to runtime repo. Used to preconfigure IlcSdkPath, IlcFrameworkPath and IlcFrameworkNativePath.
 * Android NDK version is r21e.
 * Android API version target is 28.
 * Android API minimal version is 21.
 * Target architecture is arm64.
 * Host architecture is x64.

	   dotnet publish -r android-arm64 -c Release --self-contained \

#### Environment Parameters 
* ObjCopyName: Path of NDK ObjCopy. This is needed in order to use StripSymbols MSBuild parameter.
* libSystemPath: Path of libSystem native libraries. This is needed to apply a hack for excluding the missing libraries.
* RealCppCompilerAndLinker: NDK linker tool. This is needed to apply a hack for excluding the missing libraries at linking process.

#### MSBuild Parameters
* CppCompilerAndLinker: Linker. The fakeClang is just an script that removes the missing libraries from linker invocation.
* SysRoot: Sysroot path from NDK. Needed for NDK compilation.
* IlcSdkPath: Path to NativeAOT libs. We can't use the standard libraries from ilcompiler.
* IlcFrameworkPath: Path to NativeAOT runtime (Managed part). We can't use the standard runtime from ilcompiler.
* IlcFrameworkNativePath: Path to NativeAOT runtime (Native part or libSystem). We can't use the standard runtime from ilcompiler.

### Dependencies
The native Android binary produced has a dependency with libc++_shared.so. So you need include it along with the generated binary. <br/>
You can get it for arm64 from:

	/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/aarch64-linux-android

## Considerations
* You can also build this in reflection-free mode, it will significantly reduce the size of the binary.

# How to test it?
This sample is inspired by https://github.com/android/ndk-samples/tree/main/hello-jni so with some changes you can load this library from that application. <br/>
## Considerations:
* The package of the calling class: com_example_hellojni
* The name of the calling class: HelloJni.
* The name of the native function in the calling class: stringFromJNI.
* In the above example the name of the JNI library: libhello-jni.so.
