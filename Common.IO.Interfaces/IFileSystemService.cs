using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
namespace Common.IO.Interfaces
{
    [ContractClassFor(typeof(IFileSystemService))]
    public abstract class IFileSystemServiceContract : IFileSystemService
    {
        public FileInfo LoadFile()
        {
            Contract.Requires(SourceUri != null, "SourceUri must be specified");
            return Contract.Result<FileInfo>();
        }

        public FileInfo LoadFile(Uri sourceUri)
        {
            Contract.Ensures(SourceUri != null, "Cannot leave method without assigning SourceUri");
            return Contract.Result<FileInfo>();
        }

        public Uri SourceUri { get; set; }
       
    }
    [ContractClass(typeof(IFileSystemServiceContract))]
    public interface IFileSystemService
    {
        FileInfo LoadFile();
        FileInfo LoadFile(Uri sourceUri);
        Uri SourceUri { get; set; }
    }
}
