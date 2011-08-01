using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public interface IGatewayProfile
    {
        string ProfileId { get; set; }        
        IList<IGatewayPaymentProfile> PaymentProfiles { get; set; }
        ICustomerData ProfileCustomerData { get; set; }
    }
}
