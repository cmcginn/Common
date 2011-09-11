using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
namespace Common.Services.Payment.Interfaces
{
    public interface IAddressType
    {
        [System.ComponentModel.DataAnnotations.Key()]
        string Id { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string Company { get; set; }
        string Country { get; set; }
        string FaxNumber { get; set; }
        string PhoneNumber { get; set; }
        string PostalCode { get; set; }
        string StateProvince { get; set; }
    }
}
