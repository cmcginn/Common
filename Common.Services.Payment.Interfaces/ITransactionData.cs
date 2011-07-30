using System;
namespace Common.Services.Payment.Interfaces
{
    
    public interface ITransactionData
    {
        decimal Amount { get; set; }
        string MerchantDescription { get; set; }
        string MerchantInvoiceNumber { get; set; }
        string PreviousTransactionReferenceNumber { get; set; }
    }
}
