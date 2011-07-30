using System;
using System.Collections.Generic;
namespace Common.Services.Payment.Interfaces
{
    public interface ITransactionResultData
    {
        string AVSCode { get; set; }
        string AVSCodeDescription { get; set; }
        string CVVCode { get; set; }
        string CVVCodeDescription { get; set; }
        List<ITransactionMessage> Messages { get; set; }
        string ResponseCode { get; set; }
        string ResponseCodeDescription { get; set; }
        bool Suceeded { get; set; }
        string TransactionReferenceNumber { get; set; }
        string TransactionReferenceNumber2 { get; set; }
    }
}
