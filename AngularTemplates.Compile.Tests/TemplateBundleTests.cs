using System;
using System.IO;
using System.Web.Optimization;
using AngularTemplates.Bundling;
using Xunit;

namespace AngularTemplates.Compile.Tests
{
    public class TemplateBundleTests
    {
        [Fact]
        public void ShouldBuildTemplateBundle()
        {
            BundleTable.EnableOptimizations = true;

            var response = Optimizer.BuildBundle("~/templates", new OptimizationSettings
            {
                ApplicationPath = Path.GetFullPath("../../../fixtures"),
                BundleSetupMethod = SetupBundles(alwaysBundle: false)
            });

            Assert.Equal(File.ReadAllText("../../../expected/compiled3.js"), response.Content);
        }

        [Fact]
        public void ShouldIgnoreEnableOptimizationsFlag()
        {
            BundleTable.EnableOptimizations = false;

            var response = Optimizer.BuildBundle("~/templates", new OptimizationSettings
            {
                ApplicationPath = Path.GetFullPath("../../../fixtures"),
                BundleSetupMethod = SetupBundles(alwaysBundle: true)
            });

            Assert.Equal(File.ReadAllText("../../../expected/compiled3.js"), response.Content);
        }

        private Action<BundleCollection> SetupBundles(bool alwaysBundle)
        {
            return collection =>
            {
                var options = new TemplateCompilerOptions
                {
                    ModuleName = "myapp",
                    Prefix = "/templates",
                    WorkingDir = "../../../fixtures"
                };

                var bundle =
                    new TemplateBundle("~/templates", options, alwaysBundle).Include("~/template1.html");
                bundle.Transforms.Clear();
                collection.Add(bundle);
            };
        }
    }
}
