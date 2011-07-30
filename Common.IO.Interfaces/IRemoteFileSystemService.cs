using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using Common.Net.Interfaces;
namespace Common.IO.Interfaces
{
     [ContractClassFor(typeof(IRemoteFileSystemService))]
    public abstract class IRemoteFileSystemServiceContract :IRemoteFileSystemService
    {
         public FileInfo LoadFile()
         {
             Contract.Requires(SourceUri != null, "SourceUri must be specified");
             Contract.Requires(DestinationUri != null, "DestinationUri must be specified");
             return Contract.Result<FileInfo>();
         }
        public FileInfo LoadFile(Uri sourceUri)
        {
            Contract.Requires(DestinationUri != null, "DestinationUri must be specified");
            return Contract.Result<FileInfo>();
        }

        public abstract FileInfo LoadFile(Uri sourceUri, Uri destinationUri);
        public Uri SourceUri { get; set; }
        public Uri DestinationUri { get; set; }
        public IHttpPost HttpPost { get; set; }
    }
    [ContractClass(typeof(IRemoteFileSystemServiceContract))]
	public interface IRemoteFileSystemService:IFileSystemService
	{        
        FileInfo LoadFile(Uri sourceUri,Uri destinationUri);
        IHttpPost HttpPost { get; set; }
        Uri DestinationUri{get;set;}
	}
}
