using System;
namespace Common.Services.Payment.Interfaces
{
    public interface ITransactionMessage
    {
        TransactionMessageType MessageType { get; set; }
        string TransactionMessageResult { get; set; }
        Exception TransactionResultException { get; set; }
    }
}
