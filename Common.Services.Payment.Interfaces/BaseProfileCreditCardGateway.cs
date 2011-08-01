using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public abstract class BaseProfileCreditCardGateway:BaseCreditCardGateway,IProfileCreditCardGateway
    {
        public abstract IGatewayProfile GetOrCreateProfile(ICustomerData customerData);
        public override bool SupportsProfile
        {
            get { return true; }
        }
    }
}
