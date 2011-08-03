using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public abstract class BaseProfileCreditCardGateway:BaseCreditCardGateway,IProfileCreditCardGateway
    {
        public abstract IGatewayProfile GetOrCreateCustomerProfile(ICustomerData customerData);        
        public override bool SupportsProfile
        {
            get { return true; }
        }
        public abstract IGatewayProfile GetCustomerProfile(string id);
        
    }
}
