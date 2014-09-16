using System.IO;
using Xunit;

namespace AngularTemplates.Compile.Tests
{
    public class TemplateCompilerTests
    {
        [Fact]
        public void ShouldUseDefaultModuleName()
        {
            var options = new TemplateCompilerOptions {
                ModuleName = "",
                WorkingDir = "../../../fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] { "../../../fixtures/template1.html" });
            Assert.Equal(result, File.ReadAllText("../../../expected/compiled1.js"));
        }

        [Fact]
        public void ShouldUseCustomModuleName()
        {
            var options = new TemplateCompilerOptions {
                ModuleName = "myapp",
                WorkingDir = "../../../fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] { "../../../fixtures/template1.html" });
            Assert.Equal(result, File.ReadAllText("../../../expected/compiled2.js"));
        }

        [Fact]
        public void ShouldAddPrefixToTemplateName()
        {
            var options = new TemplateCompilerOptions {
                Prefix = "/templates",
                ModuleName = "myapp",
                WorkingDir = "../../../fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] { "../../../fixtures/template1.html" });
            Assert.Equal(result, File.ReadAllText("../../../expected/compiled3.js"));
        }

        [Fact]
        public void ShouldCombineMultipleFiles()
        {
            var options = new TemplateCompilerOptions {
                Prefix = "/templates",
                ModuleName = "myapp",
                WorkingDir = "../../../fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] {
                "../../../fixtures/template1.html",
                "../../../fixtures/template2.html"
            });
            Assert.Equal(result, File.ReadAllText("../../../expected/compiled4.js"));
        }
    }
}

