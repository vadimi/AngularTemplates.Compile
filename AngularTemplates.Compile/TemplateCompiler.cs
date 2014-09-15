using System;
using System.IO;
using System.Web;
using System.Text;

namespace AngularTemplates.Compile
{
    public class TemplateCompiler
    {
        private TemplateCompilerOptions options;
        private readonly string baseUrl;
        private readonly string moduleName;

        public TemplateCompiler(TemplateCompilerOptions options)
        {
            this.options = options;
            baseUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                baseUrl = options.BaseUrl;
                if (!baseUrl.EndsWith("/"))
                {
                    baseUrl += "/";
                }
            }

            moduleName = string.IsNullOrWhiteSpace(options.ModuleName) ? "app" : options.ModuleName;
        }

        public string Compile(string[] templateFiles)
        {
            var sb = new StringBuilder();
            using (var stream = new StringWriter(sb))
            {
                Compile(stream, templateFiles);
            }

            return sb.ToString();
        }

        public void CompileToFile(string[] templateFiles)
        {
            CheckOutputDir();

            using (var stream = new StreamWriter(options.OutputPath))
            {
                Compile(stream, templateFiles);
            }
        }

        private void Compile(TextWriter writer, string[] templateFiles)
        {
            if (templateFiles == null || templateFiles.Length == 0)
            {
                throw new ArgumentException("templateFiles cannot be null or empty.");
            }

            writer.Write("angular.module('");
            writer.Write(moduleName);
            writer.WriteLine("', []).run(['$templateCache', function ($templateCache) {");
            foreach (var file in templateFiles)
            {
                var templateName = GetTemplateName(file);
                var template = File.ReadAllText(file);
                WriteToStream(writer, templateName, template);
            }
            writer.Write("}]);");
        }

        void WriteToStream(TextWriter writer, string templateName, string template)
        {
            writer.Write("$templateCache.put('");
            writer.Write(templateName);
            writer.Write("', ");
            writer.Write(CompileTemplate(template));
            writer.WriteLine(");");
        }

        private string CompileTemplate(string template)
        {
            return HttpUtility.JavaScriptStringEncode(template, true);
        }

        private string GetTemplateName(string file)
        {
            return baseUrl + Path.GetFileName(file);
        }

        private void CheckOutputDir()
        {
            var outputDir = Path.GetDirectoryName(options.OutputPath);
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
    }
}