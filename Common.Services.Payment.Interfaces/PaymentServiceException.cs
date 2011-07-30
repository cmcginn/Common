using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public class PaymentServiceException : System.Exception
    {
        public PaymentServiceException(string message) : base(message) { }
    }
}
