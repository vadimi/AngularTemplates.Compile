using System;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace AngularTemplates.Compile
{
    public class AngularTemplatesTask : Task
    {
        [Required]
        public ITaskItem[] SourceFiles { get; set; }

        [Required]
        public string OutputFile { get; set; }

        public string BaseUrl { get; set; }

        public string ModuleName { get; set; }

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
                Log.LogErrorFromException(ex);
                return false;
            }
        }

        private void Compile()
        {
            var options = new TemplateCompilerOptions {
                OutputPath = OutputFile,
                BaseUrl = BaseUrl,
                ModuleName = ModuleName
            };
            var compiler = new TemplateCompiler(options);
            compiler.Compile(SourceFiles.Select(s => s.ItemSpec).ToArray());
            Log.LogMessage("Compiled {0} templates to {1}", SourceFiles.Length, OutputFile);
        }
    }
}

