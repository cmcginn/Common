using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Types;
using Common.Types.Interfaces;
using System.Xml.Serialization;
namespace Common.Services.Fax.Interfaces
{
    public class FaxMessage:IMessage<IFaxMessage>
    {
        [XmlAttribute]
        public string TraceId { get; set; }     
        public IFaxMessage Payload { get; set; }
    }
}
