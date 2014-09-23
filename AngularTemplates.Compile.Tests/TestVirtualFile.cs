using System.IO;
using System.Text;
using System.Web.Hosting;

namespace AngularTemplates.Compile.Tests
{
    internal class TestVirtualFile : VirtualFile
    {
        public TestVirtualFile(string virtualPath, string contents)
            : base(virtualPath)
        {
            Contents = contents;
        }

        public string Contents { get; set; }

        public override Stream Open()
        {
            return new MemoryStream(Encoding.Default.GetBytes(Contents));
        }
    }
}