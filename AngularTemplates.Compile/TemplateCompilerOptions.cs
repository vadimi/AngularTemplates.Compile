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
        /// Gets or sets the template name prefix
        /// </summary>
        /// <value>The base URL.</value>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the name of the angular module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; set; }

        /// <summary>
        /// If Standalone is true, new angular module will be created, otherwise existing module will be used
        /// </summary>
        public bool Standalone { get; set; }

        /// <summary>
        /// Defines current working directory to build relative path for template name. If it is empty Environment.CurrentDirectory will be used instead. 
        /// </summary>
        public string WorkingDir { get; set; }
    }
}

