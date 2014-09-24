using System.IO;
using System.Web.Optimization;
using Xunit;

namespace AngularTemplates.Compile.Tests
{
    public class TemplateBundleTests
    {
        [Fact]
        public void ShouldBuildTemplateBundle()
        {
            var response = Optimizer.BuildBundle("~/templates", new OptimizationSettings
            {
                ApplicationPath = Path.GetFullPath("../../../fixtures"),
                BundleSetupMethod = SetupBundles
            });

            Assert.Equal(File.ReadAllText("../../../expected/compiled3.js"), response.Content);
        }

        private void SetupBundles(BundleCollection collection)
        {
            var options = new TemplateCompilerOptions
            {
                ModuleName = "myapp",
                Prefix = "/templates",
                WorkingDir = "../../../fixtures"
            };

            var bundle =
                new TemplateBundle("~/templates", options).Include("~/template1.html");
            bundle.Transforms.Clear();
            collection.Add(bundle);
        }
    }
}
