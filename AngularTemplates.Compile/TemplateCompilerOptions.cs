using System;

namespace AngularTemplates.Compile
{
    public class TemplateCompilerOptions
    {
        /// <summary>
        /// Gets or sets the output path of compiled templates.
        /// </summary>
        /// <value>The output path.</value>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the base URL in angular template name
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the angular module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }
    }
}

