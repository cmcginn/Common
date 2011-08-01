using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{
    public class GatewayPaymentProfile:IGatewayPaymentProfile
    {        
        public IPaymentCardData PaymentCardData { get; set; }
        public ICustomerData CustomerData { get; set; }
        public string PaymentProfileId { get; set; }
        public string MerchantId { get; set; }       
        public string CustomerDescription { get; set; }
    }
}
