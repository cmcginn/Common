using System;

namespace Common.Services.Payment.Interfaces
{
    
    public interface ICustomerData
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string Company { get; set; }
        string Country { get; set; }
        string CustomerId { get; set; }
        string EmailAddress { get; set; }
        string FaxNumber { get; set; }
        string FirstName { get; set; }
        string IPAddress { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string PostalCode { get; set; }
        string State { get; set; }
    }
}
