using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services.Payment.Interfaces
{
    public interface IPaymentData
    {
        string Id { get; set; }
        IPaymentCardData CardData { get; set; }        
        ICustomerData Customer { get; set; }
        IPaymentGatewaySettings GatewaySettings { get; set; }
        ITransactionData Transaction { get; set; }
        ITransactionResultData TransactionResult { get; set; }
    }
}
