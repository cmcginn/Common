using System;

namespace Common.Services.Payment.Interfaces
{
    
    public interface ICustomerData
    {
        IAddressType Address { get; set; }        
        string MerchantId { get; set; }        
        string CustomerId { get; set; }
        string EmailAddress { get; set; }        
        string FirstName { get; set; }
        string IPAddress { get; set; }
        string LastName { get; set; }        
        string CustomerDescription { get; set; }
    }
}
