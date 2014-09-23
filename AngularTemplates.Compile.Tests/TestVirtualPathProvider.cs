using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Hosting;

namespace AngularTemplates.Compile.Tests
{
    public class TestVirtualPathProvider : VirtualPathProvider
    {
        private string NormalizeVirtualPath(string virtualPath, bool isDirectory = false)
        {
            if (!virtualPath.StartsWith("~"))
            {
                virtualPath = "~" + virtualPath;
            }
            virtualPath = virtualPath.Replace('\\', '/');
            // Normalize directories to always have an ending "/"
            if (isDirectory && !virtualPath.EndsWith("/"))
            {
                return virtualPath + "/";
            }
            return virtualPath;
        }

        // Files on disk (virtualPath -> file)
        private readonly Dictionary<string, VirtualFile> _fileMap = new Dictionary<string, VirtualFile>();

        private Dictionary<string, VirtualFile> FileMap
        {
            get { return _fileMap; }
        }

        public void AddFile(VirtualFile file)
        {
            FileMap[NormalizeVirtualPath(file.VirtualPath)] = file;
        }

        private readonly Dictionary<string, VirtualDirectory> _directoryMap = new Dictionary<string, VirtualDirectory>();

        private Dictionary<string, VirtualDirectory> DirectoryMap
        {
            get { return _directoryMap; }
        }

        public void AddDirectory(VirtualDirectory dir)
        {
            DirectoryMap[NormalizeVirtualPath(dir.VirtualPath, isDirectory: true)] = dir;
        }

        public override bool FileExists(string virtualPath)
        {
            return FileMap.ContainsKey(NormalizeVirtualPath(virtualPath));
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return DirectoryMap.ContainsKey(NormalizeVirtualPath(virtualDir, true));
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return FileMap[NormalizeVirtualPath(virtualPath)];
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            return DirectoryMap[NormalizeVirtualPath(virtualDir, true)];
        }

        internal class TestVirtualDirectory : VirtualDirectory
        {
            public TestVirtualDirectory(string virtualPath)
                : base(virtualPath)
            {
            }

            private readonly List<VirtualFile> _directoryFiles = new List<VirtualFile>();

            public List<VirtualFile> DirectoryFiles
            {
                get { return _directoryFiles; }
            }

            private readonly List<VirtualDirectory> _subDirs = new List<VirtualDirectory>();

            public List<VirtualDirectory> SubDirectories
            {
                get { return _subDirs; }
            }

            public override IEnumerable Files
            {
                get { return DirectoryFiles; }
            }

            public override IEnumerable Children
            {
                get { throw new NotImplementedException(); }
            }

            public override IEnumerable Directories
            {
                get { return SubDirectories; }
            }
        }
    }
}