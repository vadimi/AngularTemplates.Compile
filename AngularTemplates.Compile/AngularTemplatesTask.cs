using System;
using System.IO;
using System.Linq;
using System.Web.Optimization;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AngularTemplates.Compile
{
    public class AngularTemplatesTask : Task
    {
        private const string BundleName = "~/templates";

        [Required]
        public ITaskItem[] SourceFiles { get; set; }

        [Required]
        public string OutputFile { get; set; }

        public string Prefix { get; set; }

        public string ModuleName { get; set; }

        public string WorkingDir { get; set; }

        public bool Standalone { get; set; }

        public override bool Execute()
        {
            if (SourceFiles.Length == 0)
            {
                Log.LogError("SourceFiles cannot be empty");
                return false;
            }

            if (string.IsNullOrEmpty(OutputFile))
            {
                Log.LogError("OutputFile cannot be empty");
                return false;
            }

            try
            {
                Compile();
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                return false;
            }
        }

        private void Compile()
        {
            var response = Optimizer.BuildBundle(BundleName, new OptimizationSettings
            {
                ApplicationPath = Environment.CurrentDirectory,
                BundleSetupMethod = SetupBundles
            });

            File.WriteAllText(OutputFile, response.Content);

            Log.LogMessage("Compiled {0} templates to {1}", SourceFiles.Length, OutputFile);
        }

        private void SetupBundles(BundleCollection collection)
        {
            var options = new TemplateCompilerOptions
            {
                OutputPath = OutputFile,
                Prefix = Prefix,
                ModuleName = ModuleName,
                WorkingDir = WorkingDir,
                Standalone = Standalone
            };

            var bundle = new TemplateBundle(BundleName, options)
                .Include(SourceFiles.Select(s => NormalizeUrl(s.ItemSpec)).ToArray());

            // we don't need any transforms here
            bundle.Transforms.Clear();

            Log.LogMessage(string.Join(", ", SourceFiles.Select(s => s.ItemSpec).ToArray()));

            collection.Add(bundle);
        }

        private string NormalizeUrl(string url)
        {
            var prefix = url.StartsWith("~") ? string.Empty : "~/";
            return Path.Combine(prefix, url);
        }
    }
}