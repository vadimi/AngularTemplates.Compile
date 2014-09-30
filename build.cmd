msbuild AngularTemplates.Compile\AngularTemplates.Compile.csproj /p:Configuration=Release /t:Clean,Build /p:Platform="Any CPU" /p:OutputPath=bin/Release
msbuild AngularTemplates.Bundling\AngularTemplates.Bundling.csproj /p:SolutionDir= /p:Configuration=Release /t:Clean,Build /p:Platform="Any CPU" /p:OutputPath=bin/Release
nuget Pack AngularTemplates.Compile.nuspec