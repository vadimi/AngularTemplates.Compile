using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using AngularTemplates.Compile;

namespace AngularTemplates.Bundling
{
    public class TemplateBundle : ScriptBundle
    {
        private readonly TemplateCompilerOptions _options;

        public TemplateBundle(string virtualPath, TemplateCompilerOptions options) : base(virtualPath)
        {
            _options = options;
        }

        public TemplateBundle(string virtualPath, string cdnPath, TemplateCompilerOptions options)
            : base(virtualPath, cdnPath)
        {
            _options = options;
        }

        public override BundleResponse GenerateBundleResponse(BundleContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if ((!_options.EnableOptimizations.HasValue && !BundleTable.EnableOptimizations) || (_options.EnableOptimizations.HasValue && !_options.EnableOptimizations.Value))
            {
                return new BundleResponse(string.Empty, new List<BundleFile>());
            }

            var bundleFiles = EnumerateFiles(context);
            var ignoredFiles = context.BundleCollection.IgnoreList.FilterIgnoredFiles(context, bundleFiles);
            var files = Orderer.OrderFiles(context, ignoredFiles).ToList();

            if (string.IsNullOrWhiteSpace(_options.WorkingDir))
            {
                _options.WorkingDir = "/";
            }

            var compiler = new TemplateCompiler(_options);
            var virtualFiles = files.Select(f => f.VirtualFile).ToArray();
            var result = compiler.Compile(virtualFiles);
            return ApplyTransforms(context, result, files);
        }
    }
}