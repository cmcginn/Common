using System;
namespace Common.Services.Payment.Interfaces
{
    public interface ITransactionMessage
    {
        string Code { get; set; }
        string Description { get; set; }
        TransactionMessageType MessageType { get; set; }
    }
}
