using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Common.Net.Interfaces;
using Common.Types;
namespace Common.Net
{
    public class HttpPost : IHttpPost
    {
        public event EventHandler<EventArgs<Stream>> Recieved;
        public event EventHandler<EventArgs<NetException>> Error;
        public string PostData { get; set; }
        protected void OnRecieved(Stream responseStream)
        {
            if (null != Recieved)
                Recieved(this,new EventArgs<Stream>(responseStream));
        }
        protected void OnError(NetException exception)
        {
            if (null != Error)
                Error(this, new EventArgs<NetException>(exception));
        }

        private PostEncodingTypes _PostEncodingType = PostEncodingTypes.UrlEncoded;
        public PostEncodingTypes PostEncodingType
        {
            get
            {
                return _PostEncodingType;
            }
            set
            {
                _PostEncodingType = value;
            }
        }
        private PostTypes _PostType = PostTypes.Get;
        public PostTypes PostType
        {
            get
            {
                return _PostType;
            }
            set
            {
                _PostType = value;
            }
        }
        public Uri PostUri { get; set; }
        IDictionary<string, string> _Parameters;
        public IDictionary<string, string> Parameters
        {
            get
            {
                if (_Parameters == null)
                    _Parameters = new Dictionary<string, string>();
                return _Parameters;
            }
            set { _Parameters = value; }
        }
        private Encoding _Encoding;
        public Encoding Encoding
        {
            get
            {
                if (_Encoding == null)
                    _Encoding = new UTF8Encoding();
                return _Encoding;
            }
            set
            {
                _Encoding = value;
            }
        }
        public NetworkCredential NetworkCredential { get; set; }
       
        private string EncodeParameters()
        {
            StringBuilder builder = new StringBuilder();
            if( Parameters.Count > 0 ) {
              var start = String.Format( "?{0}={1}", Parameters.First().Key, Parameters.First().Value );
              builder.Append( start );
              if( Parameters.Count > 1 ) {
                Parameters.Skip( 1 ).Select( n => String.Format( "&{0}={1}", n.Key, System.Web.HttpUtility.UrlEncode( n.Value ) ) ).ToList().ForEach( n => builder.Append( n ) );
              }
            } else if( !String.IsNullOrEmpty( PostData ) )
              builder.Append( PostData );
            return builder.ToString();
        }

        public byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType, IDictionary<string, string> parameters, Encoding encoding)
        {
            Encoding = encoding;
            return Post(postUri, postEncodingType, postType, parameters);
        }
        

        public byte[] Post()
        {
            byte[] result = null;
            try
            {

                string requestContentType = String.Empty;
                switch (PostEncodingType)
                {
                    case PostEncodingTypes.UrlEncoded:
                        requestContentType = "application/x-www-form-urlencoded";
                        break;
                  case PostEncodingTypes.Xml:
                        requestContentType = "text/xml";
                        break;
                    default:
                        requestContentType = "application/x-www-form-urlencoded";
                        break;
                }
                string encodedParameters = EncodeParameters();
                HttpWebRequest request = null;


                request = (HttpWebRequest)WebRequest.Create(PostUri);
                if (NetworkCredential != null)
                    request.Credentials = NetworkCredential;
                if (PostType == PostTypes.Post)
                {                    
                    request.Method = "POST";
                    request.ContentType = requestContentType;
                    request.ContentLength = encodedParameters.Length;
                    using (Stream writeStream = request.GetRequestStream())
                    {
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] bytes = encoding.GetBytes(PostData);
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    var uri = PostUri.ToString() + encodedParameters;
                    request = (HttpWebRequest)WebRequest.Create(uri);
                    if (NetworkCredential != null)
                        request.Credentials = NetworkCredential;
                    request.Method = "GET";
                }            
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        OnRecieved(responseStream);
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding))
                        {
                            result = Encoding.GetBytes(readStream.ReadToEnd());
                        }
                        
                    }
                }              
                
            }
            catch (System.Net.WebException wex)
            {
                var netException = new NetException("HttpPost Failed", wex);
                OnError(netException);
            }
            catch (System.Exception ex)
            {
                var netException = new NetException("HttpPost Failed", ex);
                OnError(netException);
            }
            return result;
        }

        public byte[] Post(Uri postUri)
        {
            PostUri = postUri;
            return Post();
        }

        public byte[] Post(Uri postUri, PostEncodingTypes postEncodingType)
        {
            PostEncodingType = postEncodingType;
            return Post(postUri);
        }

        public byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType)
        {
            PostType = postType;
            return Post(postUri, postEncodingType);
        }

        public byte[] Post(Uri postUri, PostEncodingTypes postEncodingType, PostTypes postType, IDictionary<string, string> parameters)
        {
            Parameters = parameters;
            return Post(postUri, postEncodingType, postType);
        }
    }
}
