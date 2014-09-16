using System;
using System.IO;
using System.Web;
using System.Text;

namespace AngularTemplates.Compile
{
    public class TemplateCompiler
    {
        private readonly TemplateCompilerOptions _options;
        private readonly string _baseUrl;
        private readonly string _moduleName;
        private readonly string _workingDir;
        private const string DefaultModuleName = "app";

        public TemplateCompiler(TemplateCompilerOptions options)
        {
            _options = options;
            _baseUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(options.Prefix))
            {
                _baseUrl = options.Prefix;
                if (!_baseUrl.EndsWith("/"))
                {
                    _baseUrl += "/";
                }
            }

            _moduleName = string.IsNullOrWhiteSpace(options.ModuleName) ? DefaultModuleName : options.ModuleName;
            _workingDir = string.IsNullOrWhiteSpace(options.WorkingDir)
                ? Environment.CurrentDirectory
                : Path.GetFullPath(options.WorkingDir);
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

            using (var stream = new StreamWriter(_options.OutputPath))
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
            writer.Write(_moduleName);
            writer.Write("'");
            if (_options.Standalone)
            {
                writer.Write(", []");
            }
            writer.WriteLine(").run(['$templateCache', function ($templateCache) {");
            foreach (var file in templateFiles)
            {
                var templateName = GetTemplateName(file);
                var template = File.ReadAllText(file);
                WriteToStream(writer, templateName, template);
            }
            writer.Write("}]);");
        }

        private void WriteToStream(TextWriter writer, string templateName, string template)
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
            var name = _baseUrl +
                       GetRelativePath(Path.GetFullPath(file), _workingDir);
            return name.ToLower();
        }

        private string GetRelativePath(string filespec, string folder)
        {
            var pathUri = new Uri(filespec);

            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            var folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString());
        }

        private void CheckOutputDir()
        {
            var outputDir = Path.GetDirectoryName(_options.OutputPath);
            if (outputDir != null && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
    }
}