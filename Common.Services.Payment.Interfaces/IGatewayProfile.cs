using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Common.Services.Payment.Interfaces
{
    public interface IGatewayProfile
    {     
        string Id { get; set; }        
        IList<IGatewayPaymentProfile> PaymentProfiles { get; set; }
        ICustomerData ProfileCustomerData { get; set; }
    }
}
