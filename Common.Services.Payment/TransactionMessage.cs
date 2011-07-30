using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{
    public class TransactionMessage : ITransactionMessage
    {
     
        private string _Description = string.Empty;
        private string _Code = string.Empty;
        private TransactionMessageType _MessageType = TransactionMessageType.Unknown;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public TransactionMessageType MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }

        public TransactionMessage()
        {

        }

        public TransactionMessage(string description, string code, TransactionMessageType type)
        {
            _Description = description;
            _Code = code;
            _MessageType = type;
        }
    }
}
