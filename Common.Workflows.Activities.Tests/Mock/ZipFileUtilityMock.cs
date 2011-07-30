using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.IO.Interfaces;
namespace Common.Workflows.Activities.Tests.Mock
{
    public class ZipFileUtilityMock:IZipFileUtilities
    {
        public IEnumerable<System.IO.FileSystemInfo> ExtractAll(System.IO.FileInfo source, System.IO.DirectoryInfo targetDirectory)
        {
            return new List<FileSystemInfo> { new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Placeholder.txt") };
        }
    }
}
