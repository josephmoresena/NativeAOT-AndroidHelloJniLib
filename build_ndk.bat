dotnet publish -r linux-bionic-arm64 /p:DefineConstants=ANDROID -p:DisableUnsupportedError=true -p:PublishAotUsingRuntimePack=true -p:AssemblyName=libhello-jni -p:RemoveSections=false
