using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment.Tests.Mocks
{
    public class PaymentServiceMock:IPaymentService
    {
        public IPaymentData PaymentData { get; set; }
  
    }
}
