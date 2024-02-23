echo clean all temp file (obj/** ,bin/**) 

bflat build -r BFlatSupport/Rxmxnx.PInvoke.Extensions.dll --no-stacktrace-data --no-globalization --no-exception-messages -Os --no-pie --separate-symbols --os:linux --arch:arm64 --libc:bionic  --target Shared -o:libhello-jni.so
