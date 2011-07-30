using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common.IO.Interfaces;
using Ionic.Zip;
namespace Common.IO
{
    public class FileUtilities:IZipFileUtilities
    {
        public IEnumerable<FileSystemInfo> ExtractAll(FileInfo source, DirectoryInfo targetDirectory)
        {
            List<FileSystemInfo> result = new List<FileSystemInfo>();
            using (ZipFile zip = ZipFile.Read(source.FullName))
            {                
                zip.ExtractAll(targetDirectory.FullName);
                zip.Entries.ToList().ForEach(zipEntry =>
                    {
                        if(zipEntry.IsDirectory)
                            result.Add(new DirectoryInfo(zipEntry.FileName));
                        else
                            result.Add(new FileInfo(zipEntry.FileName));
                    });
                return result;
            }

        }
    }
}
