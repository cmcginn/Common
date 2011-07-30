using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Common.Types;
using System.Diagnostics.Contracts;
namespace Common.Net.Interfaces
{
    [ContractClassFor(typeof(IHttpPost))]
    public abstract class IHttpPostContract:IHttpPost
    {

        public event EventHandler<EventArgs<Stream>> Recieved;
        public Encoding Encoding { get; set; }
        public event EventHandler<EventArgs<NetException>> Error;
        public PostEncodingTypes PostEncodingType { get; set; }
        public PostTypes PostType { get; set; }
        public Uri PostUri { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public NetworkCredential NetworkCredential { get; set; }
        public string PostData { get; set; }
        public abstract byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType, IDictionary<string, string> parameters,Encoding encoding);
        public byte[] Post()
        {
            Contract.Requires(PostUri != null, "PostUri must be supplied");
            return Contract.Result<Byte[]>();
        }

        public abstract byte[] Post(Uri postUri);       

        public abstract byte[] Post(Uri postUri, PostEncodingTypes postEncodingType);

        public abstract byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType);

        public abstract byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType, IDictionary<string, string> parameters);



    }
    [ContractClass(typeof(IHttpPostContract))]
    public interface IHttpPost
    {
        event EventHandler<EventArgs<Stream>> Recieved;
        event EventHandler<EventArgs<NetException>> Error;
        Encoding Encoding { get; set; }
        PostEncodingTypes PostEncodingType
        {
            get;
            set;
        }
        PostTypes PostType
        {
            get;
            set;
        }
        string PostData { get; set; }
        Uri PostUri
        {
            get;
            set;
        }
        IDictionary<string, string> Parameters
        {
            get;
            set;
        }
        System.Net.NetworkCredential NetworkCredential
        {
            get;
            set;
        }
        byte[] Post();
        byte[] Post(Uri postUri);
        byte[] Post(Uri postUri, PostEncodingTypes postEncodingType);
        byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType);
        byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType, IDictionary<string, string> parameters);
        byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType, IDictionary<string, string> parameters, Encoding encoding);

    }
}
