# HelloJniLib

This repo is a sample for Android JNI libraries compilation with NativeAOT.
For now, we support two ways to compile this library:

1. Using BFlat tool.
2. Using .NET SDK 9.0

# BFlat

In order to optimize the build process we recommend the use of [BFlat tool](https://github.com/bflattened/bflat).
This tool works on both Windows and Linux, so WSL environment is not needed for android compilation.

### Prerequisites

This library depends on [Rxmxnx.PInvoke.Extensions](https://github.com/josephmoresena/Rxmxnx.PInvoke.Extensions)
and [Rxmxnx.JNetInterface](https://github.com/josephmoresena/Rxmxnx.JNetInterface), so you need download the latest
version of these packages:

* https://www.nuget.org/api/v2/package/Rxmxnx.PInvoke.Extensions
* https://www.nuget.org/packages/Rxmxnx.JNetInterface.Core
* https://www.nuget.org/packages/Rxmxnx.JNetInterface

Extract dlls file to BFlatSupport folder.

### Build

Use the following command to build library.

	    {PATH_TO_BFLAT}bflat build -r BFlatSupport/Rxmxnx.PInvoke.Extensions.dll -r BFlatSupport/Rxmxnx.JNetInterface.Core.dll -r BFlatSupport/Rxmxnx.JNetInterface.dll --no-stacktrace-data --no-globalization --no-exception-messages -Os --no-pie --separate-symbols --os:linux --arch:arm64 --libc:bionic -o:libhello-jni.so

# NET SDK 9.0

With new .NET 9.0 SDK we are now able to compile NativeAOT android binaries using linux-bionic RID.

### How to build it

This process was tested for android-arm64 (`linux-bionic-arm64`) and android-arm32 (`linux-bionic-arm`) compilation but
may
work for android-x64 (`linux-bionic-x64`) too. <br/>
The following commands assume:

* .NET project imports [BionicNativeAot.targets](BionicNativeAot.targets) file.
* **ANDROID_NDK_ROOT** environment variable: Full path to NDK. Used to preconfigure **CppCompilerAndLinker**, *
  *ObjCopyName** and
  **SysRoot**.
* Android NDK version is **r26b**.
* Target architecture is **arm64**.
* Host architecture is windows, linux or macOS x64.

      dotnet publish -r linux-bionic-arm64 -p:DisableUnsupportedError=true -p:PublishAotUsingRuntimePack=true -p:AssemblyName=libhello-jni

#### Environment Parameters

* **ObjCopyName**: Path of NDK **llvm-objcopy**. This is needed in order to use StripSymbols MSBuild parameter.

#### MSBuild Parameters

* **CppCompilerAndLinker**: Linker. On Windows, NDK executables must be encapsulated in **cmd** files in order to be used.
  On Linux and macOS, the executables are used directly.
* **SysRoot**: Sysroot path from NDK. Needed for NDK compilation.
* **RemoveSections**: Removes **__init** and **__fini** symbols from .exports file. When it detects that the NDK version
  is greater than r26 it is automatically set to true.
* **AssemblyName**: In order to produce a .so file with given name.

## Considerations

* You can also build this in reflection-free mode, it will significantly reduce the size of the binary.
* This branch uses the **JNetInterface** public API, so it doesn't contain any direct JNI calls.
* In order to reduce the size of the resulting binary, all available JNetInterface **switch features** were activated.
* Enabling the **JNetInterface.EnableTrace** feature switch can give you more information about the JNI calls involved
  when interacting with the **JNetInterface APIs**.
* If you want to use JNI directly, without the overhead of **JNetInterface**, check out
  the [main](https://github.com/josephmoresena/NativeAOT-AndroidHelloJniLib/tree/main) branch.

# How to test it?

This sample is inspired by [ndk-sample/hello-jni](https://github.com/android/ndk-samples/tree/main/hello-jni) so with
some changes you can load
this library from that application. <br/>

## Considerations:

* The package of the calling class: **com.example.hellojni**
* The name of the calling class: *HelloJni*.
* The name of the native function in the calling class: **stringFromJNI**.
* In the above example the name of the JNI library: **libhello-jni.so**.
* On Android, **JNI_OnUnload** is never called.