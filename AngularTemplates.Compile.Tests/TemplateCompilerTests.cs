using System.IO;
using System.Web.Hosting;
using Xunit;

namespace AngularTemplates.Compile.Tests
{
    public class TemplateCompilerTests
    {
        private readonly TestVirtualFile _template1;
        private readonly TestVirtualFile _template2;

        public TemplateCompilerTests()
        {
            _template1 = new TestVirtualFile("/fixtures/template1.html", "test");
            _template2 = new TestVirtualFile("/fixtures/template2.html", "test2");
        }

        [Fact]
        public void ShouldUseDefaultModuleName()
        {
            var options = new TemplateCompilerOptions
            {
                ModuleName = "",
                WorkingDir = "/fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new VirtualFile[] {_template1});
            Assert.Equal(File.ReadAllText("../../../expected/compiled1.js"), result);
        }

        [Fact]
        public void ShouldUseCustomModuleName()
        {
            var options = new TemplateCompilerOptions
            {
                ModuleName = "myapp",
                WorkingDir = "/fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new VirtualFile[] {_template1});
            Assert.Equal(File.ReadAllText("../../../expected/compiled2.js"), result);
        }

        [Fact]
        public void ShouldAddPrefixToTemplateName()
        {
            var options = new TemplateCompilerOptions
            {
                Prefix = "/templates",
                ModuleName = "myapp",
                WorkingDir = "/fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new VirtualFile[] {_template1});
            Assert.Equal(File.ReadAllText("../../../expected/compiled3.js"), result);
        }

        [Fact]
        public void ShouldCombineMultipleFiles()
        {
            var options = new TemplateCompilerOptions
            {
                Prefix = "/templates",
                ModuleName = "myapp",
                WorkingDir = "/fixtures"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new VirtualFile[]
            {
                _template1,
                _template2
            });
            Assert.Equal(File.ReadAllText("../../../expected/compiled4.js"), result);
        }

        [Fact]
        public void ShouldRenderStandaloneModule()
        {
            var options = new TemplateCompilerOptions
            {
                ModuleName = "",
                WorkingDir = "/fixtures",
                Standalone = true
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new VirtualFile[] {_template1});
            Assert.Equal(File.ReadAllText("../../../expected/compiled5.js"), result);
        }

        [Fact]
        public void ShouldLowercaseTemplateName()
        {
            var options = new TemplateCompilerOptions
            {
                ModuleName = "",
                WorkingDir = "/fixtures",
                LowercaseTemplateName = true
            };

            var template = new TestVirtualFile("/fixtures/TEMPLATE1.html", "test");
            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new VirtualFile[] {template});
            Assert.Equal(File.ReadAllText("../../../expected/compiled1.js"), result);
        }
    }
}