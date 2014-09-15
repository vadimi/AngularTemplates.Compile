using NUnit.Framework;
using System;
using AngularTemplates.Compile;
using System.IO;

namespace AngularTemplates.Compile.Tests
{
    [TestFixture()]
    public class TemplateCompilerTests
    {
        [Test()]
        public void TestEmptyModuleName()
        {
            var options = new TemplateCompilerOptions {
                ModuleName = ""
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] { "../../fixtures/template1.html" });
            Assert.AreEqual(result, File.ReadAllText("../../expected/compiled1.js"));
        }

        [Test()]
        public void TestCustomModuleName()
        {
            var options = new TemplateCompilerOptions {
                ModuleName = "myapp"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] { "../../fixtures/template1.html" });
            Assert.AreEqual(result, File.ReadAllText("../../expected/compiled2.js"));
        }

        [Test()]
        public void TestCustomBaseUrl()
        {
            var options = new TemplateCompilerOptions {
                BaseUrl = "/templates",
                ModuleName = "myapp"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] { "../../fixtures/template1.html" });
            Assert.AreEqual(result, File.ReadAllText("../../expected/compiled3.js"));
        }

        [Test()]
        public void TestMultipleFiles()
        {
            var options = new TemplateCompilerOptions {
                BaseUrl = "/templates",
                ModuleName = "myapp"
            };

            var compiler = new TemplateCompiler(options);
            var result = compiler.Compile(new [] {
                "../../fixtures/template1.html",
                "../../fixtures/template2.html"
            });
            Assert.AreEqual(result, File.ReadAllText("../../expected/compiled4.js"));
        }
    }
}

