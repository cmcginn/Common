using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
namespace Common.IO.Interfaces
{
    [ContractClassFor(typeof(IZipFileUtilities))]
    public abstract class IZipFileUtilitiesContract:IZipFileUtilities
    {
        public IEnumerable<FileSystemInfo> ExtractAll(FileInfo source, DirectoryInfo targetDirectory)
        {
            Contract.Requires(source.Exists, "Source file does not exist.");
            Contract.Requires(targetDirectory.Exists, "Target directory does not exist.");
            return Contract.Result<IEnumerable<FileSystemInfo>>();
        }
    }
    [ContractClass(typeof(IZipFileUtilitiesContract))]
    public interface IZipFileUtilities
    {
        IEnumerable<FileSystemInfo> ExtractAll(FileInfo source,DirectoryInfo targetDirectory);
    }
}
