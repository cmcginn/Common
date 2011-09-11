using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.IO.Interfaces;
using Common.Net.Interfaces;
using Common.Types;
using System.Diagnostics.Contracts;
using Microsoft.Practices.Unity;
namespace Common.IO
{
    public class HttpFileSystemService : IRemoteFileSystemService
    {
        [Dependency]
        public IHttpPost HttpPost { get; set; }
        public Uri SourceUri { get; set; }
        public Uri DestinationUri { get; set; }
        public FileInfo LoadFile()
        {
            Contract.Ensures(Contract.Result<FileInfo>().Exists, "Response could not be written to File, The File does not exist");
            FileInfo result = null;
            HttpPost.Error += delegate(object sender, EventArgs<NetException> e)
            {
                throw (e.Value);
            };
            HttpPost.Recieved += delegate(object sender, EventArgs<Stream> e)
            {
                using (FileStream fs = new FileStream(DestinationUri.LocalPath, FileMode.Create, FileAccess.Write))
                {
                    e.Value.CopyTo(fs);
                    fs.Close();
                }
                result = new FileInfo(DestinationUri.LocalPath);
            };
            byte[] source = HttpPost.Post(SourceUri, PostEncodingTypes.UrlEncoded, PostTypes.Get);
            return result;
        }
        public FileInfo LoadFile(Uri sourceUri)
        {
            SourceUri = sourceUri;
            return LoadFile();
        }
        public FileInfo LoadFile(Uri sourceUri, Uri destinationUri)
        {
            DestinationUri = destinationUri;
            return LoadFile(sourceUri);
        }


    }
}
