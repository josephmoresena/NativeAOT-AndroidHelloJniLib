# HelloJniLib

This repo is a sample for Android JNI libraries compilation with NativeAOT.
For now, we support two ways to compile this library:

1. Using BFlat tool.
2. Using .NET SDK 8.0

# BFlat

In order to optimize the build process we recommend the use of [BFlat tool](https://github.com/bflattened/bflat).
This tool works on both Windows and Linux, so WSL environment is not needed for android compilation.

### Prerequisites

This library depends on [Rxmxnx.PInvoke.Extensions](https://github.com/josephmoresena/Rxmxnx.PInvoke.Extensions), so you need download the latest
version of these packages:

* https://www.nuget.org/api/v2/package/Rxmxnx.PInvoke.Extensions

Extract dlls file to BFlatSupport folder.

### Build

Use the following command to build library.

	    {PATH_TO_BFLAT}bflat build -r BFlatSupport/Rxmxnx.PInvoke.Extensions.dll --no-stacktrace-data --no-globalization --no-exception-messages -Os --no-pie --separate-symbols --os:linux --arch:arm64 --libc:bionic -o:libhello-jni.so

# NET SDK 8.0

With new .NET 8.0 SDK we are now able to compile NativeAOT android binaries using linux-bionic RID.

### How to build it

This process was tested for android-arm64 (linux-bionic-arm64) compilation but may work for android-x64 (
linux-bionic-x64) too. <br/>
The following commands assume:

* **ANDROID_NDK_ROOT** environment variable: Full path to NDK. Used to preconfigure **CppCompilerAndLinker**, **ObjCopyName** and
  **SysRoot**.
* Android NDK version is **r26b**.
* Target architecture is **arm64**.
* Host architecture is windows, linux or macOS x64.

  dotnet publish -r linux-bionic-arm64 -p:DisableUnsupportedError=true -p:PublishAotUsingRuntimePack=true -p:
  AssemblyName=libhello-jni -p:RemoveSections=true

#### Environment Parameters

* **ObjCopyName**: Path of NDK **llvm-objcopy**. This is needed in order to use StripSymbols MSBuild parameter.

#### MSBuild Parameters

* **CppCompilerAndLinker**: Linker. The android_fake_clang is just a script that invokes the real NDK Clang executable.
* **SysRoot**: Sysroot path from NDK. Needed for NDK compilation.
* **RemoveSections**: Hack to remove **__init** and **__fini** symbols from .exports file.
* **AssemblyName**: In order to produce a .so file with given name.
* **UseLibCSections**: In order to use **__libc_init** and **__libc_fini** as exported **__init** and **__fini** symbols.

## Considerations

* You can also build this in reflection-free mode, it will significantly reduce the size of the binary.
* This branch uses direct JNI calls via PInvoke but does not require the use of unsafe context.
* All rules for JNI call invocations apply to this implementation.
* The Jni namespace contains the structures and delegates necessary to interact with JNI securely.
* If you want to use JNI in a more friendly and familiar way with .NET code, check out the [jnetinterface](https://github.com/josephmoresena/NativeAOT-AndroidHelloJniLib/tree/jnetinterface) branch.

# How to test it?

This sample is inspired by [ndk-sample/hello-jni](https://github.com/android/ndk-samples/tree/main/hello-jni) so with some changes you can load
this library from that application. <br/>

## Considerations:

* The package of the calling class: **com.example.hellojni**
* The name of the calling class: *HelloJni*.
* The name of the native function in the calling class: **stringFromJNI**.
* In the above example the name of the JNI library: **libhello-jni.so**.
* On Android, **JNI_OnUnload** is never called.