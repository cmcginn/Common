using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace Common.Services.Payment {
  [DataContract( Namespace = "http://Common.Services.Payments" )]
  [KnownType( typeof( AddressType ) )]
  public class AddressType : IAddressType {
    [DataMember]
    public string Id { get; set; }
    [DataMember]
    public string AddressLine1 { get; set; }
    [DataMember(IsRequired=false)]
    public string AddressLine2 { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public string Company { get; set; }
    [DataMember]
    public string Country { get; set; }
    [DataMember]
    public string FaxNumber { get; set; }
    [DataMember]
    public string PhoneNumber { get; set; }
    [DataMember]
    public string PostalCode { get; set; }
    [DataMember]
    public string StateProvince { get; set; }
  }
}
