msbuild AngularTemplates.Compile\AngularTemplates.Compile.csproj /p:Configuration=Release /t:Clean,Build
nuget Pack AngularTemplates.Compile.nuspec