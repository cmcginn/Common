using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public interface IGatewayPaymentProfile
    {
        ICustomerData CustomerData { get; set; }
        IPaymentCardData PaymentCardData { get; set; }
        string Id { get; set; }
    }
}
