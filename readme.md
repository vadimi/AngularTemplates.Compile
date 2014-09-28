AngularTemplates.Compile
========================

ASP.NET MVC bundle and MsBuild Task to combine multiple Angular JS templates into a single javascript file. [$templateCache](https://docs.angularjs.org/api/ng/service/$templateCache)  angular service is used to cache templates.

Supported platforms:

- .NET 4.0+
- Mono 3.8+

## Options
`Prefix` - String to prefix template urls with.

`ModuleName` - AngularJS module name. If not specified defaults to `app`.

`Standalone` - Boolean indicated if the templates are part of an existing module or a standalone. Defaults to `false`. If the value is `false`, the module will look like `angular.module('app')`, otherwise `angular.module('app', [])`.

`WorkingDir` - Working directory to locate templates in.

`LowercaseTemplateName` - Boolean that indicates if template names (urls) need to be lowercased or not. Defaults to `false`.

`OutputPath` - Output path of the compiled JS.

## Bundling example

Specify in BundleConfig.cs the following bundle with options:

```csharp
var options = new TemplateCompilerOptions
	{
	    ModuleName = "myapp",
	    Prefix = "/",
	    Standalone = true
	};

var bundle = new TemplateBundle("~/templates", options)
	.Include("~/templates/template1.html", "~/templates/template2.html");
bundles.Add(bundle);
```

In your view render this bundle:

```csharp
@Scripts.Render("~/templates")
```

## MsBuild example

```xml
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/MsBuild/2003" DefaultTargets="BuildBundles">
  <UsingTask TaskName="AngularTemplates.Compile.AngularTemplatesTask"
  	AssemblyFile="..\packages\AngularTemplates.Compile.1.0.0\lib\net40\AngularTemplates.Compile.dll" />


  <Target Name="BuildBundles">

    <ItemGroup>

      <Templates Include="$(ProjectDir)Scripts\templates\*.html;"/>

    </ItemGroup>

    <AngularTemplatesTask SourceFiles="@(Templates)"
         OutputFile="$(ProjectDir)Scripts\templates\compiled.js"
         Prefix="/"
         ModuleName="myapp"
         WorkingDir="$(ProjectDir)"
         LowercaseTemplateName="True" />

  </Target>

</Project>
```
