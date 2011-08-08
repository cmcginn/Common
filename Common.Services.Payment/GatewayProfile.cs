using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{
    public class GatewayProfile:IGatewayProfile
    {
        public string Id { get; set; }
        IList<IGatewayPaymentProfile> _PaymentProfiles;
        public IList<IGatewayPaymentProfile> PaymentProfiles
        {
            get { return _PaymentProfiles; }
            set { _PaymentProfiles = value; }
        }
        public ICustomerData ProfileCustomerData { get; set; }
     
    }
}
