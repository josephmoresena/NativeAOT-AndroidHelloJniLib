dotnet publish -r 
 -p:DisableUnsupportedError=true -p:PublishAotUsingRuntimePack=true -p:AssemblyName=libhello-jni -
p:RemoveSections=false
