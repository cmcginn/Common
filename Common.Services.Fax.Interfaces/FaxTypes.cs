using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using Common.Types;
namespace Common.Services.Fax.Interfaces
{
 
    public class FaxJob
    {
        [XmlAttribute]
        public string TraceId { get; set; }
        public ContactPoint ContactPoint { get; set; }
        public CreativeWork CreativeWork { get; set; }       
        public ResponseStatusType ResponseStatusType { get; set; }
        //public FaxRequest FaxRequest { get; set; }
        //public FaxResponse FaxResponse { get; set; }
    }

    public class FaxRequest
    {
        public ContactPoint ContactPoint { get; set; }
        public CreativeWork CreativeWork { get; set; }	
    }
    //TODO:Make ennumeration  type
    public class FaxResponse
    {
        public ResponseStatusType ResponseStatus { get; set; }
    }
}
