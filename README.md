# HelloJniLib
 This repo is a sample for Android JNI libraries compilation with NativeAOT.
 
# Prerequisites
This sample requires the NativeAOT Runtime and Libraries for Android Systems. <br/>
To get them by now you need to build them by your self using https://github.com/MichalStrehovsky/runtime/commit/0e1708375f93f66476179c54f8bb2123a88b901a

# How to build it
This process was tested for android-arm64 compilation but may works for android-x64 too. <br/>
The following command assumes:
 * The working directory is the same of this repo. 
 * ANDROID_NDK_ROOT envirionment variable: Full path to NDK. 
 * RUNTIME_REPO envirionment variable: Full path to runtime repo.
 * Android NDK version is r21b.
 * Android API version target is 28.
 * Android API minimal version is 21.
 * Target architecture is arm64.
 * Host architecture is x64.

   libSystemPath=$RUNTIME_REPO/artifacts/bin/runtime/net7.0-Android-Release-arm64/ \
   RealCppCompilerAndLinker=$ANDROID_NDK_ROOT/toolchains/llvm/prebuilt/linux-x86_64/bin/aarch64-linux-android21-clang \
   dotnet publish -r android-arm64 -c Release --self-contained \
	/p:CppCompilerAndLinker=./fakeClang \
	/p:SysRoot=$ANDROID_NDK_ROOT/toolchains/llvm/prebuilt/linux-x86_64/sysroot \
	/p:IlcSdkPath=$RUNTIME_REPO/artifacts/bin/coreclr/Android.arm64.Release/aotsdk/ \
	/p:IlcFrameworkPath=$RUNTIME_REPO/artifacts/bin/runtime/net7.0-Android-Release-arm64/ \
	/p:IlcFrameworkNativePath=$RUNTIME_REPO/artifacts/bin/runtime/net7.0-Android-Release-arm64/ 

## Parameters
	* libSystemPath: Path of libSystem native libraries. This is needed to apply a hack for excluding the missing libraries.
	* RealCppCompilerAndLinker: NDK linker tool. This is needed to apply a hack for excluding the missing libraries at linking process.
	* CppCompilerAndLinker: Linker. The fakeClang is just an script that removes the missing libraries from linker invocation.
	* SysRoot: Sysroot path from NDK. Needed for NDK compilation.
	* IlcSdkPath: Path to NativeAOT libs. We can't use the standard libraries from ilcompiler.
	* IlcFrameworkPath: Path to NativeAOT runtime (Managed part). We can't use the standard runtime from ilcompiler.
	* IlcFrameworkNativePath: Path to NativeAOT runtime (Native part or libSystem). We can't use the standard runtime from ilcompiler.

## Considerations
	* You can also build this in reflection-free mode, it will significantly reduce the size of the binary.

# How to strip an android binary
You can't strip an android binary using strip command from your linux envirionment so you need use from Android NDK. <br/>
For example:
	ANDROID_NDK_ROOT/toolchains/llvm/prebuilt/linux-x86_64/bin/x86_64-linux-android-strip bin/Release/net6.0/android-arm64/publish/AndroidHelloJniLib.so

# How to test it?
This sample is inspired by https://github.com/android/ndk-samples/tree/main/hello-jni so with some changes you can load this library from that application. <br/>
## Considerations:
  * The package of the calling class: com_example_hellojni
  * The name of the calling class: HelloJni.
  * The name of the native function in the calling class: stringFromJNI.
  * In the above example the name of the JNI library: libhello-jni.so.

# Dependencies
The native Android binary produced has a dependency with libc++_shared.so. So you need include it along with the generated binary. <br/>
You can get it for arm64 from:
	/toolchains/llvm/prebuilt/linux-x86_64/sysroot/usr/lib/aarch64-linux-android