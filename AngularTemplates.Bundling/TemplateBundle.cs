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
        private readonly bool _bundleAlways;

        /// <summary>
        /// Initializes new instance of <code>TemplateBundle</code>
        /// </summary>
        /// <param name="virtualPath">The virtual path of the bundle</param>
        /// <param name="options">Template compiler options</param>
        public TemplateBundle(string virtualPath, TemplateCompilerOptions options) : this(virtualPath, options, false)
        {
        }

        /// <summary>
        /// Initializes new instance of <code>TemplateBundle</code>
        /// </summary>
        /// <param name="virtualPath">The virtual path of the bundle</param>
        /// <param name="options">Template compiler options</param>
        /// <param name="bundleAlways">If true BundleTable.EnableOptimizations flag is ignored</param>
        public TemplateBundle(string virtualPath, TemplateCompilerOptions options, bool bundleAlways) : base(virtualPath)
        {
            _options = options;
            _bundleAlways = bundleAlways;
        }

        /// <summary>
        /// Initializes new instance of <code>TemplateBundle</code>
        /// </summary>
        /// <param name="virtualPath">The virtual path of the bundle</param>
        /// <param name="cdnPath">The path of a Content Delivery Network</param>
        /// <param name="options">Template compiler options</param>
        public TemplateBundle(string virtualPath, string cdnPath, TemplateCompilerOptions options)
            : base(virtualPath, cdnPath)
        {
            _options = options;
        }

        public override BundleResponse GenerateBundleResponse(BundleContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (!BundleTable.EnableOptimizations && !_bundleAlways)
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