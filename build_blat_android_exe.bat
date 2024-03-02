set "OutPath=Build"
if not exist "%OutPath%" (
    echo Folder does not exist. Creating folder...
    mkdir "%OutPath%"
    echo Folder created successfully.
) 

bflat build  -r BFlatSupport/Rxmxnx.PInvoke.Extensions.dll -d ANDROID -Os  --separate-symbols --os linux --arch arm64 --libc bionic 
