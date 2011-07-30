using System;
using System.Collections.Generic;
using System.Text;
namespace Common.Services.Payment.Interfaces
{

    public class TransactionData : ITransactionData
    {
        private decimal _Amount = 0;
        private string _PreviousTransactionReferenceNumber = string.Empty;
        private string _MerchantDescription = string.Empty;
        private string _MerchantInvoiceNumber = string.Empty;
        

        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string PreviousTransactionReferenceNumber
        {
            get { return _PreviousTransactionReferenceNumber; }
            set { _PreviousTransactionReferenceNumber = value; }
        }
        public string MerchantDescription
        {
            get { return _MerchantDescription; }
            set { _MerchantDescription = value; }
        }
        public string MerchantInvoiceNumber
        {
            get { return _MerchantInvoiceNumber; }
            set { _MerchantInvoiceNumber = value; }
        }

    }
}
