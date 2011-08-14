using System;
using System.Collections.Generic;
using System.Text;
using Common.Services.Payment.Interfaces;
using System.Runtime.Serialization;
namespace Common.Services.Payment
{
    [DataContract( Namespace = "http://Common.Services.Payments" )]
    [KnownType(typeof(TransactionData))]
    public class TransactionData : ITransactionData
    {
        private decimal _Amount = 0;
        private string _PreviousTransactionReferenceNumber = string.Empty;
        private string _MerchantDescription = string.Empty;
        private string _MerchantInvoiceNumber = string.Empty;
        private List<ITransactionMessage> _TransactionMessages = new List<ITransactionMessage>();

        [DataMember]
        public List<ITransactionMessage> TransactionMessages
        {
            get { return _TransactionMessages; }
            set { _TransactionMessages = value; }
        }
        [DataMember]
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        [DataMember]
        public string PreviousTransactionReferenceNumber
        {
            get { return _PreviousTransactionReferenceNumber; }
            set { _PreviousTransactionReferenceNumber = value; }
        }
        [DataMember]
        public string MerchantDescription
        {
            get { return _MerchantDescription; }
            set { _MerchantDescription = value; }
        }
        [DataMember]
        public string MerchantInvoiceNumber
        {
            get { return _MerchantInvoiceNumber; }
            set { _MerchantInvoiceNumber = value; }
        }






       
    }
}
