using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public interface IPaymentProfile:ICustomerData
    {
        string PaymentProfileId { get; set; }
    }
}
