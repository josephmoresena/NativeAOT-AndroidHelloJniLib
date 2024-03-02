@echo off
echo clean all temp file (obj/** ,bin/**) 
set "OutPath=Build"
if not exist "%OutPath%" (
    echo Folder does not exist. Creating folder...
    mkdir "%OutPath%"
    echo Folder created successfully.
) 
bflat build -r BFlatSupport/Rxmxnx.PInvoke.Extensions.dll -d ANDROID --no-stacktrace-data --no-globalization --no-exception-messages -Os --no-pie --separate-symbols --os:linux --arch:arm64 --libc:bionic  --target Shared -o:./Build/libhello-jni.so
echo "exit".