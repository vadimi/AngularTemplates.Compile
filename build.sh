#!/bin/bash

xbuild AngularTemplates.Compile/AngularTemplates.Compile.csproj /p:Configuration=Release /t:Clean
xbuild AngularTemplates.Compile/AngularTemplates.Compile.csproj /p:Configuration=Release /t:Build
nuget Pack AngularTemplates.Compile.nuspec