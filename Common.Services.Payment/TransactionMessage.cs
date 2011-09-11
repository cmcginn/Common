using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Runtime.Serialization;

namespace Common.Services.Payment
{
    [DataContract( Namespace = "http://Common.Services.Payments" )]
    [KnownType(typeof(TransactionMessage))]
    public class TransactionMessage : ITransactionMessage
    {

        private string _Description = string.Empty;
        private string _Code = string.Empty;
        private string _Message = String.Empty;
        private Exception _TransactionResultException;
        
        public Exception TransactionResultException
        {
            get { return _TransactionResultException; }
            set { _TransactionResultException = value; }
        }
        [DataMember]
        public string TransactionMessageResult
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private TransactionMessageType _MessageType = TransactionMessageType.Unknown;
        [DataMember]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [DataMember]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        [DataMember]
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
