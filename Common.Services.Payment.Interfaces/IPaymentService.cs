using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace Common.Services.Payment.Interfaces
{
   
    public interface IPaymentService
    {
        IPaymentData PaymentData { get; set; }
    }
}
